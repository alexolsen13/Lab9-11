﻿namespace SportStore.Models
{
    public class Cart //корзина
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();

        public void AddItem(Product product, int quantity) // добавление в корзину
        {
            CartLine line = Lines.Where(p => p.Product.ProductID == product.ProductID)
                .FirstOrDefault();
            if(line==null)
            {
                Lines.Add(new CartLine()
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Product product) // удаление элемента из корзины
        {
            Lines.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }

        public decimal ComputeTotalValue() //расчёт общей стоимости покупки
        {
            return Lines.Sum(e => e.Product.Price * e.Quantity);
        }

        public void Clear() // очистка корзины
        {
            Lines.Clear();
        }
    }

    //определяет выбранный товар
    public class CartLine
    {
        public int CartLineId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
