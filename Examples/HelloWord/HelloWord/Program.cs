using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NSHelloWord
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                var app = new HelloWorldApplication();

                // Version 1 
                app.Raise(new AskNameEvent());

                // Version 2 
                //app.Raise(new AskFirstNameEvent());

                await Task.Delay(Timeout.Infinite);
            }).Wait();
        }
    }
}
