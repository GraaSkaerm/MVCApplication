using MVCApplication.Models;

namespace MVCApplication
{
    public interface ICustomerContainer
    {
        public List<Customer> Customers { get; }
    }
}
