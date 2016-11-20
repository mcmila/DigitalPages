using System;
using RestSharp;

namespace Marvelous
{
    public class MarvelClient
    {
        private readonly string _publicKey;
        private readonly string _privateKey;
        private readonly IRestClient _restClient;

        public MarvelClient(string publicKey, string privateKey, IRestClient restClient = null)
        {
            if (string.IsNullOrEmpty(publicKey)) throw new ArgumentException("You must supply a public API key.");
            if (string.IsNullOrEmpty(privateKey)) throw new ArgumentException("You must supply a private API key.");

            _publicKey = publicKey;
            _privateKey = privateKey;
            _restClient = restClient;
        }

        public CharactersClient<Character> Characters
        {
            get
            {
                return new CharactersClient<Character>(_publicKey, _privateKey, _restClient);
            }
        }

        public CharactersClient<Comic> CharactersComics
        {
            get
            {
                return new CharactersClient<Comic>(_publicKey, _privateKey, _restClient);
            }
        }

        public ComicsClient<Comic> Comics
        {
            get
            {
                return new ComicsClient<Comic>(_publicKey, _privateKey, _restClient);
            }
        }

        public CreatorsClient<Creator> Creators
        {
            get
            {
                return new CreatorsClient<Creator>(_publicKey, _privateKey, _restClient);
            }
        }

        public EventsClient<Event> Events
        {
            get
            {
                return new EventsClient<Event>(_publicKey, _privateKey, _restClient);
            }
        }

        public SeriesClient<Series> Series
        {
            get
            {
                return new SeriesClient<Series>(_publicKey, _privateKey, _restClient);
            }
        }

        public StoriesClient<Story> Stories
        {
            get
            {
                return new StoriesClient<Story>(_publicKey, _privateKey, _restClient);
            }
        }
        
    }
}