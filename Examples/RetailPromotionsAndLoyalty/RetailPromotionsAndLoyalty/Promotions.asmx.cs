using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;

namespace RetailPromotionsAndLoyalty
{
    /// <summary>
    /// Summary description for Promotions
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Promotions : System.Web.Services.WebService
    {
        private static PromotionsApplication app = new PromotionsApplication();

        [WebMethod]
        public Basket BeginSale()
        {
            var ret = default(Basket);

            Task.Run(
                async () =>
                {
                    ret = (await app.Raise<BeginSaleSuccess>(new BeginSaleEvent())).Basket;
                }).Wait();

            return ret;
        }

        [WebMethod]
        public void AddProduct(SalesLine saleline)
        {
            Task.Run(
                async () =>
                {
                    await app.Raise<AddProductSuccess>(
                        new AddProductEvent()
                        {
                            SalesLine = saleline,
                        });
                }).Wait();
        }

        [WebMethod]
        public Basket RequestTotal(Basket basket)
        {
            Task.Run(
                async () =>
                {
                    basket = (await app.Raise<RequestTotalSuccess>(
                        new RequestTotalEvent()
                        {
                            Basket = basket,
                        })).Basket;
                }).Wait();

            return basket;
        }

        [WebMethod]
        public void AddPayment(Payment payment)
        {
            Task.Run(
                async () =>
                {
                    await app.Raise<AddPaymentSuccess>(
                        new AddPaymentEvent()
                        {
                            Payment = payment,
                        });
                }).Wait();
        }

        [WebMethod]
        public void EndSale(Basket basket)
        {
            Task.Run(
                async () =>
                {
                    await app.Raise<EndSaleSuccess>(
                        new EndSaleEvent()
                        {
                            Basket = basket,
                        });
                }).Wait();
        }

        [WebMethod]
        public void SetCustomer(Basket basket, Customer customer)
        {
            Task.Run(
                async () =>
                {
                    await app.Raise<SetCustomerSuccess>(
                        new SetCustomerEvent()
                        {
                            Basket = basket,
                            Customer = customer,
                        });
                }).Wait();
        }
    }
}
