using System;

namespace CSharpDetoxification.VirtualMemberCallInConstructor.Poison1
{
    class Base
    {
        public Base()
        {
            Console.WriteLine("Base constructor");
        }
    }
    class Derived : Base
    {
        public Derived()
        {
            Console.WriteLine("Derived constructor");
        }
    }

    class Client
    {
        static void Main1()
        {
            new Derived();
        }    
    }
    
}
