using System;
using CSharpDetoxification.VirtualMemberCallInConstructor.Poison;

namespace CSharpDetoxification.VirtualMemberCallInConstructor
{
    static class Program
    {
        static void Main()
        {
            new Derived();
            Console.Read();
        }
    }
}
