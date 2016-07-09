using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NSHelloWord
{
    public partial class PersonIdVersion2
    {
        public void Convert(PersonId dest)
        {
            dest.Value = int.Parse(Regex.Replace((Value ?? "0"), "[^0-9]+", "", RegexOptions.Compiled));
        }
    }

    public partial class PersonIdVersion3
    {
        public void Convert(PersonIdVersion2 dest)
        {
            dest.Value = string.Format("PERSON{0:D8}", Value);
        }
    }

    public partial class PersonIdVersion4
    {
        public void Convert(PersonIdVersion3 dest)
        {
            dest.Value = ulong.Parse(Regex.Replace((Value ?? "0"), "[^0-9]+", "", RegexOptions.Compiled));
        }
    }

    public partial class PersonVersion2
    {
        public void Convert(Person dest)
        {
            base.Convert(dest);

            dest.PersonName.Value = string.Format("{0} {1}", FirstName, LastName);
        }
    }

    public partial class PersonVersion3
    {
        public void Convert(PersonVersion2 dest)
        {
            base.Convert(dest);

            dest.PersonId.Value = "PERSON00000000";
        }
    }

    public partial class AskNameAction
    {
        public override void Execute()
        {
            Console.WriteLine("What is your name? (First & Last)");
            var name = Console.ReadLine();

            AskNameCompletedEvent.Person.PersonName.Value = name;
            AskNameCompletedEvent.Raise();
        }
    }

    public partial class SayHelloAction
    {
        public override void Execute()
        {
            Console.WriteLine("Hello {0}!", Person.PersonName);

            SayHelloCompletedEvent.Raise();
        }
    }

    public partial class CheckPersonName
    {
        public override bool Check()
        {
            return Regex.IsMatch(AskNameCompletedEvent.Person.PersonName, @"^(\s?[A-Z][^\d]+){2}$");
        }
    }

    public partial class CheckFirstName
    {
        public override bool Check()
        {
            return Regex.IsMatch(AskLastNameCompletedEvent.Person.FirstName, @"^[A-Z][^\d]+$");
        }
    }

    public partial class CheckLastName
    {
        public override bool Check()
        {
            return Regex.IsMatch(AskLastNameCompletedEvent.Person.LastName, @"^[A-Z][^\d]+$");
        }
    }

    public partial class AskFirstNameAction
    {
        public override void Execute()
        {
            Console.WriteLine("Type your first name!");

            AskFirstNameCompletedEvent.Person.FirstName.Value = Console.ReadLine();
            AskFirstNameCompletedEvent.Raise();
        }
    }

    public partial class AskLastNameAction
    {
        public override void Execute()
        {
            Console.WriteLine("Type your last name!");

            Person.LastName.Value = Console.ReadLine();
            AskLastNameCompletedEvent.Person = Person;
            AskLastNameCompletedEvent.Raise();
        }
    }

    public partial class ApplicationExitAction
    {
        public override void Execute()
        {
            if(Debugger.IsAttached)
            {
                Console.WriteLine("Press any key to exit!");
                Console.ReadKey();
            }
            Environment.Exit(0);
        }
    }

    public partial class HelloWorldApplication
    {
    }
}
