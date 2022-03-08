#nullable disable
namespace Etl.Processamento.Helpers
{
    public class GenerateResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class GenerateResponse<T> : GenerateResponse
    {
        public T Value { get; set; }
    }
}
