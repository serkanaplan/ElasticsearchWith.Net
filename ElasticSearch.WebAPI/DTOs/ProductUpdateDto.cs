
using Elasticsearch.WebAPI.DTOs;

namespace Elasticsearch.webAPI.DTOs;
public record ProductUpdateDto(string Id, string Name, decimal Price, int Stock, ProductFeatureDto Feature)
{
}