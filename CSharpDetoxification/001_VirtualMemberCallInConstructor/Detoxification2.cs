using System;

namespace CSharpDetoxification.VirtualMemberCallInConstructor.Detoxification2
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
        public Base()
        {
            V(); // Virtual member call in constructor
        }
        public virtual void V()
        {
            Console.WriteLine("Base.V");
        }
    }
    class Derived : Base
    {
        private Foo foo;
        public Derived()
        {
            foo = new Foo("foo in Derived");
        }

        public override void V()
        {
            Console.WriteLine("Derived.V");
            foo.Bar(); // will throw NullReferenceException
        }
    }
    
}
