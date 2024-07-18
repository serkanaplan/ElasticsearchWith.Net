
using Elasticsearch.WebAPI.DTOs;

namespace Elasticsearch.webAPI.DTOs;

public record ProductDto(string Id, string Name, decimal Price, int Stock, ProductFeatureDto? Feature)
{
}