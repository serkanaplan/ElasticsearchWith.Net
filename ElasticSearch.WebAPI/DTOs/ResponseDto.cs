
using System.Net;

namespace Elasticsearch.webAPI.DTOs;

public record ResponseDto<T>
{
    public T? Data { get; set; }

    public List<string>? Errors { get; set; }

    public HttpStatusCode Status { get; set; }


    // Static Factory Method
    public static ResponseDto<T> Success(T data, HttpStatusCode status) => new() { Data = data, Status = status };

    public static ResponseDto<T> Fail(List<string> errors, HttpStatusCode status) => new() { Errors = errors, Status = status };

    public static ResponseDto<T> Fail(string error, HttpStatusCode status) => new() { Errors = [error], Status = status };

}