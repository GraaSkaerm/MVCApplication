using MVCApplication.Models;

namespace MVCApplication
{
    public class CustomerClient : ICustomerClient
    {
        private HttpClient _client;

        public CustomerClient(HttpClient client)
        {
            _client = client;
        }
        
        private IList<T> ConvertToJson<T>(HttpContent content)
        {
            Task<IList<T>?> readTask = content.ReadFromJsonAsync<IList<T>>();
            readTask.Wait();

            IList<T>? resault = readTask.Result;
            if (resault != null) return resault;
            return new List<T>();
        }

        private IEnumerable<T> RequestCollection<T>(string collectionName)
        {
            HttpResponseMessage response = RequestMessage(collectionName);
            if (response.IsSuccessStatusCode == false) return Enumerable.Empty<T>();
            return ConvertToJson<T>(response.Content);
        }

        private HttpResponseMessage RequestMessage(string message)
        {

            Task<HttpResponseMessage> responseTask = _client.GetAsync(message);
            responseTask.WaitAsync(new TimeSpan());
            HttpResponseMessage response = responseTask.Result;
            return response;
        }


        private List<T> GetCollection<T>(CustomerCollectionRequest request)
        {
            string requestName = request.ToString();
            IEnumerable<T> collection = RequestCollection<T>(requestName);
            return new List<T>(collection);
        }

        public List<SalesPerson> RequestSalesPeople()
        {
            CustomerCollectionRequest request = CustomerCollectionRequest.SalesPersons;
            return GetCollection<SalesPerson>(request);
        }

        public List<Order> RequestOrders()
        {
            CustomerCollectionRequest request = CustomerCollectionRequest.Orderlines;
            return GetCollection<Order>(request);
        }
    }
}
