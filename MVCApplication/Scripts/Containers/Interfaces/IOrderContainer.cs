using MVCApplication.Models;

namespace MVCApplication
{
    public interface IOrderContainer
    {
        public List<Order> Orders { get; }
        public bool HasOrders();
    }
}
