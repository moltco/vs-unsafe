using System;
using System.CodeDom;
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

            // List1 & List2.Records will be null which is not correct
            // as the passed object (that implements interface) has
            // Records.Count = 3
            Debug.WriteLine(
                $"Records is {List1.Records} and has " +
                $"{List1.Records?.Count} items");

            // let's try casting here
            if (one?.GetType() == typeof(ExampleListManager))
            {
                List1 = one as ExampleListManager;
            }

            Debug.WriteLine(
              $"After explicit CAST Records is {List1.Records} and has " +
              $"{List1.Records?.Count} items");


            ///<notice>
            ///
            /// Entry Point error happens on code that looks like this
            /// 
            ///</notice>
            if (List1 != null && List1.Records != null)
            {
                if (List1?.Records?.Count > 0) // throws Entry Point Not Found error here
                {
                    Debug.WriteLine("I have passed Entry Point error");
                }
            }

            foreach (var entry in List1.Records)
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


        public int DoSomething_v2(
          ExampleListManager one,
          ExampleListManager two
        )
        {
            Debug.WriteLine("DoSomething_v2: Begin");
            List1 = one;
            List2 = two;
            Debug.WriteLine(
                $"DoSomething_v2: List1.Records is {List1.Records} and " +
                $"has {List1.Records?.Count} items");

            Debug.WriteLine(
             $"DoSomething_v2: List1.Records with explicit Cast is {(List1 as ExampleListManager).Records} and " +
             $"has {(List1 as ExampleListManager).Records?.Count} items");

            return List1.Records?.Count ?? -1;
        }
    }
}
