using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RetailPromotionsAndLoyalty
{
    public partial class BeginSaleAction
    {
        public override void Execute()
        {
            var basket = new BasketVersion2();
            basket.BasketId.Value = Guid.NewGuid();

            PromotionsApplication.Baskets[basket.BasketId] = basket;

            BeginSaleSuccess.Basket = basket;
            BeginSaleSuccess.Raise();
        }
    }

    public partial class AddProductAction
    {
        public override void Execute()
        {
            var basketid = SalesLine.BasketId.Value;

            if (PromotionsApplication.Baskets.ContainsKey(basketid))
            {
                var basket = PromotionsApplication.Baskets[basketid];

                if (!PromotionsApplication.SalesLines.ContainsKey(basketid))
                    PromotionsApplication.SalesLines[basketid] = new List<SalesLine>();

                var saleslines = PromotionsApplication.SalesLines[basketid];

                SalesLine.SalesLineId.Value = Guid.NewGuid();

                saleslines.Add(SalesLine);

                basket.Total.Value += SalesLine.Quantity * SalesLine.UnitPrice;

                AddProductSuccess.SalesLine = SalesLine;
                AddProductSuccess.Raise();
            }
        }
    }

    public partial class RequestTotalAction
    {
        public override void Execute()
        {
            var basketid = Basket.BasketId.Value;

            if (PromotionsApplication.Baskets.ContainsKey(basketid))
            {
                RequestTotalSuccess.Basket = PromotionsApplication.Baskets[basketid];
                RequestTotalSuccess.Raise();
            }
        }
    }

    public partial class AddPaymentAction
    {
        public override void Execute()
        {
            var basketid = Payment.BasketId.Value;

            if (PromotionsApplication.Baskets.ContainsKey(basketid))
            {
                var basket = PromotionsApplication.Baskets[basketid];

                if (!PromotionsApplication.Payments.ContainsKey(basketid))
                {
                    if (basket.Total.Value == Payment.Value.Value)
                    {
                        PromotionsApplication.Payments[basketid] = Payment;

                        AddPaymentSuccess.Payment = Payment;
                        AddPaymentSuccess.Raise();
                    }
                }
            }
        }
    }

    public partial class EndSaleAction
    {
        public override void Execute()
        {
            var basketid = Basket.BasketId.Value;

            if (PromotionsApplication.Baskets.ContainsKey(basketid))
            {
                PromotionsApplication.Baskets.Remove(basketid);

                EndSaleSuccess.Raise();
            }
        }
    }

    public partial class FindPromotionAction
    {
        public override void Execute()
        {
            var barcode = SalesLine.Barcode.Value;

            if (PromotionsApplication.Promotions.ContainsKey(barcode))
            {
                var promo = PromotionsApplication.Promotions[barcode];

                FindPromotionSuccess.SalesLine = SalesLine;
                FindPromotionSuccess.Promotion = promo.Where(p => p.Type >= 1 && p.Type <= 3).First();

                FindPromotionSuccess.Raise();
            }
        }
    }

    public partial class ApplyPromotionAction
    {
        public override void Execute()
        {
            var basketid = SalesLine.BasketId.Value;
            var basket = PromotionsApplication.Baskets[basketid];
            var basket2 = default(BasketVersion2);

            if (!PromotionsApplication.BasketsWithPromotion.ContainsKey(basketid))
            {
                basket2 = new BasketVersion2();
                PromotionsApplication.BasketsWithPromotion[basketid] = basket2;

                basket2.BasketId.Value = basketid;
            }

            basket2 = PromotionsApplication.BasketsWithPromotion[basketid];

            var line = new SalesLineVersion2();

            line.BasketId.Value = SalesLine.BasketId;
            line.Barcode.Value = SalesLine.Barcode;
            line.Description.Value = SalesLine.Description;
            line.Quantity.Value = SalesLine.Quantity;
            line.UnitPrice.Value = SalesLine.UnitPrice;
            line.SalesLineId.Value = SalesLine.SalesLineId;

            line.Discount.Value = line.Quantity * line.UnitPrice * (Promotion.Discount / 100.0M);
            line.PromotionCode = Promotion.PromotionCode;

            if (!PromotionsApplication.SalesLinesWithPromotion.ContainsKey(basketid))
                PromotionsApplication.SalesLinesWithPromotion[basketid] = new List<SalesLineVersion2>();

            PromotionsApplication.SalesLinesWithPromotion[basketid].Add(line);

            basket2.DiscountTotal.Value += line.Discount;
            basket2.SubTotal.Value += line.Quantity * line.UnitPrice;
            basket2.Total.Value += (line.Quantity * line.UnitPrice) - line.Discount;

            basket.Total.Value = basket2.Total;

            ApplyPromotionSuccess.SalesLine = line;
            ApplyPromotionSuccess.Promotion = Promotion;
            ApplyPromotionSuccess.Basket = basket2;
            ApplyPromotionSuccess.Raise();
        }
    }

    public partial class FindBasketPromAction
    {
        public override void Execute()
        {
            var basketid = Basket.BasketId.Value;
            
            PromotionsApplication.SalesPromotions[basketid] =
                (from p in PromotionsApplication.Promotions.Values.SelectMany(p => p)
                 from l in PromotionsApplication.SalesLines[basketid]
                 where p.Barcode.Value == l.Barcode.Value
                 select p).
                    GroupBy(p => p.PromotionCode.Value).
                    Select(grp => grp.First()).
                    OrderBy(p => p.Priority.Value).
                    ToList();

            FindBasketPromSuccess.Basket = Basket;
            FindBasketPromSuccess.Raise();
        }
    }
    
    public partial class ApplyBasketPromAction
    {
        public override void Execute()
        {
            var basketid = Basket.BasketId.Value;
            var basket = PromotionsApplication.Baskets[basketid];
            var basket2 = PromotionsApplication.BasketsWithPromotion[basketid];
            
            PromotionsApplication.SalesLinesWithPromotion[basketid] = new List<SalesLineVersion2>();
            
            var buffer =
                (from l in PromotionsApplication.SalesLines[basketid].ToList()
                 group l by new { l.Barcode.Value }
                 into grp
                 select grp).Select(
                    grp =>
                    {
                        var ret = new SalesLineVersion2();
                        var first = grp.First();
                        ret.BasketId.Value = first.BasketId;
                        ret.Barcode.Value = first.Barcode;
                        ret.Description.Value = first.Description;
                        ret.SalesLineId.Value = Guid.Empty;
                        ret.Quantity.Value = grp.Sum(l => l.Quantity);
                        ret.UnitPrice.Value = grp.Sum(l => l.Quantity * l.UnitPrice) / ret.Quantity;
                        return ret;
                    }).ToList();

            basket2.DiscountTotal.Value = 0;
            basket2.SubTotal.Value = 0;
            basket2.Total.Value = 0;

            foreach(var promo in PromotionsApplication.SalesPromotions[basketid])
            {
                foreach (var line in buffer.ToList())
                {
                    if (!string.IsNullOrEmpty(line.PromotionCode.Value)) continue;

                    if(!string.IsNullOrEmpty(promo.BenefitCode))
                    {
                        if (!PromotionsApplication.SalesCustomerCodes.ContainsKey(basketid))
                            continue;

                        var customercode = PromotionsApplication.SalesCustomerCodes[basketid];

                        if (!PromotionsApplication.CustomerBenefits.ContainsKey(customercode))
                            continue;

                        if (!PromotionsApplication.CustomerBenefits[customercode].Any(b => b == promo.BenefitCode.Value))
                            continue;
                    }
                    
                    if (promo.Type.Value == 0)
                    {
                        var takexpayy = PromotionsApplication.TakeXPayYPromotions[promo];
                        
                        if(line.Quantity / takexpayy.TakeQuantity >= 1)
                        {
                            if(line.Quantity % takexpayy.TakeQuantity > 0)
                            {
                                var newline = new SalesLineVersion2();

                                newline.BasketId.Value = line.BasketId;
                                newline.Barcode.Value = line.Barcode;
                                newline.Description.Value = line.Description;
                                newline.SalesLineId.Value = Guid.Empty;
                                newline.Quantity.Value = line.Quantity.Value % takexpayy.TakeQuantity.Value;
                                newline.UnitPrice.Value = line.UnitPrice;
                                
                                buffer.Add(newline);

                                line.Quantity.Value =  ((int)line.Quantity / takexpayy.TakeQuantity) * takexpayy.TakeQuantity;
                            }

                            line.Discount.Value = takexpayy.OfferQuantity * line.UnitPrice;
                            line.PromotionCode.Value = promo.PromotionCode;

                            PromotionsApplication.SalesLinesWithPromotion[basketid].Add(line);

                            basket2.DiscountTotal.Value += line.Discount;
                            basket2.SubTotal.Value += line.Quantity * line.UnitPrice;
                            basket2.Total.Value += (line.Quantity * line.UnitPrice) - line.Discount;
                        }
                    }
                    else if (promo.Type.Value >= 1 && promo.Type.Value <= 3)
                    {
                        line.Discount.Value = line.Quantity * line.UnitPrice * (promo.Discount / 100.0M);
                        line.PromotionCode.Value = promo.PromotionCode;

                        PromotionsApplication.SalesLinesWithPromotion[basketid].Add(line);

                        basket2.DiscountTotal.Value += line.Discount;
                        basket2.SubTotal.Value += line.Quantity * line.UnitPrice;
                        basket2.Total.Value += (line.Quantity * line.UnitPrice) - line.Discount;
                    }
                }
            }

            foreach(var line in buffer.Where(b => string.IsNullOrEmpty(b.PromotionCode)))
            {
                basket2.DiscountTotal.Value += 0;
                basket2.SubTotal.Value += line.Quantity * line.UnitPrice;
                basket2.Total.Value += (line.Quantity * line.UnitPrice) - line.Discount;
            }

            basket.Total.Value = basket2.Total;

            ApplyBasketPromSuccess.Raise();
        }
    } 

    public partial class SetCustomerAction
    {
        public override void Execute()
        {
            var basketid = Basket.BasketId.Value;
            PromotionsApplication.SalesCustomerCodes[basketid] = Customer.Barcode.Value;

            SetCustomerSuccess.Raise();
        }
    }

    public partial class PromotionsApplication
    {
        internal static Dictionary<Guid, Basket> Baskets = new Dictionary<Guid, Basket>();
        internal static Dictionary<Guid, BasketVersion2> BasketsWithPromotion = new Dictionary<Guid, BasketVersion2>();
        internal static Dictionary<Guid, List<SalesLine>> SalesLines = new Dictionary<Guid, List<SalesLine>>();
        internal static Dictionary<Guid, List<SalesLineVersion2>> SalesLinesWithPromotion = new Dictionary<Guid, List<SalesLineVersion2>>();
        internal static Dictionary<Guid, Payment> Payments = new Dictionary<Guid, Payment>();
        internal static Dictionary<string, List<PromotionVersion3>> Promotions = new Dictionary<string, List<PromotionVersion3>>();
        internal static Dictionary<Guid, List<PromotionVersion3>> SalesPromotions = new Dictionary<Guid, List<PromotionVersion3>>();
        internal static Dictionary<PromotionVersion3, TakeXPayYPromotion> TakeXPayYPromotions = new Dictionary<PromotionVersion3, TakeXPayYPromotion>();
        internal static Dictionary<Guid, string> SalesCustomerCodes = new Dictionary<Guid, string>();
        internal static Dictionary<string, List<string>> CustomerBenefits = new Dictionary<string, List<string>>();

        static PromotionsApplication()
        {
            var promo = new PromotionVersion3();

            promo.PromotionCode.Value = "00001";
            promo.Barcode.Value = "123";
            promo.Type.Value = 1;
            promo.Discount.Value = 30.0M;
            promo.Priority.Value = 99;
            promo.BenefitCode.Value = "001";

            var promobasket = new PromotionVersion3();

            promobasket.PromotionCode.Value = "00002";
            promobasket.Barcode.Value = "123";
            promobasket.Type.Value = 0;
            promobasket.Discount.Value = 0;
            promobasket.Priority.Value = 1;

            var takexpayy = new TakeXPayYPromotion();

            takexpayy.PromotionCode.Value = "00002";
            takexpayy.TakeQuantity.Value = 3;
            takexpayy.OfferQuantity.Value = 1;

            Promotions[promo.Barcode] = new List<PromotionVersion3>();
            Promotions[promo.Barcode].Add(promo);
            Promotions[promo.Barcode].Add(promobasket);

            TakeXPayYPromotions[promobasket] = takexpayy;

            CustomerBenefits["12345"] = new List<string>(new string[] { "001" });
        }
    }
}
