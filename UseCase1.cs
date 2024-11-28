using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ExampleProject
{
    internal class UseCase1
    {
        private IListManager<IListEntry> List1 = new ExampleListManager();
        private IListManager<IListEntry> List2 = new ExampleListManager();

        public StringBuilder DoSomething_v1(
           IListManager<IListEntry> one,
           IListManager<IListEntry> two
        )
        {
            StringBuilder sb = new StringBuilder();
            Debug.WriteLine("DoSomething_v1: Begin");
            List1 = one;
            List2 = two;


            ///<notice>
            ///
            /// Entry Point error happens on code that looks like this
            /// 
            ///</notice>
            if (List1 != null && List1.Records != null)
            {
                if (List1?.Records?.Count > 0)
                {
                    Debug.WriteLine("I have passed Entry Point error");
                }
            }

            foreach(var entry in List1.Records)
            {
                sb.Append(entry.ToString());
                Debug.WriteLine(entry.ToString());
            }

            foreach (var entry in List2.Records)
            {
                sb.Append(entry.ToString());
                Debug.WriteLine(entry.ToString());
            }
            Debug.WriteLine("DoSomething_v1: End");
            
            return sb;
        }

        public void TestDoSomehting_v1()
        {
            StringBuilder sb = DoSomething_v1(
                new ExampleListManager(), 
                new ExampleListManager());
        }
    }
}
