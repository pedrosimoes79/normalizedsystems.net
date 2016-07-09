using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using NormalizedSystems.Net;

namespace RetailPromotionsAndLoyalty
{
    [Serializable]
	public partial class BasketId : NormalizedSystems.Net.FieldElement<Guid>
	{	
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "BasketId", Version = 1 };

	}

    [Serializable]
	public partial class Barcode : NormalizedSystems.Net.FieldElement<string>
	{	
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "Barcode", Version = 1 };

	}

    [Serializable]
	public partial class Description : NormalizedSystems.Net.FieldElement<string>
	{	
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "Description", Version = 1 };

	}

    [Serializable]
	public partial class Quantity : NormalizedSystems.Net.FieldElement<decimal>
	{	
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "Quantity", Version = 1 };

	}

    [Serializable]
	public partial class UnitPrice : NormalizedSystems.Net.FieldElement<decimal>
	{	
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "UnitPrice", Version = 1 };

	}

    [Serializable]
	public partial class Total : NormalizedSystems.Net.FieldElement<decimal>
	{	
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "Total", Version = 1 };

	}

    [Serializable]
	public partial class Code : NormalizedSystems.Net.FieldElement<string>
	{	
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "Code", Version = 1 };

	}

    [Serializable]
	public partial class Value : NormalizedSystems.Net.FieldElement<decimal>
	{	
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "Value", Version = 1 };

	}

    [Serializable]
	public partial class Discount : NormalizedSystems.Net.FieldElement<decimal>
	{	
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "Discount", Version = 1 };

	}

    [Serializable]
	public partial class Type : NormalizedSystems.Net.FieldElement<int>
	{	
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "Type", Version = 1 };

	}

    [Serializable]
	public partial class PromotionCode : NormalizedSystems.Net.FieldElement<string>
	{	
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "PromotionCode", Version = 1 };

	}

    [Serializable]
	public partial class SubTotal : NormalizedSystems.Net.FieldElement<decimal>
	{	
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "SubTotal", Version = 1 };

	}

    [Serializable]
	public partial class DiscountTotal : NormalizedSystems.Net.FieldElement<decimal>
	{	
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "DiscountTotal", Version = 1 };

	}

    [Serializable]
	public partial class SalesLineId : NormalizedSystems.Net.FieldElement<Guid>
	{	
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "SalesLineId", Version = 1 };

	}

    [Serializable]
	public partial class Priority : NormalizedSystems.Net.FieldElement<int>
	{	
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "Priority", Version = 1 };

	}

    [Serializable]
	public partial class TakeQuantity : NormalizedSystems.Net.FieldElement<int>
	{	
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "TakeQuantity", Version = 1 };

	}

    [Serializable]
	public partial class OfferQuantity : NormalizedSystems.Net.FieldElement<int>
	{	
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "OfferQuantity", Version = 1 };

	}

    [Serializable]
	public partial class BenefitCode : NormalizedSystems.Net.FieldElement<string>
	{	
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "BenefitCode", Version = 1 };

	}

    [Serializable]
	public partial class Basket : NormalizedSystems.Net.DataElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "Basket", Version = 1 };

		public BasketId BasketId
        {
            get
            {
                return Fields["BasketId"].Cast<BasketId>();
            }
            set
            {
				Fields["BasketId"] = value;
            }
        }

		public Total Total
        {
            get
            {
                return Fields["Total"].Cast<Total>();
            }
            set
            {
				Fields["Total"] = value;
            }
        }

		public Basket()
        {
            Fields["BasketId"] = new BasketId();
            Fields["Total"] = new Total();
        }

	}

    [Serializable]
	public partial class SalesLine : NormalizedSystems.Net.DataElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "SalesLine", Version = 1 };

		public BasketId BasketId
        {
            get
            {
                return Fields["BasketId"].Cast<BasketId>();
            }
            set
            {
				Fields["BasketId"] = value;
            }
        }

		public SalesLineId SalesLineId
        {
            get
            {
                return Fields["SalesLineId"].Cast<SalesLineId>();
            }
            set
            {
				Fields["SalesLineId"] = value;
            }
        }

		public Barcode Barcode
        {
            get
            {
                return Fields["Barcode"].Cast<Barcode>();
            }
            set
            {
				Fields["Barcode"] = value;
            }
        }

		public Description Description
        {
            get
            {
                return Fields["Description"].Cast<Description>();
            }
            set
            {
				Fields["Description"] = value;
            }
        }

		public Quantity Quantity
        {
            get
            {
                return Fields["Quantity"].Cast<Quantity>();
            }
            set
            {
				Fields["Quantity"] = value;
            }
        }

		public UnitPrice UnitPrice
        {
            get
            {
                return Fields["UnitPrice"].Cast<UnitPrice>();
            }
            set
            {
				Fields["UnitPrice"] = value;
            }
        }

		public SalesLine()
        {
            Fields["BasketId"] = new BasketId();
            Fields["SalesLineId"] = new SalesLineId();
            Fields["Barcode"] = new Barcode();
            Fields["Description"] = new Description();
            Fields["Quantity"] = new Quantity();
            Fields["UnitPrice"] = new UnitPrice();
        }

	}

    [Serializable]
	public partial class Payment : NormalizedSystems.Net.DataElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "Payment", Version = 1 };

		public BasketId BasketId
        {
            get
            {
                return Fields["BasketId"].Cast<BasketId>();
            }
            set
            {
				Fields["BasketId"] = value;
            }
        }

		public Code Code
        {
            get
            {
                return Fields["Code"].Cast<Code>();
            }
            set
            {
				Fields["Code"] = value;
            }
        }

		public Description Description
        {
            get
            {
                return Fields["Description"].Cast<Description>();
            }
            set
            {
				Fields["Description"] = value;
            }
        }

		public Value Value
        {
            get
            {
                return Fields["Value"].Cast<Value>();
            }
            set
            {
				Fields["Value"] = value;
            }
        }

		public Payment()
        {
            Fields["BasketId"] = new BasketId();
            Fields["Code"] = new Code();
            Fields["Description"] = new Description();
            Fields["Value"] = new Value();
        }

	}

    [Serializable]
	public partial class Promotion : NormalizedSystems.Net.DataElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "Promotion", Version = 1 };

		public PromotionCode PromotionCode
        {
            get
            {
                return Fields["PromotionCode"].Cast<PromotionCode>();
            }
            set
            {
				Fields["PromotionCode"] = value;
            }
        }

		public Barcode Barcode
        {
            get
            {
                return Fields["Barcode"].Cast<Barcode>();
            }
            set
            {
				Fields["Barcode"] = value;
            }
        }

		public Type Type
        {
            get
            {
                return Fields["Type"].Cast<Type>();
            }
            set
            {
				Fields["Type"] = value;
            }
        }

		public Discount Discount
        {
            get
            {
                return Fields["Discount"].Cast<Discount>();
            }
            set
            {
				Fields["Discount"] = value;
            }
        }

		public Promotion()
        {
            Fields["PromotionCode"] = new PromotionCode();
            Fields["Barcode"] = new Barcode();
            Fields["Type"] = new Type();
            Fields["Discount"] = new Discount();
        }

	}

    [Serializable]
	public partial class SalesLineVersion2 : NormalizedSystems.Net.DataElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "SalesLine", Version = 2 };

		public BasketId BasketId
        {
            get
            {
                return Fields["BasketId"].Cast<BasketId>();
            }
            set
            {
				Fields["BasketId"] = value;
            }
        }

		public SalesLineId SalesLineId
        {
            get
            {
                return Fields["SalesLineId"].Cast<SalesLineId>();
            }
            set
            {
				Fields["SalesLineId"] = value;
            }
        }

		public Barcode Barcode
        {
            get
            {
                return Fields["Barcode"].Cast<Barcode>();
            }
            set
            {
				Fields["Barcode"] = value;
            }
        }

		public Description Description
        {
            get
            {
                return Fields["Description"].Cast<Description>();
            }
            set
            {
				Fields["Description"] = value;
            }
        }

		public Quantity Quantity
        {
            get
            {
                return Fields["Quantity"].Cast<Quantity>();
            }
            set
            {
				Fields["Quantity"] = value;
            }
        }

		public UnitPrice UnitPrice
        {
            get
            {
                return Fields["UnitPrice"].Cast<UnitPrice>();
            }
            set
            {
				Fields["UnitPrice"] = value;
            }
        }

		public PromotionCode PromotionCode
        {
            get
            {
                return Fields["PromotionCode"].Cast<PromotionCode>();
            }
            set
            {
				Fields["PromotionCode"] = value;
            }
        }

		public Discount Discount
        {
            get
            {
                return Fields["Discount"].Cast<Discount>();
            }
            set
            {
				Fields["Discount"] = value;
            }
        }

		public SalesLineVersion2()
        {
            Fields["BasketId"] = new BasketId();
            Fields["SalesLineId"] = new SalesLineId();
            Fields["Barcode"] = new Barcode();
            Fields["Description"] = new Description();
            Fields["Quantity"] = new Quantity();
            Fields["UnitPrice"] = new UnitPrice();
            Fields["PromotionCode"] = new PromotionCode();
            Fields["Discount"] = new Discount();
        }

		public static implicit operator SalesLine(SalesLineVersion2 obj)
        {
            var ret = new SalesLine();
			obj.Convert(ret);
			return ret;
        }

	}

    [Serializable]
	public partial class BasketVersion2 : NormalizedSystems.Net.DataElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "Basket", Version = 2 };

		public BasketId BasketId
        {
            get
            {
                return Fields["BasketId"].Cast<BasketId>();
            }
            set
            {
				Fields["BasketId"] = value;
            }
        }

		public Total Total
        {
            get
            {
                return Fields["Total"].Cast<Total>();
            }
            set
            {
				Fields["Total"] = value;
            }
        }

		public SubTotal SubTotal
        {
            get
            {
                return Fields["SubTotal"].Cast<SubTotal>();
            }
            set
            {
				Fields["SubTotal"] = value;
            }
        }

		public DiscountTotal DiscountTotal
        {
            get
            {
                return Fields["DiscountTotal"].Cast<DiscountTotal>();
            }
            set
            {
				Fields["DiscountTotal"] = value;
            }
        }

		public BasketVersion2()
        {
            Fields["BasketId"] = new BasketId();
            Fields["Total"] = new Total();
            Fields["SubTotal"] = new SubTotal();
            Fields["DiscountTotal"] = new DiscountTotal();
        }

		public static implicit operator Basket(BasketVersion2 obj)
        {
            var ret = new Basket();
			obj.Convert(ret);
			return ret;
        }

	}

    [Serializable]
	public partial class PromotionVersion2 : NormalizedSystems.Net.DataElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "Promotion", Version = 2 };

		public PromotionCode PromotionCode
        {
            get
            {
                return Fields["PromotionCode"].Cast<PromotionCode>();
            }
            set
            {
				Fields["PromotionCode"] = value;
            }
        }

		public Barcode Barcode
        {
            get
            {
                return Fields["Barcode"].Cast<Barcode>();
            }
            set
            {
				Fields["Barcode"] = value;
            }
        }

		public Type Type
        {
            get
            {
                return Fields["Type"].Cast<Type>();
            }
            set
            {
				Fields["Type"] = value;
            }
        }

		public Discount Discount
        {
            get
            {
                return Fields["Discount"].Cast<Discount>();
            }
            set
            {
				Fields["Discount"] = value;
            }
        }

		public Priority Priority
        {
            get
            {
                return Fields["Priority"].Cast<Priority>();
            }
            set
            {
				Fields["Priority"] = value;
            }
        }

		public PromotionVersion2()
        {
            Fields["PromotionCode"] = new PromotionCode();
            Fields["Barcode"] = new Barcode();
            Fields["Type"] = new Type();
            Fields["Discount"] = new Discount();
            Fields["Priority"] = new Priority();
        }

		public static implicit operator Promotion(PromotionVersion2 obj)
        {
            var ret = new Promotion();
			obj.Convert(ret);
			return ret;
        }

	}

    [Serializable]
	public partial class DirectDiscountPromotion : NormalizedSystems.Net.DataElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "DirectDiscountPromotion", Version = 1 };

		public PromotionCode PromotionCode
        {
            get
            {
                return Fields["PromotionCode"].Cast<PromotionCode>();
            }
            set
            {
				Fields["PromotionCode"] = value;
            }
        }

		public Type Type
        {
            get
            {
                return Fields["Type"].Cast<Type>();
            }
            set
            {
				Fields["Type"] = value;
            }
        }

		public Discount Discount
        {
            get
            {
                return Fields["Discount"].Cast<Discount>();
            }
            set
            {
				Fields["Discount"] = value;
            }
        }

		public DirectDiscountPromotion()
        {
            Fields["PromotionCode"] = new PromotionCode();
            Fields["Type"] = new Type();
            Fields["Discount"] = new Discount();
        }

	}

    [Serializable]
	public partial class TakeXPayYPromotion : NormalizedSystems.Net.DataElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "TakeXPayYPromotion", Version = 1 };

		public PromotionCode PromotionCode
        {
            get
            {
                return Fields["PromotionCode"].Cast<PromotionCode>();
            }
            set
            {
				Fields["PromotionCode"] = value;
            }
        }

		public TakeQuantity TakeQuantity
        {
            get
            {
                return Fields["TakeQuantity"].Cast<TakeQuantity>();
            }
            set
            {
				Fields["TakeQuantity"] = value;
            }
        }

		public OfferQuantity OfferQuantity
        {
            get
            {
                return Fields["OfferQuantity"].Cast<OfferQuantity>();
            }
            set
            {
				Fields["OfferQuantity"] = value;
            }
        }

		public TakeXPayYPromotion()
        {
            Fields["PromotionCode"] = new PromotionCode();
            Fields["TakeQuantity"] = new TakeQuantity();
            Fields["OfferQuantity"] = new OfferQuantity();
        }

	}

    [Serializable]
	public partial class PromotionVersion3 : NormalizedSystems.Net.DataElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "Promotion", Version = 3 };

		public PromotionCode PromotionCode
        {
            get
            {
                return Fields["PromotionCode"].Cast<PromotionCode>();
            }
            set
            {
				Fields["PromotionCode"] = value;
            }
        }

		public Barcode Barcode
        {
            get
            {
                return Fields["Barcode"].Cast<Barcode>();
            }
            set
            {
				Fields["Barcode"] = value;
            }
        }

		public Type Type
        {
            get
            {
                return Fields["Type"].Cast<Type>();
            }
            set
            {
				Fields["Type"] = value;
            }
        }

		public Discount Discount
        {
            get
            {
                return Fields["Discount"].Cast<Discount>();
            }
            set
            {
				Fields["Discount"] = value;
            }
        }

		public Priority Priority
        {
            get
            {
                return Fields["Priority"].Cast<Priority>();
            }
            set
            {
				Fields["Priority"] = value;
            }
        }

		public BenefitCode BenefitCode
        {
            get
            {
                return Fields["BenefitCode"].Cast<BenefitCode>();
            }
            set
            {
				Fields["BenefitCode"] = value;
            }
        }

		public PromotionVersion3()
        {
            Fields["PromotionCode"] = new PromotionCode();
            Fields["Barcode"] = new Barcode();
            Fields["Type"] = new Type();
            Fields["Discount"] = new Discount();
            Fields["Priority"] = new Priority();
            Fields["BenefitCode"] = new BenefitCode();
        }

		public static implicit operator PromotionVersion2(PromotionVersion3 obj)
        {
            var ret = new PromotionVersion2();
			obj.Convert(ret);
			return ret;
        }

		public static implicit operator Promotion(PromotionVersion3 obj)
        {
				return (Promotion)(PromotionVersion2)obj;
        }

	}

    [Serializable]
	public partial class Customer : NormalizedSystems.Net.DataElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "Customer", Version = 1 };

		public Barcode Barcode
        {
            get
            {
                return Fields["Barcode"].Cast<Barcode>();
            }
            set
            {
				Fields["Barcode"] = value;
            }
        }

		public Customer()
        {
            Fields["Barcode"] = new Barcode();
        }

	}

    [Serializable]
	public partial class Benefit : NormalizedSystems.Net.DataElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "Benefit", Version = 1 };

		public BenefitCode BenefitCode
        {
            get
            {
                return Fields["BenefitCode"].Cast<BenefitCode>();
            }
            set
            {
				Fields["BenefitCode"] = value;
            }
        }

		public Benefit()
        {
            Fields["BenefitCode"] = new BenefitCode();
        }

	}

	public partial class BeginSaleEvent : NormalizedSystems.Net.EventElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "BeginSaleEvent", Version = 1 };


		public BeginSaleEvent()
        {
        }

	}

	public partial class BeginSaleSuccess : NormalizedSystems.Net.EventElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "BeginSaleSuccess", Version = 1 };

		public Basket Basket
        {
            get
            {
                return ContentData["Basket"].Cast<Basket>();
            }
            set
            {
				ContentData["Basket"] = value;
            }
        }


		public BeginSaleSuccess()
        {
            ContentData["Basket"] = new Basket();
        }

	}

	public partial class AddProductEvent : NormalizedSystems.Net.EventElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AddProductEvent", Version = 1 };

		public SalesLine SalesLine
        {
            get
            {
                return ContentData["SalesLine"].Cast<SalesLine>();
            }
            set
            {
				ContentData["SalesLine"] = value;
            }
        }


		public AddProductEvent()
        {
            ContentData["SalesLine"] = new SalesLine();
        }

	}

	public partial class AddProductSuccess : NormalizedSystems.Net.EventElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AddProductSuccess", Version = 1 };

		public SalesLine SalesLine
        {
            get
            {
                return ContentData["SalesLine"].Cast<SalesLine>();
            }
            set
            {
				ContentData["SalesLine"] = value;
            }
        }


		public AddProductSuccess()
        {
            ContentData["SalesLine"] = new SalesLine();
        }

	}

	public partial class RequestTotalEvent : NormalizedSystems.Net.EventElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "RequestTotalEvent", Version = 1 };

		public Basket Basket
        {
            get
            {
                return ContentData["Basket"].Cast<Basket>();
            }
            set
            {
				ContentData["Basket"] = value;
            }
        }


		public RequestTotalEvent()
        {
            ContentData["Basket"] = new Basket();
        }

	}

	public partial class RequestTotalSuccess : NormalizedSystems.Net.EventElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "RequestTotalSuccess", Version = 1 };

		public Basket Basket
        {
            get
            {
                return ContentData["Basket"].Cast<Basket>();
            }
            set
            {
				ContentData["Basket"] = value;
            }
        }


		public RequestTotalSuccess()
        {
            ContentData["Basket"] = new Basket();
        }

	}

	public partial class AddPaymentEvent : NormalizedSystems.Net.EventElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AddPaymentEvent", Version = 1 };

		public Payment Payment
        {
            get
            {
                return ContentData["Payment"].Cast<Payment>();
            }
            set
            {
				ContentData["Payment"] = value;
            }
        }


		public AddPaymentEvent()
        {
            ContentData["Payment"] = new Payment();
        }

	}

	public partial class AddPaymentSuccess : NormalizedSystems.Net.EventElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AddPaymentSuccess", Version = 1 };

		public Payment Payment
        {
            get
            {
                return ContentData["Payment"].Cast<Payment>();
            }
            set
            {
				ContentData["Payment"] = value;
            }
        }


		public AddPaymentSuccess()
        {
            ContentData["Payment"] = new Payment();
        }

	}

	public partial class EndSaleEvent : NormalizedSystems.Net.EventElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "EndSaleEvent", Version = 1 };

		public Basket Basket
        {
            get
            {
                return ContentData["Basket"].Cast<Basket>();
            }
            set
            {
				ContentData["Basket"] = value;
            }
        }


		public EndSaleEvent()
        {
            ContentData["Basket"] = new Basket();
        }

	}

	public partial class EndSaleSuccess : NormalizedSystems.Net.EventElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "EndSaleSuccess", Version = 1 };


		public EndSaleSuccess()
        {
        }

	}

	public partial class FindPromotionSuccess : NormalizedSystems.Net.EventElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "FindPromotionSuccess", Version = 1 };

		public SalesLine SalesLine
        {
            get
            {
                return ContentData["SalesLine"].Cast<SalesLine>();
            }
            set
            {
				ContentData["SalesLine"] = value;
            }
        }

		public Promotion Promotion
        {
            get
            {
                return ContentData["Promotion"].Cast<Promotion>();
            }
            set
            {
				ContentData["Promotion"] = value;
            }
        }


		public FindPromotionSuccess()
        {
            ContentData["SalesLine"] = new SalesLine();
            ContentData["Promotion"] = new Promotion();
        }

	}

	public partial class ApplyPromotionSuccess : NormalizedSystems.Net.EventElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "ApplyPromotionSuccess", Version = 1 };

		public SalesLineVersion2 SalesLine
        {
            get
            {
                return ContentData["SalesLine"].Cast<SalesLineVersion2>();
            }
            set
            {
				ContentData["SalesLine"] = value;
            }
        }

		public Promotion Promotion
        {
            get
            {
                return ContentData["Promotion"].Cast<Promotion>();
            }
            set
            {
				ContentData["Promotion"] = value;
            }
        }

		public BasketVersion2 Basket
        {
            get
            {
                return ContentData["Basket"].Cast<BasketVersion2>();
            }
            set
            {
				ContentData["Basket"] = value;
            }
        }


		public ApplyPromotionSuccess()
        {
            ContentData["SalesLine"] = new SalesLineVersion2();
            ContentData["Promotion"] = new Promotion();
            ContentData["Basket"] = new BasketVersion2();
        }

	}

	public partial class FindBasketPromSuccess : NormalizedSystems.Net.EventElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "FindBasketPromSuccess", Version = 1 };

		public Basket Basket
        {
            get
            {
                return ContentData["Basket"].Cast<Basket>();
            }
            set
            {
				ContentData["Basket"] = value;
            }
        }


		public FindBasketPromSuccess()
        {
            ContentData["Basket"] = new Basket();
        }

	}

	public partial class ApplyBasketPromSuccess : NormalizedSystems.Net.EventElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "ApplyBasketPromSuccess", Version = 1 };


		public ApplyBasketPromSuccess()
        {
        }

	}

	public partial class SetCustomerEvent : NormalizedSystems.Net.EventElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "SetCustomerEvent", Version = 1 };

		public Basket Basket
        {
            get
            {
                return ContentData["Basket"].Cast<Basket>();
            }
            set
            {
				ContentData["Basket"] = value;
            }
        }

		public Customer Customer
        {
            get
            {
                return ContentData["Customer"].Cast<Customer>();
            }
            set
            {
				ContentData["Customer"] = value;
            }
        }


		public SetCustomerEvent()
        {
            ContentData["Basket"] = new Basket();
            ContentData["Customer"] = new Customer();
        }

	}

	public partial class SetCustomerSuccess : NormalizedSystems.Net.EventElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "SetCustomerSuccess", Version = 1 };


		public SetCustomerSuccess()
        {
        }

	}

	public partial class BeginSaleAction : NormalizedSystems.Net.ActionElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "BeginSaleAction", Version = 1 };

		public BeginSaleSuccess BeginSaleSuccess
        {
            get
            {
                return OutputEvents["BeginSaleSuccess"].Cast<BeginSaleSuccess>();
            }
            set
            {
				OutputEvents["BeginSaleSuccess"] = value;
            }
        }


		public BeginSaleAction()
        {
            OutputEvents["BeginSaleSuccess"] = new BeginSaleSuccess();
        }
	}

	public partial class AddProductAction : NormalizedSystems.Net.ActionElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AddProductAction", Version = 1 };

		public SalesLine SalesLine
        {
            get
            {
                return InputData["SalesLine"].Cast<SalesLine>();
            }
            set
            {
				InputData["SalesLine"] = value;
            }
        }

		public AddProductSuccess AddProductSuccess
        {
            get
            {
                return OutputEvents["AddProductSuccess"].Cast<AddProductSuccess>();
            }
            set
            {
				OutputEvents["AddProductSuccess"] = value;
            }
        }


		public AddProductAction()
        {
            InputData["SalesLine"] = new SalesLine();
            OutputEvents["AddProductSuccess"] = new AddProductSuccess();
        }
	}

	public partial class RequestTotalAction : NormalizedSystems.Net.ActionElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "RequestTotalAction", Version = 1 };

		public Basket Basket
        {
            get
            {
                return InputData["Basket"].Cast<Basket>();
            }
            set
            {
				InputData["Basket"] = value;
            }
        }

		public RequestTotalSuccess RequestTotalSuccess
        {
            get
            {
                return OutputEvents["RequestTotalSuccess"].Cast<RequestTotalSuccess>();
            }
            set
            {
				OutputEvents["RequestTotalSuccess"] = value;
            }
        }


		public RequestTotalAction()
        {
            InputData["Basket"] = new Basket();
            OutputEvents["RequestTotalSuccess"] = new RequestTotalSuccess();
        }
	}

	public partial class AddPaymentAction : NormalizedSystems.Net.ActionElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AddPaymentAction", Version = 1 };

		public Payment Payment
        {
            get
            {
                return InputData["Payment"].Cast<Payment>();
            }
            set
            {
				InputData["Payment"] = value;
            }
        }

		public AddPaymentSuccess AddPaymentSuccess
        {
            get
            {
                return OutputEvents["AddPaymentSuccess"].Cast<AddPaymentSuccess>();
            }
            set
            {
				OutputEvents["AddPaymentSuccess"] = value;
            }
        }


		public AddPaymentAction()
        {
            InputData["Payment"] = new Payment();
            OutputEvents["AddPaymentSuccess"] = new AddPaymentSuccess();
        }
	}

	public partial class EndSaleAction : NormalizedSystems.Net.ActionElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "EndSaleAction", Version = 1 };

		public Basket Basket
        {
            get
            {
                return InputData["Basket"].Cast<Basket>();
            }
            set
            {
				InputData["Basket"] = value;
            }
        }

		public EndSaleSuccess EndSaleSuccess
        {
            get
            {
                return OutputEvents["EndSaleSuccess"].Cast<EndSaleSuccess>();
            }
            set
            {
				OutputEvents["EndSaleSuccess"] = value;
            }
        }


		public EndSaleAction()
        {
            InputData["Basket"] = new Basket();
            OutputEvents["EndSaleSuccess"] = new EndSaleSuccess();
        }
	}

	public partial class FindPromotionAction : NormalizedSystems.Net.ActionElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "FindPromotionAction", Version = 1 };

		public SalesLine SalesLine
        {
            get
            {
                return InputData["SalesLine"].Cast<SalesLine>();
            }
            set
            {
				InputData["SalesLine"] = value;
            }
        }

		public FindPromotionSuccess FindPromotionSuccess
        {
            get
            {
                return OutputEvents["FindPromotionSuccess"].Cast<FindPromotionSuccess>();
            }
            set
            {
				OutputEvents["FindPromotionSuccess"] = value;
            }
        }


		public FindPromotionAction()
        {
            InputData["SalesLine"] = new SalesLine();
            OutputEvents["FindPromotionSuccess"] = new FindPromotionSuccess();
        }
	}

	public partial class ApplyPromotionAction : NormalizedSystems.Net.ActionElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "ApplyPromotionAction", Version = 1 };

		public SalesLine SalesLine
        {
            get
            {
                return InputData["SalesLine"].Cast<SalesLine>();
            }
            set
            {
				InputData["SalesLine"] = value;
            }
        }

		public Promotion Promotion
        {
            get
            {
                return InputData["Promotion"].Cast<Promotion>();
            }
            set
            {
				InputData["Promotion"] = value;
            }
        }

		public ApplyPromotionSuccess ApplyPromotionSuccess
        {
            get
            {
                return OutputEvents["ApplyPromotionSuccess"].Cast<ApplyPromotionSuccess>();
            }
            set
            {
				OutputEvents["ApplyPromotionSuccess"] = value;
            }
        }


		public ApplyPromotionAction()
        {
            InputData["SalesLine"] = new SalesLine();
            InputData["Promotion"] = new Promotion();
            OutputEvents["ApplyPromotionSuccess"] = new ApplyPromotionSuccess();
        }
	}

	public partial class FindBasketPromAction : NormalizedSystems.Net.ActionElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "FindBasketPromAction", Version = 1 };

		public Basket Basket
        {
            get
            {
                return InputData["Basket"].Cast<Basket>();
            }
            set
            {
				InputData["Basket"] = value;
            }
        }

		public FindBasketPromSuccess FindBasketPromSuccess
        {
            get
            {
                return OutputEvents["FindBasketPromSuccess"].Cast<FindBasketPromSuccess>();
            }
            set
            {
				OutputEvents["FindBasketPromSuccess"] = value;
            }
        }


		public FindBasketPromAction()
        {
            InputData["Basket"] = new Basket();
            OutputEvents["FindBasketPromSuccess"] = new FindBasketPromSuccess();
        }
	}

	public partial class ApplyBasketPromAction : NormalizedSystems.Net.ActionElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "ApplyBasketPromAction", Version = 1 };

		public Basket Basket
        {
            get
            {
                return InputData["Basket"].Cast<Basket>();
            }
            set
            {
				InputData["Basket"] = value;
            }
        }

		public ApplyBasketPromSuccess ApplyBasketPromSuccess
        {
            get
            {
                return OutputEvents["ApplyBasketPromSuccess"].Cast<ApplyBasketPromSuccess>();
            }
            set
            {
				OutputEvents["ApplyBasketPromSuccess"] = value;
            }
        }


		public ApplyBasketPromAction()
        {
            InputData["Basket"] = new Basket();
            OutputEvents["ApplyBasketPromSuccess"] = new ApplyBasketPromSuccess();
        }
	}

	public partial class SetCustomerAction : NormalizedSystems.Net.ActionElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "SetCustomerAction", Version = 1 };

		public Basket Basket
        {
            get
            {
                return InputData["Basket"].Cast<Basket>();
            }
            set
            {
				InputData["Basket"] = value;
            }
        }

		public Customer Customer
        {
            get
            {
                return InputData["Customer"].Cast<Customer>();
            }
            set
            {
				InputData["Customer"] = value;
            }
        }

		public SetCustomerSuccess SetCustomerSuccess
        {
            get
            {
                return OutputEvents["SetCustomerSuccess"].Cast<SetCustomerSuccess>();
            }
            set
            {
				OutputEvents["SetCustomerSuccess"] = value;
            }
        }


		public SetCustomerAction()
        {
            InputData["Basket"] = new Basket();
            InputData["Customer"] = new Customer();
            OutputEvents["SetCustomerSuccess"] = new SetCustomerSuccess();
        }
	}

    public class BeginSaleRule : NormalizedSystems.Net.RuleElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "BeginSaleRule", Version = 1 };

		public BeginSaleEvent BeginSaleEvent
        {
            get
            {
                return Events["BeginSaleEvent"].Cast<BeginSaleEvent>();
            }
            set
            {
				Events["BeginSaleEvent"] = value;
            }
        }

		public BeginSaleAction BeginSaleAction
        {
            get
            {
                return Actions["BeginSaleAction"].Cast<BeginSaleAction>();
            }
            set
            {
				Actions["BeginSaleAction"] = value;
            }
        }


		public BeginSaleRule()
        {
            Events["BeginSaleEvent"] = new BeginSaleEvent();
            Actions["BeginSaleAction"] = new BeginSaleAction();
            LogicType = LogicType.And;
        }
	}

    public class AddProductRule : NormalizedSystems.Net.RuleElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AddProductRule", Version = 1 };

		public AddProductEvent AddProductEvent
        {
            get
            {
                return Events["AddProductEvent"].Cast<AddProductEvent>();
            }
            set
            {
				Events["AddProductEvent"] = value;
            }
        }

		public AddProductAction AddProductAction
        {
            get
            {
                return Actions["AddProductAction"].Cast<AddProductAction>();
            }
            set
            {
				Actions["AddProductAction"] = value;
            }
        }


		public AddProductRule()
        {
            Events["AddProductEvent"] = new AddProductEvent();
            Actions["AddProductAction"] = new AddProductAction();
            LogicType = LogicType.And;
        }
	}

    public class RequestTotalRule : NormalizedSystems.Net.RuleElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "RequestTotalRule", Version = 1 };

		public RequestTotalEvent RequestTotalEvent
        {
            get
            {
                return Events["RequestTotalEvent"].Cast<RequestTotalEvent>();
            }
            set
            {
				Events["RequestTotalEvent"] = value;
            }
        }

		public ApplyBasketPromSuccess ApplyBasketPromSuccess
        {
            get
            {
                return Events["ApplyBasketPromSuccess"].Cast<ApplyBasketPromSuccess>();
            }
            set
            {
				Events["ApplyBasketPromSuccess"] = value;
            }
        }

		public RequestTotalAction RequestTotalAction
        {
            get
            {
                return Actions["RequestTotalAction"].Cast<RequestTotalAction>();
            }
            set
            {
				Actions["RequestTotalAction"] = value;
            }
        }


		public RequestTotalRule()
        {
            Events["RequestTotalEvent"] = new RequestTotalEvent();
            Events["ApplyBasketPromSuccess"] = new ApplyBasketPromSuccess();
            Actions["RequestTotalAction"] = new RequestTotalAction();
            LogicType = LogicType.And;
        }
	}

    public class AddPaymentRule : NormalizedSystems.Net.RuleElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AddPaymentRule", Version = 1 };

		public AddPaymentEvent AddPaymentEvent
        {
            get
            {
                return Events["AddPaymentEvent"].Cast<AddPaymentEvent>();
            }
            set
            {
				Events["AddPaymentEvent"] = value;
            }
        }

		public AddPaymentAction AddPaymentAction
        {
            get
            {
                return Actions["AddPaymentAction"].Cast<AddPaymentAction>();
            }
            set
            {
				Actions["AddPaymentAction"] = value;
            }
        }


		public AddPaymentRule()
        {
            Events["AddPaymentEvent"] = new AddPaymentEvent();
            Actions["AddPaymentAction"] = new AddPaymentAction();
            LogicType = LogicType.And;
        }
	}

    public class EndSaleRule : NormalizedSystems.Net.RuleElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "EndSaleRule", Version = 1 };

		public EndSaleEvent EndSaleEvent
        {
            get
            {
                return Events["EndSaleEvent"].Cast<EndSaleEvent>();
            }
            set
            {
				Events["EndSaleEvent"] = value;
            }
        }

		public EndSaleAction EndSaleAction
        {
            get
            {
                return Actions["EndSaleAction"].Cast<EndSaleAction>();
            }
            set
            {
				Actions["EndSaleAction"] = value;
            }
        }


		public EndSaleRule()
        {
            Events["EndSaleEvent"] = new EndSaleEvent();
            Actions["EndSaleAction"] = new EndSaleAction();
            LogicType = LogicType.And;
        }
	}

    public class FindPromotionRule : NormalizedSystems.Net.RuleElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "FindPromotionRule", Version = 1 };

		public AddProductSuccess AddProductSuccess
        {
            get
            {
                return Events["AddProductSuccess"].Cast<AddProductSuccess>();
            }
            set
            {
				Events["AddProductSuccess"] = value;
            }
        }

		public FindPromotionAction FindPromotionAction
        {
            get
            {
                return Actions["FindPromotionAction"].Cast<FindPromotionAction>();
            }
            set
            {
				Actions["FindPromotionAction"] = value;
            }
        }


		public FindPromotionRule()
        {
            Events["AddProductSuccess"] = new AddProductSuccess();
            Actions["FindPromotionAction"] = new FindPromotionAction();
            LogicType = LogicType.And;
        }
	}

    public class ApplyPromotionRule : NormalizedSystems.Net.RuleElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "ApplyPromotionRule", Version = 1 };

		public FindPromotionSuccess FindPromotionSuccess
        {
            get
            {
                return Events["FindPromotionSuccess"].Cast<FindPromotionSuccess>();
            }
            set
            {
				Events["FindPromotionSuccess"] = value;
            }
        }

		public ApplyPromotionAction ApplyPromotionAction
        {
            get
            {
                return Actions["ApplyPromotionAction"].Cast<ApplyPromotionAction>();
            }
            set
            {
				Actions["ApplyPromotionAction"] = value;
            }
        }


		public ApplyPromotionRule()
        {
            Events["FindPromotionSuccess"] = new FindPromotionSuccess();
            Actions["ApplyPromotionAction"] = new ApplyPromotionAction();
            LogicType = LogicType.And;
        }
	}

    public class FindBasketPromRule : NormalizedSystems.Net.RuleElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "FindBasketPromRule", Version = 1 };

		public RequestTotalEvent RequestTotalEvent
        {
            get
            {
                return Events["RequestTotalEvent"].Cast<RequestTotalEvent>();
            }
            set
            {
				Events["RequestTotalEvent"] = value;
            }
        }

		public FindBasketPromAction FindBasketPromAction
        {
            get
            {
                return Actions["FindBasketPromAction"].Cast<FindBasketPromAction>();
            }
            set
            {
				Actions["FindBasketPromAction"] = value;
            }
        }


		public FindBasketPromRule()
        {
            Events["RequestTotalEvent"] = new RequestTotalEvent();
            Actions["FindBasketPromAction"] = new FindBasketPromAction();
            LogicType = LogicType.And;
        }
	}

    public class ApplyBasketPromRule : NormalizedSystems.Net.RuleElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "ApplyBasketPromRule", Version = 1 };

		public FindBasketPromSuccess FindBasketPromSuccess
        {
            get
            {
                return Events["FindBasketPromSuccess"].Cast<FindBasketPromSuccess>();
            }
            set
            {
				Events["FindBasketPromSuccess"] = value;
            }
        }

		public ApplyBasketPromAction ApplyBasketPromAction
        {
            get
            {
                return Actions["ApplyBasketPromAction"].Cast<ApplyBasketPromAction>();
            }
            set
            {
				Actions["ApplyBasketPromAction"] = value;
            }
        }


		public ApplyBasketPromRule()
        {
            Events["FindBasketPromSuccess"] = new FindBasketPromSuccess();
            Actions["ApplyBasketPromAction"] = new ApplyBasketPromAction();
            LogicType = LogicType.And;
        }
	}

    public class SetCustomerRule : NormalizedSystems.Net.RuleElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "SetCustomerRule", Version = 1 };

		public SetCustomerEvent SetCustomerEvent
        {
            get
            {
                return Events["SetCustomerEvent"].Cast<SetCustomerEvent>();
            }
            set
            {
				Events["SetCustomerEvent"] = value;
            }
        }

		public SetCustomerAction SetCustomerAction
        {
            get
            {
                return Actions["SetCustomerAction"].Cast<SetCustomerAction>();
            }
            set
            {
				Actions["SetCustomerAction"] = value;
            }
        }


		public SetCustomerRule()
        {
            Events["SetCustomerEvent"] = new SetCustomerEvent();
            Actions["SetCustomerAction"] = new SetCustomerAction();
            LogicType = LogicType.And;
        }
	}


    public partial class PromotionsApplication : NormalizedSystems.Net.Application
	{
        public PromotionsApplication()
        {
            AddRule<BeginSaleRule>();
            AddRule<AddProductRule>();
            AddRule<RequestTotalRule>();
            AddRule<AddPaymentRule>();
            AddRule<EndSaleRule>();
            AddRule<FindPromotionRule>();
            AddRule<ApplyPromotionRule>();
            AddRule<FindBasketPromRule>();
            AddRule<ApplyBasketPromRule>();
            AddRule<SetCustomerRule>();
        }
	}
}

