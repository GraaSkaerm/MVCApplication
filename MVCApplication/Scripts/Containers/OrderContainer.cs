using MVCApplication.Models;

namespace MVCApplication
{
    public class OrderContainer : IOrderContainer
    {
        private List<Order> _orders;

        public List<Order> Orders { get => _orders; }


        public OrderContainer()
        {
            _orders = new List<Order>();
        }



        public bool HasOrders()
        {
            return _orders.Count != 0;
        }
    }
}
