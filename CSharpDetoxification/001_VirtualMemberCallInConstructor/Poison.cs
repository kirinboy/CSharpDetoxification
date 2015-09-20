using System;

namespace CSharpDetoxification.VirtualMemberCallInConstructor.Poison
{
    class Foo
    {
        public Foo(string s)
        {
            Console.WriteLine(s);
        }
        public void Bar() { }
    }
    class Base
    {
        readonly Foo baseFoo = new Foo("Base initializer");
        public Base()
        {
            Console.WriteLine("Base constructor");
        }
    }
    class Derived : Base
    {
        readonly Foo derivedFoo = new Foo("Derived initializer");
        public Derived()
        {
            Console.WriteLine("Derived constructor");
        }
    }
    
}
