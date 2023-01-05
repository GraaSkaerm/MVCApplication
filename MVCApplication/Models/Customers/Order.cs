namespace MVCApplication.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int SalesPersonId { get; set; }

        public int OrderPrice { get; set; }
        public string? OrderName { get; set; }
        public string? OrderDate { get; set; }

        public DateTime DateTime
        {
            get
            {
                if (OrderDate == null) throw new Exception();
                return DateTime.Parse(OrderDate);
            }
        }

        private DateTime? _dateTime;


    }
}
