using System;
namespace MauiBlazor.Shared.Models;

public interface IResponseModel
{
    Exception Ex { get; set; }
    string Message { get; set; }
    bool Success { get; set; }
}

public class ResponseModel<T> : IResponseModel
{
    public T Data { get; set; }
    public Exception Ex { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }

    public ResponseModel(bool success = false)
    {
        Success = success;
    }


}