using MVCApplication.Models;

namespace MVCApplication
{
    public class CustomerContainer : ICustomerContainer
    {
        private List<Customer> _costumers;
        public List<Customer> Customers { get => _costumers; }

        public CustomerContainer()
        {
            _costumers = new List<Customer>();
        }



    }
}
