using MVCApplication.Models;

namespace MVCApplication
{
    public interface ICustomerClient
    {
        public List<SalesPerson> RequestSalesPeople();
        public List<Order> RequestOrders();
    }
}
