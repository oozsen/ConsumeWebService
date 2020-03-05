using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsumeWebService.ConsumingService
{
    public class APIViewModel : BaseViewModel
    {                   
        private List<Posts> _postsList { get; set; }
        public List<Posts> PostsList
        {
            get
            {
                return _postsList;
            }

            set
            {
                if (value != _postsList)
                {
                    _postsList = value;
                    NotifyPropertyChanged();
                }
            }
        }

               
        public APIViewModel()
        {
            GetDataAsync();
        }

      

        private async void GetDataAsync()
        {
            IsLoading = true;
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.GetAsync("https://my-json-server.typicode.com/oozsen/mockjson/posts");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var posts = JsonConvert.DeserializeObject<List<Posts>>(content);
                PostsList = new List<Posts>(posts);
            }
            else
            {
                Debug.WriteLine("Veri çekilirken bir hata oluştu!!");
            }
            IsLoading = false;
        }

        private async void PostDataAsync()
        {
            var uri = "https://my-json-server.typicode.com/oozsen/mockjson/posts";
            IsLoading = true;
            HttpClient httpClient = new HttpClient();

            var newPost = new Posts()
            {
                id = 4,
                title = "title 4",
                description = "description 4"
            };

            var jsonObject = JsonConvert.SerializeObject(newPost);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Veri başarılı olarak kaydedilmiştir!");
            }
            else
            {
                Debug.WriteLine("Veri çekilirken bir hata oluştu!!!");
            }
            IsLoading = false;
        }

        private async void DeleteDataAsync()
        {
            var url = "https://my-json-server.typicode.com/oozsen/mockjson/posts";
            IsLoading = true;
            HttpClient httpClient = new HttpClient();

            var id = 4;
            var uri = new Uri(string.Format(url, id));

            var response = await httpClient.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Veri başarılı bir şekilde silindi!");
            }
            else
            {
                Debug.WriteLine("Veri silinirken bir hata oluştu!!");
            }
            IsLoading = false;
        }


        public class Posts
        {
            public int id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
        }
    }
}
