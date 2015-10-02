using System;

namespace CSharpDetoxification.VirtualMemberCallInConstructor.Poison2
{
    class Base
    {
        public void M()
        {
            Console.WriteLine("Base.M");
        }

        public virtual void V()
        {
            Console.WriteLine("Base.V");
        }
    }
    class Derived : Base
    {
        public new void M()
        {
            Console.WriteLine("Derived.M");
        }

        public override void V()
        {
            Console.WriteLine("Derived.V");
        }
    }

    class Client
    {
        static void Main2()
        {
            var d = new Derived();
            Base b = d;
            b.M();
            b.V();
            d.M();
            d.V();
            Console.Read();
        }
    }
}
