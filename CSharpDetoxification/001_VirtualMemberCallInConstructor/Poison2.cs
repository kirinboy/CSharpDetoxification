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
    
}
