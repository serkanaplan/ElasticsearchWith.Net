
using Elasticsearch.WebAPI.Models;

namespace Elasticsearch.WebAPI.DTOs;

public record ProductCreateDto(string Name, decimal Price, int Stock, ProductFeatureDto Feature)
{

    //automapper kullanmak yerine gelen dtoyu producta dönüştürme işlemini burda yaptık
    public Product CreateProduct()
    {
        return new Product
        {
            Name = Name,
            Price = Price,
            Stock = Stock,
            Feature = new ProductFeature()
            {
                Width = Feature.Width,
                Height = Feature.Height,
                Color = (EColor)int.Parse(Feature.Color)
            }
        };
    }
}