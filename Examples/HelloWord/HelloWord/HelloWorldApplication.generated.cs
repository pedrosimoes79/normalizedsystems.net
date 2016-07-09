
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using NormalizedSystems.Net;

namespace NSHelloWord
{
	public partial class PersonName : NormalizedSystems.Net.FieldElement<string>
	{	
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "PersonName", Version = 1 };

	}

	public partial class PersonId : NormalizedSystems.Net.FieldElement<int>
	{	
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "PersonId", Version = 1 };

	}

	public partial class PersonIdVersion2 : NormalizedSystems.Net.FieldElement<string>
	{	
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "PersonId", Version = 2 };

		private PersonId _base;

		public new string Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                base.Value = value;
                Convert(_base);
            }
        }

		public PersonIdVersion2()
        {
            _base = new PersonId();
        }

		public static implicit operator PersonId(PersonIdVersion2 obj)
        {
            return obj._base;
        }

	}

	public partial class PersonIdVersion3 : NormalizedSystems.Net.FieldElement<ulong>
	{	
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "PersonId", Version = 3 };

		private PersonIdVersion2 _base;

		public new ulong Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                base.Value = value;
                Convert(_base);
            }
        }

		public PersonIdVersion3()
        {
            _base = new PersonIdVersion2();
        }

		public static implicit operator PersonIdVersion2(PersonIdVersion3 obj)
        {
            return obj._base;
        }

		public static implicit operator PersonId(PersonIdVersion3 obj)
        {
            return obj._base;
        }

	}

	public partial class PersonIdVersion4 : NormalizedSystems.Net.FieldElement<string>
	{	
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "PersonId", Version = 4 };

		private PersonIdVersion3 _base;

		public new string Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                base.Value = value;
                Convert(_base);
            }
        }

		public PersonIdVersion4()
        {
            _base = new PersonIdVersion3();
        }

		public static implicit operator PersonIdVersion3(PersonIdVersion4 obj)
        {
            return obj._base;
        }

		public static implicit operator PersonIdVersion2(PersonIdVersion4 obj)
        {
            return obj._base;
        }

		public static implicit operator PersonId(PersonIdVersion4 obj)
        {
            return obj._base;
        }

	}

	public partial class FirstName : NormalizedSystems.Net.FieldElement<string>
	{	
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "FirstName", Version = 1 };

	}

	public partial class LastName : NormalizedSystems.Net.FieldElement<string>
	{	
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "LastName", Version = 1 };

	}

	public partial class Person : NormalizedSystems.Net.DataElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "Person", Version = 1 };

		public PersonId PersonId
        {
            get
            {
                return Fields["PersonId"].Cast<PersonId>();
            }
            set
            {
				Fields["PersonId"] = value;
            }
        }

		public PersonName PersonName
        {
            get
            {
                return Fields["PersonName"].Cast<PersonName>();
            }
            set
            {
				Fields["PersonName"] = value;
            }
        }

		public Person()
        {
            Fields["PersonId"] = new PersonId();
            Fields["PersonName"] = new PersonName();
        }

	}

	public partial class PersonVersion2 : NormalizedSystems.Net.DataElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "Person", Version = 2 };

		public PersonIdVersion2 PersonId
        {
            get
            {
                return Fields["PersonId"].Cast<PersonIdVersion2>();
            }
            set
            {
				Fields["PersonId"] = value;
            }
        }

		public FirstName FirstName
        {
            get
            {
                return Fields["FirstName"].Cast<FirstName>();
            }
            set
            {
				Fields["FirstName"] = value;
            }
        }

		public LastName LastName
        {
            get
            {
                return Fields["LastName"].Cast<LastName>();
            }
            set
            {
				Fields["LastName"] = value;
            }
        }

		public PersonVersion2()
        {
            Fields["PersonId"] = new PersonIdVersion2();
            Fields["FirstName"] = new FirstName();
            Fields["LastName"] = new LastName();
        }

		public static implicit operator Person(PersonVersion2 obj)
        {
            var ret = new Person();
			obj.Convert(ret);
			return ret;
        }

	}

	public partial class PersonVersion3 : NormalizedSystems.Net.DataElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "Person", Version = 3 };

		public FirstName FirstName
        {
            get
            {
                return Fields["FirstName"].Cast<FirstName>();
            }
            set
            {
				Fields["FirstName"] = value;
            }
        }

		public LastName LastName
        {
            get
            {
                return Fields["LastName"].Cast<LastName>();
            }
            set
            {
				Fields["LastName"] = value;
            }
        }

		public PersonVersion3()
        {
            Fields["FirstName"] = new FirstName();
            Fields["LastName"] = new LastName();
        }

		public static implicit operator PersonVersion2(PersonVersion3 obj)
        {
            var ret = new PersonVersion2();
			obj.Convert(ret);
			return ret;
        }

		public static implicit operator Person(PersonVersion3 obj)
        {
				return (Person)(PersonVersion2)obj;
        }

	}

	public partial class AskNameEvent : NormalizedSystems.Net.EventElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AskNameEvent", Version = 1 };


		public AskNameEvent()
        {
        }

	}

	public partial class AskNameCompletedEvent : NormalizedSystems.Net.EventElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AskNameCompletedEvent", Version = 1 };

		public Person Person
        {
            get
            {
                return ContentData["Person"].Cast<Person>();
            }
            set
            {
				ContentData["Person"] = value;
            }
        }


		public AskNameCompletedEvent()
        {
            ContentData["Person"] = new Person();
        }

	}

	public partial class SayHelloCompletedEvent : NormalizedSystems.Net.EventElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "SayHelloCompletedEvent", Version = 1 };


		public SayHelloCompletedEvent()
        {
        }

	}

	public partial class AskFirstNameEvent : NormalizedSystems.Net.EventElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AskFirstNameEvent", Version = 1 };


		public AskFirstNameEvent()
        {
        }

	}

	public partial class AskFirstNameCompletedEvent : NormalizedSystems.Net.EventElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AskFirstNameCompletedEvent", Version = 1 };

		public PersonVersion3 Person
        {
            get
            {
                return ContentData["Person"].Cast<PersonVersion3>();
            }
            set
            {
				ContentData["Person"] = value;
            }
        }


		public AskFirstNameCompletedEvent()
        {
            ContentData["Person"] = new PersonVersion3();
        }

	}

	public partial class AskLastNameCompletedEvent : NormalizedSystems.Net.EventElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AskLastNameCompletedEvent", Version = 1 };

		public PersonVersion3 Person
        {
            get
            {
                return ContentData["Person"].Cast<PersonVersion3>();
            }
            set
            {
				ContentData["Person"] = value;
            }
        }


		public AskLastNameCompletedEvent()
        {
            ContentData["Person"] = new PersonVersion3();
        }

	}

	public partial class AskNameAction : NormalizedSystems.Net.ActionElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AskNameAction", Version = 1 };

		public AskNameCompletedEvent AskNameCompletedEvent
        {
            get
            {
                return OutputEvents["AskNameCompletedEvent"].Cast<AskNameCompletedEvent>();
            }
            set
            {
				OutputEvents["AskNameCompletedEvent"] = value;
            }
        }


		public AskNameAction()
        {
            OutputEvents["AskNameCompletedEvent"] = new AskNameCompletedEvent();
        }
	}

	public partial class SayHelloAction : NormalizedSystems.Net.ActionElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "SayHelloAction", Version = 1 };

		public Person Person
        {
            get
            {
                return InputData["Person"].Cast<Person>();
            }
            set
            {
				InputData["Person"] = value;
            }
        }

		public SayHelloCompletedEvent SayHelloCompletedEvent
        {
            get
            {
                return OutputEvents["SayHelloCompletedEvent"].Cast<SayHelloCompletedEvent>();
            }
            set
            {
				OutputEvents["SayHelloCompletedEvent"] = value;
            }
        }


		public SayHelloAction()
        {
            InputData["Person"] = new Person();
            OutputEvents["SayHelloCompletedEvent"] = new SayHelloCompletedEvent();
        }
	}

	public partial class ApplicationExitAction : NormalizedSystems.Net.ActionElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "ApplicationExitAction", Version = 1 };


		public ApplicationExitAction()
        {
        }
	}

	public partial class AskFirstNameAction : NormalizedSystems.Net.ActionElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AskFirstNameAction", Version = 1 };

		public AskFirstNameCompletedEvent AskFirstNameCompletedEvent
        {
            get
            {
                return OutputEvents["AskFirstNameCompletedEvent"].Cast<AskFirstNameCompletedEvent>();
            }
            set
            {
				OutputEvents["AskFirstNameCompletedEvent"] = value;
            }
        }


		public AskFirstNameAction()
        {
            OutputEvents["AskFirstNameCompletedEvent"] = new AskFirstNameCompletedEvent();
        }
	}

	public partial class AskLastNameAction : NormalizedSystems.Net.ActionElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AskLastNameAction", Version = 1 };

		public PersonVersion3 Person
        {
            get
            {
                return InputData["Person"].Cast<PersonVersion3>();
            }
            set
            {
				InputData["Person"] = value;
            }
        }

		public AskLastNameCompletedEvent AskLastNameCompletedEvent
        {
            get
            {
                return OutputEvents["AskLastNameCompletedEvent"].Cast<AskLastNameCompletedEvent>();
            }
            set
            {
				OutputEvents["AskLastNameCompletedEvent"] = value;
            }
        }


		public AskLastNameAction()
        {
            InputData["Person"] = new PersonVersion3();
            OutputEvents["AskLastNameCompletedEvent"] = new AskLastNameCompletedEvent();
        }
	}

	public partial class CheckPersonName : NormalizedSystems.Net.ConditionElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "CheckPersonName", Version = 1 };

        public AskNameCompletedEvent AskNameCompletedEvent
        {
            get
            {
                return Events["AskNameCompletedEvent"].Cast<AskNameCompletedEvent>();
            }
            set
            {
				Events["AskNameCompletedEvent"] = value;
            }
        }


		public CheckPersonName()
        {
            Events["AskNameCompletedEvent"] = new AskNameCompletedEvent();
        }
	}

	public partial class CheckFirstName : NormalizedSystems.Net.ConditionElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "CheckFirstName", Version = 1 };

        public AskLastNameCompletedEvent AskLastNameCompletedEvent
        {
            get
            {
                return Events["AskLastNameCompletedEvent"].Cast<AskLastNameCompletedEvent>();
            }
            set
            {
				Events["AskLastNameCompletedEvent"] = value;
            }
        }


		public CheckFirstName()
        {
            Events["AskLastNameCompletedEvent"] = new AskLastNameCompletedEvent();
        }
	}

	public partial class CheckLastName : NormalizedSystems.Net.ConditionElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "CheckLastName", Version = 1 };

        public AskLastNameCompletedEvent AskLastNameCompletedEvent
        {
            get
            {
                return Events["AskLastNameCompletedEvent"].Cast<AskLastNameCompletedEvent>();
            }
            set
            {
				Events["AskLastNameCompletedEvent"] = value;
            }
        }


		public CheckLastName()
        {
            Events["AskLastNameCompletedEvent"] = new AskLastNameCompletedEvent();
        }
	}

    public class AskNameRule : NormalizedSystems.Net.RuleElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AskNameRule", Version = 1 };

		public AskNameEvent AskNameEvent
        {
            get
            {
                return Events["AskNameEvent"].Cast<AskNameEvent>();
            }
            set
            {
				Events["AskNameEvent"] = value;
            }
        }

		public AskNameAction AskNameAction
        {
            get
            {
                return Actions["AskNameAction"].Cast<AskNameAction>();
            }
            set
            {
				Actions["AskNameAction"] = value;
            }
        }


		public AskNameRule()
        {
            Events["AskNameEvent"] = new AskNameEvent();
            Actions["AskNameAction"] = new AskNameAction();
            LogicType = LogicType.And;
        }
	}

    public class SayHelloRule : NormalizedSystems.Net.RuleElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "SayHelloRule", Version = 1 };

		public AskNameCompletedEvent AskNameCompletedEvent
        {
            get
            {
                return Events["AskNameCompletedEvent"].Cast<AskNameCompletedEvent>();
            }
            set
            {
				Events["AskNameCompletedEvent"] = value;
            }
        }

		public AskLastNameCompletedEvent AskLastNameCompletedEvent
        {
            get
            {
                return Events["AskLastNameCompletedEvent"].Cast<AskLastNameCompletedEvent>();
            }
            set
            {
				Events["AskLastNameCompletedEvent"] = value;
            }
        }

		public CheckPersonName CheckPersonName
        {
            get
            {
                return Conditions["CheckPersonName"].Cast<CheckPersonName>();
            }
            set
            {
				Conditions["CheckPersonName"] = value;
            }
        }

		public CheckFirstName CheckFirstName
        {
            get
            {
                return Conditions["CheckFirstName"].Cast<CheckFirstName>();
            }
            set
            {
				Conditions["CheckFirstName"] = value;
            }
        }

		public CheckLastName CheckLastName
        {
            get
            {
                return Conditions["CheckLastName"].Cast<CheckLastName>();
            }
            set
            {
				Conditions["CheckLastName"] = value;
            }
        }

		public SayHelloAction SayHelloAction
        {
            get
            {
                return Actions["SayHelloAction"].Cast<SayHelloAction>();
            }
            set
            {
				Actions["SayHelloAction"] = value;
            }
        }


		public SayHelloRule()
        {
            Events["AskNameCompletedEvent"] = new AskNameCompletedEvent();
            Events["AskLastNameCompletedEvent"] = new AskLastNameCompletedEvent();
            Conditions["CheckPersonName"] = new CheckPersonName();
            Conditions["CheckFirstName"] = new CheckFirstName();
            Conditions["CheckLastName"] = new CheckLastName();
            Actions["SayHelloAction"] = new SayHelloAction();
            LogicType = LogicType.Or;
        }
	}

    public class ApplicationExitRule : NormalizedSystems.Net.RuleElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "ApplicationExitRule", Version = 1 };

		public SayHelloCompletedEvent SayHelloCompletedEvent
        {
            get
            {
                return Events["SayHelloCompletedEvent"].Cast<SayHelloCompletedEvent>();
            }
            set
            {
				Events["SayHelloCompletedEvent"] = value;
            }
        }

		public ApplicationExitAction ApplicationExitAction
        {
            get
            {
                return Actions["ApplicationExitAction"].Cast<ApplicationExitAction>();
            }
            set
            {
				Actions["ApplicationExitAction"] = value;
            }
        }


		public ApplicationExitRule()
        {
            Events["SayHelloCompletedEvent"] = new SayHelloCompletedEvent();
            Actions["ApplicationExitAction"] = new ApplicationExitAction();
            LogicType = LogicType.Or;
        }
	}

    public class AskFirstNameRule : NormalizedSystems.Net.RuleElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AskFirstNameRule", Version = 1 };

		public AskFirstNameEvent AskFirstNameEvent
        {
            get
            {
                return Events["AskFirstNameEvent"].Cast<AskFirstNameEvent>();
            }
            set
            {
				Events["AskFirstNameEvent"] = value;
            }
        }

		public AskFirstNameAction AskFirstNameAction
        {
            get
            {
                return Actions["AskFirstNameAction"].Cast<AskFirstNameAction>();
            }
            set
            {
				Actions["AskFirstNameAction"] = value;
            }
        }


		public AskFirstNameRule()
        {
            Events["AskFirstNameEvent"] = new AskFirstNameEvent();
            Actions["AskFirstNameAction"] = new AskFirstNameAction();
            LogicType = LogicType.And;
        }
	}

    public class AskLastNameRule : NormalizedSystems.Net.RuleElement
	{
		public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AskLastNameRule", Version = 1 };

		public AskFirstNameCompletedEvent AskFirstNameCompletedEvent
        {
            get
            {
                return Events["AskFirstNameCompletedEvent"].Cast<AskFirstNameCompletedEvent>();
            }
            set
            {
				Events["AskFirstNameCompletedEvent"] = value;
            }
        }

		public AskLastNameAction AskLastNameAction
        {
            get
            {
                return Actions["AskLastNameAction"].Cast<AskLastNameAction>();
            }
            set
            {
				Actions["AskLastNameAction"] = value;
            }
        }


		public AskLastNameRule()
        {
            Events["AskFirstNameCompletedEvent"] = new AskFirstNameCompletedEvent();
            Actions["AskLastNameAction"] = new AskLastNameAction();
            LogicType = LogicType.And;
        }
	}


    public partial class HelloWorldApplication : NormalizedSystems.Net.Application
	{
        public HelloWorldApplication()
        {
            AddRule<AskNameRule>();
            AddRule<SayHelloRule>();
            AddRule<ApplicationExitRule>();
            AddRule<AskFirstNameRule>();
            AddRule<AskLastNameRule>();
        }
	}
}

