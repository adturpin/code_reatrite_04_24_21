using System.Collections.Generic;

namespace SupermarketReceipt
{
    public class Items
    {
        private readonly List<ProductQuantity> _items = new List<ProductQuantity>();
        public Items()
        {
            
        }

        private Items(List<ProductQuantity> list)
        {
            _items = new List<ProductQuantity>(list);
        }
        public List<ProductQuantity> GetAll()
        {
            return new List<ProductQuantity>(_items);
        }

        public Items Add(ProductQuantity productQuantity)
        {
            var list = new List<ProductQuantity>(_items);
            list.Add(productQuantity);
            return new Items(list);
        }

    }
}
