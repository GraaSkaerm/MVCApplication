using MVCApplication.Extensions;
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






        // New example
        public List<Order> GetAllSalesPersonOrders(int salesPersonId)
        {
            List<Order>[] batches = BatchExtension.CreatBatches(_orders, 50);

            int amountOfBatches = batches.Length;
            List<Order>[] allSalesPeoplesOrders = new List<Order>[amountOfBatches];


            Parallel.For(0, amountOfBatches, (batchIndex) =>
            {
                List<Order> batch = batches[batchIndex];
                allSalesPeoplesOrders[batchIndex] = GetAllSalesPersonOrders(batch, salesPersonId);
            });


            List<Order> salesPersonsOrders = new List<Order>();

            foreach (var batch in allSalesPeoplesOrders)
            {
                salesPersonsOrders.AddRange(batch);
            }


            return salesPersonsOrders;
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


    }
}
