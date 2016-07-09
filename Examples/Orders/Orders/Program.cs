using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Orders
{
    public class Program
    {
        public static OrdersApplication App { get; } = new OrdersApplication();
 
        static void Main(string[] args)
        {
            Application.Run(new FormOrder());
        }
    }
}
