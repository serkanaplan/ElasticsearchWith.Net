using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elasticsearch.webAPI.DTOs;
using Elasticsearch.WebAPI.Models;
using System.Collections.Immutable;

namespace Elasticsearch.webAPI.Repo;
public class ProductRepository(ElasticsearchClient client)
{
    private readonly ElasticsearchClient _client = client;
    private const string indexName = "products";


    public async Task<Product?> SaveAsync(Product newProduct)
    {
        newProduct.Created = DateTime.Now;
        // IndexAsync metodu, veri kaydetme işlemini yapar.
        // var response = await _client.IndexAsync(newProduct, x => x.Index(indexName));
        var response = await _client.IndexAsync(newProduct, x => x.Index(indexName).Id(Guid.NewGuid().ToString()));

        if (!response.IsValidResponse) return null;

        newProduct.Id = response.Id;

        return newProduct;
    }


    public async Task<ImmutableList<Product>> GetAllAsync()
    {
        var result = await _client.SearchAsync<Product>(s => s.Index(indexName).Query(q => q.MatchAll(new MatchAllQuery())));

        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;

        // return result.Documents.ToImmutableList();
        return [.. result.Documents];
    }

    

    public async Task<Product?> GetByIdAsync(string id)
    {
        var response = await _client.GetAsync<Product>(id, x => x.Index(indexName));

        if (!response.IsValidResponse && response.Source == null) return null;

        response.Source!.Id = response.Id;

        return response.Source;
    }


    public async Task<bool> UpdateSynch(ProductUpdateDto updateProduct)
    {
        var response = await _client.UpdateAsync<Product, ProductUpdateDto>(indexName,updateProduct.Id, x => x.Index(indexName).Doc(updateProduct));

        return response.IsValidResponse;
    }


    /// <summary>
    /// Hata yönetimi için bu method ele alınmıştır.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DeleteResponse> DeleteAsync(string id)
    {
        var response = await _client.DeleteAsync<Product>(id, x => x.Index(indexName));

        return response;
    }
}