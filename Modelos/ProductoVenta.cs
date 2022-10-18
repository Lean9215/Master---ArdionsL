using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Modelos
{
    public class ProductoVenta
    {
        public int Id { get; set; }
        public int Stock { get; set; }
        public int IdProducto { get; set; }
        public int IdVenta { get; set; }

        public ProductoVenta() //constructor
        {
            Id = 0;
            Stock = 0;
            IdProducto = 0;
            IdVenta = 0;    
        }
    }
}
