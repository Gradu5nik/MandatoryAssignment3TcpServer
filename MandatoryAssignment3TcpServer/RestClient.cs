using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using MandatoryAssigment1Library;
using Newtonsoft.Json;

namespace MandatoryAssignment3TcpServer
{
    public class RestClient
    {
        public async Task<string> GetAll()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await
                    client.GetAsync("http://localhost:22887/api/Books");
                //IEnumerable<Book> res = await
                //    response.Content.ReadFromJsonAsync<IEnumerable<Book>>();
                string res = await
                    response.Content.ReadAsStringAsync();
                return res;
            }
        }
        public async Task<string> Get(string isbn)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await
                       client.GetAsync("http://localhost:22887/api/Books/"+isbn);
                string res = await
                    response.Content.ReadAsStringAsync();
                return res;
            }
        }
        public async Task<Book> Post(string bookJson)
        {
            using (HttpClient client = new HttpClient())
            {
                JsonContent bookToPost = JsonContent.Create(bookJson);
                HttpResponseMessage response = await
                    client.PostAsync("http://localhost:22887/api/Books/", bookToPost);
                Book res = await
                    response.Content.ReadFromJsonAsync<Book>();
                return res;
            }
        }
        public async Task<Book> Put(string bookJson)
        {
            using (HttpClient client = new HttpClient())
            {
                JsonContent bookToPut = JsonContent.Create(bookJson);
                Book updatedBook = JsonConvert.DeserializeObject<Book>(bookJson);
                HttpResponseMessage response = await
                    client.PutAsync("http://localhost:22887/api/Books/" + updatedBook.ISBN13, bookToPut);
                Book res = await
                    response.Content.ReadFromJsonAsync<Book>();
                return res;
            }
        }
        public async void Remove(string isbn)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await
                    client.DeleteAsync("http://localhost:22887/api/Books/" + isbn);

            }
        }
    }
}
