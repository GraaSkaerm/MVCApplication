using Microsoft.AspNetCore.Mvc;
using MVCApplication.Models;

namespace MVCApplication.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerContainer _customerContainer;
        private ICustomerClient _customerClient;

        public CustomerController(ICustomerClient customerClient, ICustomerContainer customerContainer)
        {
            _customerClient = customerClient;
            _customerContainer = customerContainer;
        }

        private bool HasCustomers(List<Customer> customers)
        {
            return customers.Count != 0;
        }



        private List<Customer> LoadCustomers()
        {

            List<Order> allOrders = _customerClient.RequestOrders();
            List<SalesPerson> salesPeople = _customerClient.RequestSalesPeople();

            List<Customer> customers = new List<Customer>();

            Parallel.For(0, salesPeople.Count, (personIndex) =>
            {
                SalesPerson person = salesPeople[personIndex];
                List<Order> orders = GetAllSalesPersonOrders(allOrders, person.Id);

                lock (customers)
                {
                    customers.Add(new Customer(person, orders));
                }
            });

            customers.Sort();

            return customers;
        }


        private List<Order> GetAllSalesPersonOrders(List<Order> orders, int salesPersonId)
        {
            List<Order> salesPersonOrders = new List<Order>();

            foreach (Order order in orders)
            {
                if (order.SalesPersonId == salesPersonId)
                {
                    salesPersonOrders.Add(order);
                }
            }

            return salesPersonOrders;
        }



        public IActionResult Index()
        {
            List<Customer> customers = _customerContainer.Customers;
            if (HasCustomers(customers) == true) return View(customers);


            List<Customer> laodedCustomers = LoadCustomers();
            _customerContainer.Customers.AddRange(laodedCustomers);


            return View(_customerContainer.Customers);
        }


        public IActionResult Details(int personID)
        {
            
            return View(_customerContainer.Customers[personID]);
        }

    }
}
