using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orders
{
    public partial class NewOrderAction
    {
        public override void Execute()
        {
            var frm = new FormOrder();
            frm.Order = Order;
            frm.Text = "Primeira aprovação";
            FirstApproveEvent.Order = Order;
            frm.Event = FirstApproveEvent;

            FormOrder.InvokeOnUI(new Action(() => frm.Show()));
        }
    }

    public partial class FirstApproveAction
    {
        public override void Execute()
        {
            var frm = new FormOrder();
            frm.Order = Order;
            frm.Text = "Segunda aprovação";
            SecondApproveEvent.Order = Order;
            frm.Event = SecondApproveEvent;

            FormOrder.InvokeOnUI(new Action(() => frm.Show()));
        }
    }

    public partial class SecondApproveAction
    {
        public override void Execute()
        {
            var frm = new FormOrder();
            frm.Order = Order;
            frm.Approved = true;
            frm.Text = "Approvado";

            FormOrder.InvokeOnUI(new Action(() => frm.Show()));
        }
    }

    public partial class OrdersApplication
    {
    }
}
