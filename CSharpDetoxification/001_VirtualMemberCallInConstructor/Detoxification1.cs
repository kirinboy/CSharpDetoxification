using System;

namespace CSharpDetoxification.VirtualMemberCallInConstructor.Detoxification1
{
    class Foo
    {
        public Foo(string s)
        {
            Console.WriteLine(s);
        }
        public void Bar() { }
    }

    internal class Base
    {
        public Base()
        {
            Console.WriteLine("Base constructor");
            if (this is Derived) (this as Derived).DoIt();
            // would deref null if we are constructing an instance of Derived
            Blah();
            // would deref null if we are constructing an instance of MoreDerived
        }

        public virtual void Blah()
        {
        }
    }


    internal class Derived : Base
    {
        private readonly Foo derivedFoo = new Foo("Derived initializer");

        public void DoIt()
        {
            derivedFoo.Bar();
        }
    }

    internal class MoreDerived : Derived
    {
        public override void Blah()
        {
            DoIt();
        }
    }
}
