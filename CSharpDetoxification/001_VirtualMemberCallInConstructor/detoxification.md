#解毒

其实这个warning很好理解，就是提醒我们不要在非封闭类型的构造函数内调用虚方法或虚属性。但为什么这样做不合适呢？在解毒之前，我们先来了解两个概念。

##类型的初始化顺序

我们先来看这样一段代码：

```csharp
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
static class Program
{
    static void Main()
    {
        new Derived();
        Console.Read();
    }
}
```

猜一猜它的输出结果是什么？

你也许已经猜到了，它的结果是

```csharp
Base constructor
Derived constructor
```

我们在初始化一个对象时，总是会先执行基类的构造函数，然后再执行子类的构造函数。


##虚方法调用

我们再来看一段代码：

```csharp
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
static class Program
{
    static void Main()
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
```

再来猜一猜输出结果。

貌似应该是
```plain
Base.M
Base.V
Derived.M
Derived.V
```

但运行一下会发现，真正的结果是这样的
```plain
Base.M
Derived.V
Derived.M
Derived.V
```

这是为什么呢？

对于非虚方法调用，编译器会进行一些额外的“动作”。比如找出所调用对象的实际类型，以访问正确的方法表（调用`b.V()`的时候就会找到变量`b`的实际类型`Derived`，从而输出`Derived.V`）。

> **毒中毒** 思考一下，为了找到所调用对象的实际类型，编译器会做哪些额外的操作？这些操作对于虚方法调用有哪些影响？

##解药

现在回到我们最初的问题，virtual member call in constructor。结合以上两个知识点，会有哪些发现？

我们稍微改造一下虚方法调用的那个例子。

```csharp
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
```

在`Base`的构造函数中调用虚方法`V()`时，ReSharper会给出Virtual member call in constructor的警告。这是因为`V`可以在`Base`的任意子类中被改写（override），而这种改写，很有可能使得它依赖于自己的构造函数，如上例所示。而由于之前提到的类型初始化顺序，在执行`Base b = new Derived();`这样的代码时，`Base`的构造函数要早于`Derived`的构造函数执行，因此在执行到`foo.Bar()`时会抛出异常。

##总结

明白了吗？我们来简单总结一下。Virtual member call in constructor的warning是因为，对于`Base b = new Derived();`这样的代码：

1. 基类构造函数的执行要早于子类构造函数
2. 基类构造函数中对于虚方法的调用，实际调用的是子类中重写的虚方法

因此，ReSharper会警告我们，这么做存在隐患。

##尾声

Come on! 艾瑞巴蒂！This is MC解毒菌！

This is just the start!

This is never gonna be over!

跟我一起，say yeahhhhh!（yearhhhhhh!）

Say yeahhhhh!（yearhhhhh!）

C-Sharrrrrrrrrrrp!!!!
