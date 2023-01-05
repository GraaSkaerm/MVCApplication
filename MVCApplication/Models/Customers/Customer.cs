using MVCApplication.Models;

namespace MVCApplication.Models
{
    public class Customer : IComparable<Customer>
    {
        public SalesPerson SalesPerson { get; }
        public List<Order> Orders { get; }

        public Customer(SalesPerson salesPerson, List<Order> orders)
        {
            this.SalesPerson = salesPerson;
            this.Orders = orders;
        }

        public int CompareTo(Customer? other)
        {
            if (other == null) return 0;
            if (SalesPerson.Id < other.SalesPerson.Id) return -1;
            return 1;
        }
    }
}
