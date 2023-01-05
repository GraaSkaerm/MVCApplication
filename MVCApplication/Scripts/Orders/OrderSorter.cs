
using MVCApplication.Models;

namespace MVCApplication
{
    public class OrderSorter
    {
        private List<Order> _orders;

        public OrderSorter(List<Order> orders)
        {
            _orders = orders;
        }


        public void Sort(OrderSortingType sortingType)
        {
            switch (sortingType)
            {
                case OrderSortingType.Id: _orders.Sort(CompareIds); break;
                case OrderSortingType.DateTimes: _orders.Sort(CampareDateTimes); break;
                default: throw new Exception("Sortin type not implemented.");
            }
        }

        private int CompareIds(Order a, Order b)
        {
            if (a.Id < b.Id) return -1;
            if (a.Id > b.Id) return 1;
            throw new Exception("Id matches.");
        }

        private int CampareDateTimes(Order a, Order b)
        {
            if (a.DateTime < b.DateTime) return -1;
            if (a.DateTime > b.DateTime) return 1;
            return 0;
        }
    }
}
