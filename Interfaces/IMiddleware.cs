namespace MindFree.Interfaces
{
    public interface IMiddleware
    {
        public Task Intercept();
    }
}
