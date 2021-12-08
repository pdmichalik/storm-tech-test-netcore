using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Todo.Services
{
    public class GravatarApiClient
    {
        private HttpClient httpClient;
        private const string defaultGravatarImage = "http://www.gravatar.com/avatar";

        public GravatarApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<(string, string)> GetProfile(string email)
        {
            try
            {
                var emailHash = email.GetHash();
                var response = await httpClient.GetFromJsonAsync<Response>($"{emailHash}.json");
                var entry = response.entry.FirstOrDefault();
                if (entry == null) return ("", "");
                var name = entry.displayName;
                var photo = entry.photos != null && entry.photos.Any() &&
                            !string.IsNullOrWhiteSpace(entry.photos.First().value)
                    ? entry.photos.First().value
                    : "";
                return (name, photo);
            }
            catch (Exception ex)
            {
                if (ex is HttpRequestException || ex is TaskCanceledException)
                    return ("", defaultGravatarImage);

                throw;
            }
            
        }

        public class Response
        {
            public Entry[] entry { get; set; }
        }

        public class Photo
        {
            public string value { get; set; }
        }

        public class Entry
        {
            public string id { get; set; }
            public List<Photo> photos { get; set; }
            public List<object> name { get; set; }
            public string displayName { get; set; }
        }
    }
}