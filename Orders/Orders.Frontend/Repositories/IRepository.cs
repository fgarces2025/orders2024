namespace Orders.Frontend.Repositories
{
    public interface IRepository
    {
        Task<HttpResponseWraper<T>> GetAsync<T>(string url);

        Task<HttpResponseWraper<object>> PostAsync<T>(string url, T model);

        Task<HttpResponseWraper<TActionResponse>> PostAsync<T, TActionResponse>(string url, T model);

        Task<HttpResponseWraper<object>> DeleteAsync<T>(string url);
        Task<HttpResponseWraper<object>> PutAsync<T>(string url, T model);
        Task<HttpResponseWraper<TActionResponse>> PutAsync<T, TActionResponse>(string url, T model);
    }
}
