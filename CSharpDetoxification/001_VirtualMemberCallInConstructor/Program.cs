using System;
using CSharpDetoxification.VirtualMemberCallInConstructor.Detoxification2;

namespace CSharpDetoxification.VirtualMemberCallInConstructor
{
    static class Program
    {
        static void Main()
        {
            var d = new Derived();
            Base b = d;
            b.V();
            d.V();
            Console.Read();
        }
    }
}
