using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    public class Test2 : Test
    {
        public delegate int Print(int value);
        public override void Process()
        {

            Print Res = delegate (int val)
            {
                return val + val;
            };

            Res(12);

        }

        public override void GetResult(ref int i)
        {
            i++;
            var resul = i + 1;
        }


        public override void GetResult1(out int i)
        {
            i = 12;
            var resul = i + 1;
        }

        public void Process1()
        {
            throw new NotImplementedException();
        }

       
    }

    public class Test3
    {
        public const int gh = 12;
        public readonly int h;

        public delegate int GetResult(int item);

        public Test3()
        {
            //h = 123;
        }
        public void getChanges()
        {
            Test2 obj1 = new Test2();
            obj1.Queue();
            obj1.Result();
            obj1.Result();
            int i = 1 ;
            Console.WriteLine("before i=" + i.ToString());

            obj1.GetResult(ref i);
            Console.WriteLine("after i="+i.ToString());
            obj1.Process();
            int j;
            obj1.GetResult1(out j);
            Console.WriteLine("after j="+j.ToString());

            GetEvent += new GetResult(Get1);
            GetEvent += new GetResult(Get2);

            foreach(GetResult invoke in GetEvent.GetInvocationList())
            {
                Console.WriteLine(invoke(1));
            }
        }

        public int Get1(int i)
        {
            return 1;
        }

        public int Get2(int i)
        {
            return 2;
        }

        public event GetResult GetEvent;




    }

}
