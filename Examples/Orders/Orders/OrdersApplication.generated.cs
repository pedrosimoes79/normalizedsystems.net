using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using NormalizedSystems.Net;

namespace Orders
{
    [Serializable]
	public partial class Description : NormalizedSystems.Net.FieldElement<string>
	{	
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "Description", Version = 1 };

	}

    [Serializable]
	public partial class Value : NormalizedSystems.Net.FieldElement<decimal>
	{	
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "Value", Version = 1 };

	}

    [Serializable]
	public partial class Order : NormalizedSystems.Net.DataElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "Order", Version = 1 };

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

		public Order()
        {
            Fields["Description"] = new Description();
            Fields["Value"] = new Value();
        }

	}

	public partial class FirstApproveEvent : NormalizedSystems.Net.EventElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "FirstApproveEvent", Version = 1 };

		public Order Order
        {
            get
            {
                return ContentData["Order"].Cast<Order>();
            }
            set
            {
				ContentData["Order"] = value;
            }
        }


		public FirstApproveEvent()
        {
            ContentData["Order"] = new Order();
        }

	}

	public partial class SecondApproveEvent : NormalizedSystems.Net.EventElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "SecondApproveEvent", Version = 1 };

		public Order Order
        {
            get
            {
                return ContentData["Order"].Cast<Order>();
            }
            set
            {
				ContentData["Order"] = value;
            }
        }


		public SecondApproveEvent()
        {
            ContentData["Order"] = new Order();
        }

	}

	public partial class NewOrderEvent : NormalizedSystems.Net.EventElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "NewOrderEvent", Version = 1 };

		public Order Order
        {
            get
            {
                return ContentData["Order"].Cast<Order>();
            }
            set
            {
				ContentData["Order"] = value;
            }
        }


		public NewOrderEvent()
        {
            ContentData["Order"] = new Order();
        }

	}

	public partial class NewOrderAction : NormalizedSystems.Net.ActionElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "NewOrderAction", Version = 1 };

		public Order Order
        {
            get
            {
                return InputData["Order"].Cast<Order>();
            }
            set
            {
				InputData["Order"] = value;
            }
        }

		public FirstApproveEvent FirstApproveEvent
        {
            get
            {
                return OutputEvents["FirstApproveEvent"].Cast<FirstApproveEvent>();
            }
            set
            {
				OutputEvents["FirstApproveEvent"] = value;
            }
        }


		public NewOrderAction()
        {
            InputData["Order"] = new Order();
            OutputEvents["FirstApproveEvent"] = new FirstApproveEvent();
        }
	}

	public partial class FirstApproveAction : NormalizedSystems.Net.ActionElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "FirstApproveAction", Version = 1 };

		public Order Order
        {
            get
            {
                return InputData["Order"].Cast<Order>();
            }
            set
            {
				InputData["Order"] = value;
            }
        }

		public SecondApproveEvent SecondApproveEvent
        {
            get
            {
                return OutputEvents["SecondApproveEvent"].Cast<SecondApproveEvent>();
            }
            set
            {
				OutputEvents["SecondApproveEvent"] = value;
            }
        }


		public FirstApproveAction()
        {
            InputData["Order"] = new Order();
            OutputEvents["SecondApproveEvent"] = new SecondApproveEvent();
        }
	}

	public partial class SecondApproveAction : NormalizedSystems.Net.ActionElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "SecondApproveAction", Version = 1 };

		public Order Order
        {
            get
            {
                return InputData["Order"].Cast<Order>();
            }
            set
            {
				InputData["Order"] = value;
            }
        }


		public SecondApproveAction()
        {
            InputData["Order"] = new Order();
        }
	}

    public class NewOrderRule : NormalizedSystems.Net.RuleElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "NewOrderRule", Version = 1 };

		public NewOrderEvent NewOrderEvent
        {
            get
            {
                return Events["NewOrderEvent"].Cast<NewOrderEvent>();
            }
            set
            {
				Events["NewOrderEvent"] = value;
            }
        }

		public NewOrderAction NewOrderAction
        {
            get
            {
                return Actions["NewOrderAction"].Cast<NewOrderAction>();
            }
            set
            {
				Actions["NewOrderAction"] = value;
            }
        }


		public NewOrderRule()
        {
            Events["NewOrderEvent"] = new NewOrderEvent();
            Actions["NewOrderAction"] = new NewOrderAction();
            LogicType = LogicType.And;
        }
	}

    public class FirstApproveRule : NormalizedSystems.Net.RuleElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "FirstApproveRule", Version = 1 };

		public FirstApproveEvent FirstApproveEvent
        {
            get
            {
                return Events["FirstApproveEvent"].Cast<FirstApproveEvent>();
            }
            set
            {
				Events["FirstApproveEvent"] = value;
            }
        }

		public FirstApproveAction FirstApproveAction
        {
            get
            {
                return Actions["FirstApproveAction"].Cast<FirstApproveAction>();
            }
            set
            {
				Actions["FirstApproveAction"] = value;
            }
        }


		public FirstApproveRule()
        {
            Events["FirstApproveEvent"] = new FirstApproveEvent();
            Actions["FirstApproveAction"] = new FirstApproveAction();
            LogicType = LogicType.And;
        }
	}

    public class SecondApproveRule : NormalizedSystems.Net.RuleElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "SecondApproveRule", Version = 1 };

		public SecondApproveEvent SecondApproveEvent
        {
            get
            {
                return Events["SecondApproveEvent"].Cast<SecondApproveEvent>();
            }
            set
            {
				Events["SecondApproveEvent"] = value;
            }
        }

		public SecondApproveAction SecondApproveAction
        {
            get
            {
                return Actions["SecondApproveAction"].Cast<SecondApproveAction>();
            }
            set
            {
				Actions["SecondApproveAction"] = value;
            }
        }


		public SecondApproveRule()
        {
            Events["SecondApproveEvent"] = new SecondApproveEvent();
            Actions["SecondApproveAction"] = new SecondApproveAction();
            LogicType = LogicType.And;
        }
	}


    public partial class OrdersApplication : NormalizedSystems.Net.Application
	{
        public OrdersApplication()
        {
            AddRule<NewOrderRule>();
            AddRule<FirstApproveRule>();
            AddRule<SecondApproveRule>();
        }
	}
}

