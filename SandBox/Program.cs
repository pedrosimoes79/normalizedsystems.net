using NormalizedSystems.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SandBox
{
    //Expand V1
    public partial class PersonId : FieldElement<int>
    {
        public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "PersonId", Version = 1 };
    }

    //Expand V2
    public partial class PersonIdVersion2 : FieldElement<string>
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

    //Custom V2
    public partial class PersonIdVersion2
    {
        public void Convert(PersonId dest)
        {
            if (Value != null)
            {
                dest.Value = int.Parse(Value.Replace("PERSON", string.Empty));
            }
        }
    }

    //Expand V3
    public partial class PersonIdVersion3 : FieldElement<ulong>
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

        public static implicit operator PersonId(PersonIdVersion3 obj)
        {
            return obj._base;
        }

        public static implicit operator PersonIdVersion2(PersonIdVersion3 obj)
        {
            return obj._base;
        }
    }

    //Custom V3
    public partial class PersonIdVersion3
    {
        public void Convert(PersonIdVersion2 dest)
        {
            dest.Value = string.Format("PERSON{0:D8}", Value);
        }
    }

    //Expand V4
    public partial class PersonIdVersion4 : FieldElement<string>
    {
        public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "Person", Version = 4 };

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

        public static implicit operator PersonId(PersonIdVersion4 obj)
        {
            return obj._base;
        }

        public static implicit operator PersonIdVersion2(PersonIdVersion4 obj)
        {
            return obj._base;
        }

        public static implicit operator PersonIdVersion3(PersonIdVersion4 obj)
        {
            return obj._base;
        }
    }

    //Custom V3
    public partial class PersonIdVersion4
    {
        public void Convert(PersonIdVersion3 dest)
        {
            dest.Value = ulong.Parse(Value.Replace("P", string.Empty));
        }
    }

    //Expand V1
    public partial class PersonName : FieldElement<string>
    {
        public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "PersonName", Version = 1 };
    }

    //Expand V2
    public partial class FirstName : FieldElement<string>
    {
        public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "FirstName", Version = 1 };
    }

    //Expand V2
    public partial class LastName : FieldElement<string>
    {
        public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "LastName", Version = 1 };
    }

    //Expand V1
    public partial class Person : DataElement
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

    //Custom V1
    public partial class Person
    {
        private static int lastId = 0;

        public static int GetNextPersonId()
        {
            return ++lastId;
        }
    }

    //Expand V2
    public partial class PersonVersion2 : DataElement
    {
        public override ElementInfo ElementInfo { get; } 
            = new ElementInfo() { Name = "Person", Version = 2 };

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

    //Custom V2
    public partial class PersonVersion2 : DataElement
    {
        private void Convert(Person obj)
        {
            base.Convert(obj);
            obj.PersonName.Value = FirstName + " " + LastName;
        }
    }

    //Expand V2
    public partial class PersonVersion3 : DataElement
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

    //Custom V2
    public partial class PersonVersion3 : DataElement
    {
        private void Convert(PersonVersion2 obj)
        {
            base.Convert(obj);
            obj.PersonId.Value = "PERSON00000000";
        }
    }

    //Expand V1
    public partial class AskNameEvent : EventElement
    {
        public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AskNameEvent", Version = 1 };
    }

    //Expand V1
    public partial class AskNameCompleted : EventElement
    {
        public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AskNameCompleted", Version = 1 };

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

        public AskNameCompleted()
        {
            ContentData["Person"] = new Person();
        }
    }

    public partial class AskNameCompletedVersion2 : EventElement
    {
        public override ElementInfo ElementInfo { get; } 
            = new ElementInfo() { Name = "AskNameCompleted", Version = 2 };

        public PersonVersion2 Person
        {
            get
            {
                return ContentData["Person"].Cast<PersonVersion2>();
            }
            set
            {
                ContentData["Person"] = value;
            }
        }

        public AskNameCompletedVersion2()
        {
            ContentData["Person"] = new PersonVersion2();
        }

        public static implicit operator AskNameCompleted(AskNameCompletedVersion2 obj)
        {
            var ret = new AskNameCompleted();
            obj.Convert(ret);
            return ret;
        }
    }

    //Expand V2
    public partial class AskFirstNameEvent : EventElement
    {
        public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AskFirstNameEvent", Version = 1 };
    }

    //Expand V3
    public partial class AskFirstNameEventVersion2 : EventElement
    {
        public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AskFirstNameEvent", Version = 2 };

        public static implicit operator AskFirstNameEvent(AskFirstNameEventVersion2 obj)
        {
            var ret = new AskFirstNameEvent();
            obj.Convert(ret);
            return ret;
        }
    }

    //Expand V2
    public partial class AskLastNameEvent : EventElement
    {
        public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AskLastNameEvent", Version = 1 };

        public PersonVersion2 Person
        {
            get
            {
                return ContentData["Person"].Cast<PersonVersion2>();
            }
            set
            {
                ContentData["Person"] = value;
            }
        }

        public AskLastNameEvent()
        {
            ContentData["Person"] = new PersonVersion2();
        }
    }

    //Expand V2
    public partial class AskFirstNameCompleted : EventElement
    {
        public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AskFirstNameCompleted", Version = 1 };

        public PersonVersion2 Person
        {
            get
            {
                return ContentData["Person"].Cast<PersonVersion2>();
            }
            set
            {
                ContentData["Person"] = value;
            }
        }

        public AskFirstNameCompleted()
        {
            ContentData["Person"] = new PersonVersion2();
        }
    }

    //Expand V2
    public partial class AskLastNameCompleted : EventElement
    {
        public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AskLastNameCompleted", Version = 1 };

        public PersonVersion2 Person
        {
            get
            {
                return ContentData["Person"].Cast<PersonVersion2>();
            }
            set
            {
                ContentData["Person"] = value;
            }
        }

        public AskLastNameCompleted()
        {
            ContentData["Person"] = new PersonVersion2();
        }
    }

    //Expand V1
    public partial class SayHelloCompleted : EventElement
    {
        public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "SayHelloCompleted", Version = 1 };
    }

    //Expand V1
    public partial class AskNameAction : ActionElement
    {
        public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AskNameAction", Version = 1 };

        public AskNameCompletedVersion2 AskNameCompleted
        {
            get
            {
                return OutputEvents["AskNameCompleted"].Cast<AskNameCompletedVersion2>();
            }
            set
            {
                OutputEvents["AskNameCompleted"] = value;
            }
        }

        public AskNameAction()
        {
            OutputEvents["AskNameCompleted"] = new AskNameCompletedVersion2();
        }
    }

    //Custom V1
    public partial class AskNameAction
    {
        public override void Execute()
        {
            Console.WriteLine("Type your name!");
            var name = Console.ReadLine();

            var person = new PersonVersion2();

            person.FirstName.Value = name.Split(' ')[0];
            person.LastName.Value = name.Split(' ')[name.Split(' ').Length - 1];

            AskNameCompleted.Person = person;

            AskNameCompleted.Raise();
        }
    }

    //Expand V2
    public partial class AskFirstNameAction : ActionElement
    {
        public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AskFirstNameAction", Version = 1 };

        public AskFirstNameCompleted AskFirstNameCompleted
        {
            get
            {
                return OutputEvents["AskFirstNameCompleted"].Cast<AskFirstNameCompleted>();
            }
            set
            {
                OutputEvents["AskFirstNameCompleted"] = value;
            }
        }

        public AskFirstNameAction()
        {
            OutputEvents["AskFirstNameCompleted"] = new AskFirstNameCompleted();
        }
    }

    //Custom V2
    public partial class AskFirstNameAction
    {
        public override void Execute()
        {
            Console.WriteLine("Type your first name!");
            var name = Console.ReadLine();

            var person = new PersonVersion2();
            person.FirstName.Value = name;

            AskFirstNameCompleted.Person = person;
            AskFirstNameCompleted.Raise();
        }
    }

    //Expand V2
    public partial class AskLastNameAction : ActionElement
    {
        public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AskLastNameAction", Version = 1 };

        public PersonVersion2 Person
        {
            get
            {
                return InputData["Person"].Cast<PersonVersion2>();
            }
            set
            {
                InputData["Person"] = value;
            }
        }

        public AskLastNameCompleted AskLastNameCompleted
        {
            get
            {
                return OutputEvents["AskLastNameCompleted"].Cast<AskLastNameCompleted>();
            }
            set
            {
                OutputEvents["AskLastNameCompleted"] = value;
            }
        }

        public AskLastNameAction()
        {
            InputData["Person"] = new PersonVersion2();
            OutputEvents["AskLastNameCompleted"] = new AskLastNameCompleted();
        }
    }

    //Custom V2
    public partial class AskLastNameAction
    {
        public override void Execute()
        {
            Console.WriteLine("Type your last name!");
            var name = Console.ReadLine();

            Person.LastName.Value = name;

            AskLastNameCompleted.Person = Person;

            AskLastNameCompleted.Raise();
        }
    }

    //Expand V1
    public partial class SayHelloAction : ActionElement
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

        public SayHelloCompleted SayHelloCompleted
        {
            get
            {
                return OutputEvents["SayHelloCompleted"].Cast<SayHelloCompleted>();
            }
            set
            {
                OutputEvents["SayHelloCompleted"] = value;
            }
        }

        public SayHelloAction()
        {
            InputData["Person"] = new Person();
            OutputEvents["SayHelloCompleted"] = new SayHelloCompleted();
        }
    }

    //Custom V1
    public partial class SayHelloAction
    {
        public override void Execute()
        {
            Console.WriteLine(string.Format("V1: Hello {0} and welcome to this new brave world of Normalized Systems! :)", Person.PersonName));

            SayHelloCompleted.Raise();
        }
    }

    //Expand V2
    public partial class SayHelloActionVersion2 : ActionElement
    {
        public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "SayHelloAction", Version = 2 };

        public PersonVersion2 Person
        {
            get
            {
                return InputData["Person"].Cast<PersonVersion2>();
            }
            set
            {
                InputData["Person"] = value;
            }
        }

        public SayHelloCompleted SayHelloCompleted
        {
            get
            {
                return OutputEvents["SayHelloCompleted"].Cast<SayHelloCompleted>();
            }
            set
            {
                OutputEvents["SayHelloCompleted"] = value;
            }
        }

        public SayHelloActionVersion2()
        {
            InputData["Person"] = new PersonVersion2();
            OutputEvents["SayHelloCompleted"] = new SayHelloCompleted();
        }
    }

    //Custom V2
    public partial class SayHelloActionVersion2
    {
        public override void Execute()
        {
            Console.WriteLine(string.Format("V2: Hello {0} {1} and welcome to this new brave world of Normalized Systems! :)", Person.FirstName, Person.LastName));

            SayHelloCompleted.Raise();
        }
    }

    //Expand
    public partial class SayHelloCompletedAction : ActionElement
    {
        public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "SayHelloCompletedAction", Version = 1 };
    }

    //Custom
    public partial class SayHelloCompletedAction
    {
        public override void Execute()
        {
            if (Debugger.IsAttached)
            {
                Console.WriteLine("Press any key!!!");
                Console.ReadKey();
            }

            Environment.Exit(0);
        }
    }

    //Expand V1
    public partial class CheckPersonName : ConditionElement
    {
        public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "CheckPersonName", Version = 1 };

        public AskNameCompleted AskNameCompleted
        {
            get
            {
                return Events["AskNameCompleted"].Cast<AskNameCompleted>();
            }
            set
            {
                Events["AskNameCompleted"] = value;
            }
        }

        public CheckPersonName()
        {
            Events["AskNameCompleted"] = new AskNameCompleted();
        }
    }

    //Custom V1
    public partial class CheckPersonName
    {
        public override bool Check()
        {
            return Regex.IsMatch(AskNameCompleted.Person.PersonName, @"^(\s?[A-Z][^\d]+)+$");
        }
    }

    //Expand V2
    public partial class CheckPersonNameVersion2 : ConditionElement
    {
        public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "CheckPersonName", Version = 2 };

        public AskLastNameCompleted AskLastNameCompleted
        {
            get
            {
                return Events["AskLastNameCompleted"].Cast<AskLastNameCompleted>();
            }
            set
            {
                Events["AskLastNameCompleted"] = value;
            }
        }

        public CheckPersonNameVersion2()
        {
            Events["AskLastNameCompleted"] = new AskLastNameCompleted();
        }
    }

    //Custom V2
    public partial class CheckPersonNameVersion2
    {
        public override bool Check()
        {
            return
                Regex.IsMatch(AskLastNameCompleted.Person.FirstName, @"^(\s?[A-Z][^\d]+){1}$") &&
                Regex.IsMatch(AskLastNameCompleted.Person.LastName, @"^(\s?[A-Z][^\d]+){1}$");
        }
    }

    //Expand V1
    public class AskNameRule : RuleElement
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
        }
    }

    //Expand V2
    public class AskFirstNameRule : RuleElement
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
        }
    }

    //Expand V2
    public class AskLastNameRule : RuleElement
    {
        public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "AskLastNameRule", Version = 1 };

        public AskFirstNameCompleted AskFirstNameCompleted
        {
            get
            {
                return Events["AskFirstNameCompleted"].Cast<AskFirstNameCompleted>();
            }
            set
            {
                Events["AskFirstNameCompleted"] = value;
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
            Events["AskFirstNameCompleted"] = new AskFirstNameCompleted();
            Actions["AskLastNameAction"] = new AskLastNameAction();
        }
    }

    //Expand V1
    public class SayHelloRule : RuleElement
    {
        public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "SayHelloRule", Version = 1 };

        public AskNameCompleted AskNameCompleted
        {
            get
            {
                return Events["AskNameCompleted"].Cast<AskNameCompleted>();
            }
            set
            {
                Events["AskNameCompleted"] = value;
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
            Events["AskNameCompleted"] = new AskNameCompleted();
            Conditions["CheckPersonName"] = new CheckPersonName();
            Actions["SayHelloAction"] = new SayHelloAction();
        }
    }

    //Expand V2
    public class SayHelloRuleVersion2 : RuleElement
    {
        public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "SayHelloRule", Version = 2 };

        public AskLastNameCompleted AskLastNameCompleted
        {
            get
            {
                return Events["AskLastNameCompleted"].Cast<AskLastNameCompleted>();
            }
            set
            {
                Events["AskLastNameCompleted"] = value;
            }
        }

        public CheckPersonNameVersion2 CheckPersonName
        {
            get
            {
                return Conditions["CheckPersonName"].Cast<CheckPersonNameVersion2>();
            }
            set
            {
                Conditions["CheckPersonName"] = value;
            }
        }

        public SayHelloActionVersion2 SayHelloAction
        {
            get
            {
                return Actions["SayHelloAction"].Cast<SayHelloActionVersion2>();
            }
            set
            {
                Actions["SayHelloAction"] = value;
            }
        }

        public SayHelloRuleVersion2()
        {
            Events["AskLastNameCompleted"] = new AskLastNameCompleted();
            Conditions["CheckPersonName"] = new CheckPersonNameVersion2();
            Actions["SayHelloAction"] = new SayHelloActionVersion2();
        }
    }

    //Expand V1
    public class SayHelloCompleteRule : RuleElement
    {
        public override ElementInfo ElementInfo { get; } = new ElementInfo() { Name = "SayHelloCompleteRule", Version = 1 };

        public SayHelloCompleted SayHelloCompleted
        {
            get
            {
                return Events["SayHelloCompleted"].Cast<SayHelloCompleted>();
            }
            set
            {
                Events["SayHelloCompleted"] = value;
            }
        }

        public SayHelloCompletedAction SayHelloCompletedAction
        {
            get
            {
                return Actions["SayHelloCompletedAction"].Cast<SayHelloCompletedAction>();
            }
            set
            {
                Actions["SayHelloCompletedAction"] = value;
            }
        }

        public SayHelloCompleteRule()
        {
            Events["SayHelloCompleted"] = new SayHelloCompleted();
            Actions["SayHelloCompletedAction"] = new SayHelloCompletedAction();
        }
    }

    //Expand V2
    public class HelloWorld : Application
    {
        public HelloWorld()
        {
            AddRule<AskNameRule>();
            AddRule<AskFirstNameRule>();
            AddRule<AskLastNameRule>();
            AddRule<SayHelloRule>();
            AddRule<SayHelloRuleVersion2>();
            AddRule<SayHelloCompleteRule>();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var field = new PersonIdVersion4() { Value = "P123" };

            Console.WriteLine(field);
            Console.WriteLine((PersonIdVersion3)field);
            Console.WriteLine((PersonIdVersion2)field);
            Console.WriteLine((PersonId)field);

            //var personV2 = new PersonVersion2() { FirstName = new FirstName() { Value = "Hello" }, LastName = new LastName() { Value = "World" } };

            //personV2.PersonId.Value = string.Format("PERSON{0:D8}", Person.GetNextPersonId());

            //Console.WriteLine("{0}|{1}|{2}", personV2.PersonId, personV2.FirstName, personV2.LastName);

            //var personV1 = (Person)personV2;

            //Console.WriteLine("{0}|{1}", personV1.PersonId, personV1.PersonName);

            var personV3 = new PersonVersion3() { FirstName = new FirstName() { Value = "Hello" }, LastName = new LastName() { Value = "World" } };

            Console.WriteLine("{0}|{1}", personV3.FirstName, personV3.LastName);

            var personV2 = (PersonVersion2)personV3;

            Console.WriteLine("{0}|{1}|{2}", personV2.PersonId, personV2.FirstName, personV2.LastName);

            var personV1 = (Person)personV2;

            Console.WriteLine("{0}|{1}", personV1.PersonId, personV1.PersonName);

            //personV2.PersonId.Value = "PERSON00000321";
            personV1.PersonId.Value = 321;

            personV1 = (Person)personV2;

            Console.WriteLine("{0}|{1}", personV1.PersonId, personV1.PersonName);

            Console.WriteLine("{0}|{1}|{2}", personV2.PersonId, personV2.FirstName, personV2.LastName);

            Task.Run(async () =>
            {
                var app = new HelloWorld();

                while (true)
                {
                    Console.WriteLine("Option:");
                    Console.WriteLine("1 -> Version 1");
                    Console.WriteLine("2 -> Version 2");
                    Console.WriteLine("3 -> Version 2 with return");
                    var option = Console.ReadLine();

                    if (option == "1")
                    {
                        app.Raise(new AskNameEvent());
                        break;
                    }

                    if (option == "2")
                    {
                        app.Raise(new AskFirstNameEvent());
                        break;
                    }

                    if (option == "3")
                    {
                        var hello = await app.Raise<AskLastNameCompleted>(new AskFirstNameEventVersion2());
                        var person = hello.Person;

                        Console.WriteLine(string.Format("AskLastNameCompleted: {0} {1}", person.FirstName, person.LastName));

                        break;
                    }
                }

                Task.Delay(Timeout.Infinite).Wait();
            }).Wait();
        }
    }
}
