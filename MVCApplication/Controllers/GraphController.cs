using Microsoft.AspNetCore.Mvc;
using MVCApplication.Models;
using Newtonsoft.Json;

namespace MVCApplication.Controllers
{
    public class GraphController : Controller
    {
        private IOrderContainer _orderContainer;
        private ICustomerClient _customerClient;

        private List<Order> _orders;

        private Dictionary<string, int> _volumeByDate;

        public GraphController(ICustomerClient customerClient, IOrderContainer orderContainer)
        {
            _customerClient = customerClient;
            _orderContainer = orderContainer;
            _orders = _orderContainer.Orders;
            _volumeByDate = new Dictionary<string, int>();
        }

        private void RequestOrders()
        {
            List<Order> orders = _customerClient.RequestOrders();
            orders.Sort(CompareOrdersByDate);
            _orderContainer.Orders.AddRange(orders);
        }

        private int CompareOrdersByDate(Order a, Order b)
        {
            DateTime aDateTime = a.DateTime;
            DateTime bDateTime = b.DateTime;
            if (aDateTime < bDateTime) return -1;
            if (aDateTime > bDateTime) return 1;
            return 0;
        }

        private void UpdateVolumeByDate()
        {
            foreach (Order order in _orders)
            {
                DateTime dateTime = order.DateTime;
                string yearMonth = dateTime.ToString("Y");

                if (_volumeByDate.ContainsKey(yearMonth) == false)
                {
                    _volumeByDate.Add(yearMonth, 1);
                    continue;
                }

                _volumeByDate[yearMonth] += 1;
            }
        }


        public IActionResult Index()
        {
            if (_orderContainer.HasOrders() == false) RequestOrders();
            UpdateVolumeByDate();
            List<DataPoint> dataPoints = new List<DataPoint>();


            foreach (var keyValue in _volumeByDate)
            {
                int amount = keyValue.Value;
                string yearMonth = keyValue.Key;
                dataPoints.Add(new DataPoint(yearMonth, amount));

            }


            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View();
        }
    }
}
