using System;

namespace Test
{
    public abstract class Test
    {

        public int Result()
        {
            return 1;
        }
        public abstract void Process();

        public abstract void GetResult(ref int i);

        public abstract void GetResult1(out int i);



        public int Queue()
        {
            return 0;
        }
    }

    public interface ITest
    {

        void Process1();

    }

    public class GenericClass<T>
    {
        //public T GetSum(T var1, T var2)
        //{
        //    dynamic a1 = var1;
        //    dynamic b = var2;
        //    return a1 + b;
        //}
    }

}
