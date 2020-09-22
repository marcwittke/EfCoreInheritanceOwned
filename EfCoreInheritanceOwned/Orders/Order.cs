namespace EfCoreInheritanceOwned.Orders
{
    public class Order
    {
        public Order(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
        public string Recipient { get; set; }
    }

    public class ExternalOrder : Order
    {
        public ExternalOrder(int id) : base(id)
        {
        }

        public OrderSupplierInfo Supplier { get; set; }
    }

    public class InternalOrder : Order
    {
        public InternalOrder(int id) : base(id)
        {
        }

        public OrderStoreInfo Store { get; set; }
    }

    public class OrderSupplierInfo
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class OrderStoreInfo
    {
        public string Name { get; set; }
        public string Site { get; set; }
    }
}