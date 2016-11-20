using System.Collections.Specialized;
using System.Threading.Tasks;
using RestSharp;

namespace Marvelous
{
    public class SeriesClient<T> : ClientBase<T> where T : MarvelBase
    {
        internal SeriesClient(string publicKey, string privateKey) : base(publicKey, privateKey)
        { }

        internal SeriesClient(string publicKey, string privateKey, IRestClient restClient) : base(publicKey, privateKey, restClient)
        { }

        #region Sync
        public dynamic Characters(int id, int limit = 20, int offset = 0, NameValueCollection queryParameters = null)
        {
            return QueryIdSubPath(id, CharactersResource, limit, offset, queryParameters);
        }

        public dynamic Comics(int id, int limit = 20, int offset = 0, NameValueCollection queryParameters = null)
        {
            return QueryIdSubPath(id, ComicsResource, limit, offset, queryParameters);
        }

        public dynamic Creators(int id, int limit = 20, int offset = 0, NameValueCollection queryParameters = null)
        {
            return QueryIdSubPath(id, CreatorsResource, limit, offset, queryParameters);
        }

        public dynamic Events(int id, int limit = 20, int offset = 0, NameValueCollection queryParameters = null)
        {
            return QueryIdSubPath(id, EventsResource, limit, offset, queryParameters);
        }

        public dynamic Stories(int id, int limit = 20, int offset = 0, NameValueCollection queryParameters = null)
        {
            return QueryIdSubPath(id, StoriesResource, limit, offset, queryParameters);
        }
        #endregion

        #region Async
        public async Task<dynamic> CharactersAsync(int id, int limit = 20, int offset = 0, NameValueCollection queryParameters = null)
        {
            return await QueryIdSubPathAsync(id, CharactersResource, limit, offset, queryParameters);
        }

        public async Task<dynamic> ComicsAsync(int id, int limit = 20, int offset = 0, NameValueCollection queryParameters = null)
        {
            return await QueryIdSubPathAsync(id, ComicsResource, limit, offset, queryParameters);
        }

        public async Task<dynamic> CreatorsAsync(int id, int limit = 20, int offset = 0, NameValueCollection queryParameters = null)
        {
            return await QueryIdSubPathAsync(id, CreatorsResource, limit, offset, queryParameters);
        }

        public async Task<dynamic> EventsAsync(int id, int limit = 20, int offset = 0, NameValueCollection queryParameters = null)
        {
            return await QueryIdSubPathAsync(id, EventsResource, limit, offset, queryParameters);
        }

        public async Task<dynamic> StoriesAsync(int id, int limit = 20, int offset = 0, NameValueCollection queryParameters = null)
        {
            return await QueryIdSubPathAsync(id, StoriesResource, limit, offset, queryParameters);
        }
        #endregion

        protected override string Resource
        {
            get { return "series"; }
        }
    }
  
}