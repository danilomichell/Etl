namespace Etl.Processamento.Helpers
{
    public abstract class AbstractResponse : IAbstractResponse
    {
        public GenerateResponse GenerateErroResponse
            (string message) =>
            new()
            {
                Message = message,
                Success = false
            };
        public GenerateResponse<T> GenerateErroResponse<T>
            (string message) =>
            new()
            {
                Message = message,
                Success = false,
                Value = default!
            };
        public GenerateResponse GenerateSuccessResponse
            () =>
            new()
            {
                Success = true,
                Message = string.Empty
            };
        public GenerateResponse<T> GenerateSuccessResponse<T>
            (T value) =>
            new()
            {
                Message = string.Empty,
                Success = true,
                Value = value
            };
    }

    public interface IAbstractResponse
    {
        GenerateResponse GenerateErroResponse(string message);
        GenerateResponse<T> GenerateErroResponse<T>(string message);
        GenerateResponse GenerateSuccessResponse();
        GenerateResponse<T> GenerateSuccessResponse<T>(T value);
    }
}
