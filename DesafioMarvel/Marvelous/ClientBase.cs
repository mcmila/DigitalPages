using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace Marvelous
{
    public abstract class ClientBase<T> where T : MarvelBase
    {
        protected const string CharactersResource = "characters";
        protected const string ComicsResource = "comics";
        protected const string CreatorsResource = "creators";
        protected const string EventsResource = "events";
        protected const string SeriesResource = "series";
        protected const string StoriesResource = "stories";

        internal const string BaseUrl = "http://gateway.marvel.com/v1/public/";
        private readonly string _publicKey;
        private readonly string _privateKey;
        private readonly IRestClient _restClient;

        internal ClientBase(string publicKey, string privateKey)
        {
            _publicKey = publicKey;
            _privateKey = privateKey;
            _restClient = CreateRequestClient();
        }

        internal ClientBase(string publicKey, string privateKey, IRestClient restClient)
        {
            _publicKey = publicKey;
            _privateKey = privateKey;
            _restClient = restClient ?? CreateRequestClient();
        }

        private static RestClient CreateRequestClient()
        {
            var client = new RestClient(BaseUrl);
            return client;
        }

        public MarvelRoot<T> Find(int id)
        {
            return QueryIdSubPath(id);
        }

        public async Task<MarvelRoot<T>> FindAsync(int id)
        {
            return await QueryIdSubPathAsync(id);
        }

        public MarvelRoot<T> FindAll(int limit = 20, int offset = 0, NameValueCollection queryParameters = null)
        {
            var options = GetOptions(limit, offset, queryParameters);

            return Query(Resource, options);
        }

        public async Task<MarvelRoot<T>> FindAllAsync(int limit = 20, int offset = 0, NameValueCollection queryParameters = null)
        {
            var options = GetOptions(limit, offset, queryParameters);

            return await QueryAsync(Resource, options);
        }

        protected MarvelRoot<T> QueryIdSubPath(int id, string path = null, int limit = 20, int offset = 0, NameValueCollection queryParameters = null)
        {
            return Query(GetResource(path), GetOptions(limit, offset, queryParameters), UrlSegmentFor("id", id));
        }

        protected async Task<MarvelRoot<T>> QueryIdSubPathAsync(int id, string path = null, int limit = 20, int offset = 0, NameValueCollection queryParameters = null)
        {
            return await QueryAsync(GetResource(path), GetOptions(limit, offset, queryParameters), UrlSegmentFor("id", id));
        }

        private MarvelRoot<T> Query(string resourcePath, NameValueCollection options, NameValueCollection urlSegments = null)
        {
            var request = PrepareRequest(resourcePath, options, urlSegments);

            var response = _restClient.Execute<MarvelRoot<T>>(request);

            if (response.ErrorException == null)
            {
                return response.Data;
            }

            throw new ApplicationException("Error retrieving response.  Check inner exception details for details.",
                response.ErrorException);
        }

        private Task<MarvelRoot<T>> QueryAsync(string resourcePath, NameValueCollection options, NameValueCollection urlSegments = null)
        {
            var request = PrepareRequest(resourcePath, options, urlSegments);

            var source = new TaskCompletionSource<MarvelRoot<T>>();

            _restClient.ExecuteAsync<dynamic>(request, response =>
            {
                if (response.ErrorException == null)
                {
                    var marvelRoot = JsonConvert.DeserializeObject<MarvelRoot<T>>(response.Content);
                    source.SetResult(marvelRoot);
                }
                else
                {
                    throw new ApplicationException("Error retrieving response.  Check inner exception details for details.",
                        response.ErrorException);
                }
            });

            return source.Task;
        }

        private string GetResource(string path)
        {
            var resourceUri = Resource + "/{id}";

            if (!string.IsNullOrEmpty(path))
            {
                resourceUri += "/" + path;
            }
            return resourceUri;
        }

        private static NameValueCollection GetOptions(int limit, int offset, NameValueCollection queryParameters)
        {
            var options = queryParameters ?? new NameValueCollection();
            options.Add("limit", limit.ToString(CultureInfo.InvariantCulture));
            options.Add("offset", offset.ToString(CultureInfo.InvariantCulture));
            return options;
        }

        private RestRequest PrepareRequest(string resourcePath, NameValueCollection options, NameValueCollection urlSegments = null)
        {
            options = options ?? new NameValueCollection();
            urlSegments = urlSegments ?? new NameValueCollection();

            var timestamp = GetTimestamp();
            var hash = GetHash(timestamp);
            var request = new RestRequest { Resource = resourcePath };

            options.AllKeys.ToList().ForEach(o => request.AddParameter(o, options[o], ParameterType.QueryString));

            urlSegments.AllKeys.ToList().ForEach(segment => request.AddUrlSegment(segment, urlSegments[segment]));

            request.AddParameter("ts", timestamp, ParameterType.QueryString);
            request.AddParameter("apikey", _publicKey, ParameterType.QueryString);
            request.AddParameter("hash", hash, ParameterType.QueryString);

            return request;
        }

        private string GetHash(string timestamp)
        {
            var combined = timestamp + _privateKey + _publicKey;
            var bytes = Encoding.UTF8.GetBytes(combined);
            var hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(bytes);
            var converted = BitConverter.ToString(hash);

            return converted.Replace("-", string.Empty).ToLower();
        }

        private static string GetTimestamp()
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var diff = DateTime.UtcNow - origin;

            return Math.Floor(diff.TotalSeconds).ToString(CultureInfo.InvariantCulture);
        }

        private static NameValueCollection UrlSegmentFor(string segmentName, object segmentValue)
        {
            return new NameValueCollection { { segmentName, segmentValue.ToString() } };
        }

        protected abstract string Resource { get; }
    }

}