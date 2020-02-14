namespace auth.server.jwt.Models.Response
{
    public interface IResponse
    {
        bool Success { get; set; }
        string Message { get; set; }
    }

    public class AppResponse : IResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}
