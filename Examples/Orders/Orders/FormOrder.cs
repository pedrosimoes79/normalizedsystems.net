using NormalizedSystems.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Orders
{
    public partial class FormOrder : Form
    {
        private static FormOrder form;

        public static void InvokeOnUI(Action action)
        {
            form.Invoke(action);
        }

        public EventElement Event { get; set; }
    
        public Order Order { get; set; }

        public bool Approved { get; set; }

        public FormOrder()
        {
            if (form == null) form = this;

            InitializeComponent();
        }

        private void FormOrder_Load(object sender, EventArgs e)
        {
            if (Order != null)
            {
                txtDescription.Text = Order.Description;
                numValue.Value = Order.Value;
            }

            btnOK.Enabled = !Approved;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(Order == null)
            {
                var order = new Order();
                order.Description.Value = txtDescription.Text;
                order.Value.Value = numValue.Value;
                
                Program.App.Raise(new NewOrderEvent() { Order = order });
            }
            else
            {
                Order.Description.Value = txtDescription.Text;
                Order.Value.Value = numValue.Value;

                Program.App.Raise(Event);

                Close();
            }
        }
    }
}
