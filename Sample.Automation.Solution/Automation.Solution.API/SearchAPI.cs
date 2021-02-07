using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Automation.Solution.API
{
    public class SearchAPI
    {
        //Create a Rest Client
        //Create a Request
        //Return response
        public bool ValidatePostByID()
        {
            var restClient = new RestClient("https://jsonplaceholder.typicode.com");
            var restRequest = new RestRequest("/posts/1", Method.GET);



            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Authorization", Properties.Settings.Default.Token);

            restRequest.RequestFormat = DataFormat.Json;

            IRestResponse response = restClient.Execute(restRequest);
            var content = response.Content;

            Post bsObj = JsonConvert.DeserializeObject<Post>(content);

            if (bsObj.userId == 1)
            {
                return true;
            }
            else
                return false;
        }
        public bool ValidateAllPosts()
        {
            var restClient = new RestClient("https://jsonplaceholder.typicode.com");
            var restRequest = new RestRequest("/posts", Method.GET);



            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Authorization", Properties.Settings.Default.Token);

            restRequest.RequestFormat = DataFormat.Json;

            IRestResponse response = restClient.Execute(restRequest);
            var content = response.Content;

            var result = JsonConvert.DeserializeObject<List<Post>>(content);

            if (result[1].userId == 1)
            {
                return true;
            }
            else
                return false;
        }
    }
            
}


