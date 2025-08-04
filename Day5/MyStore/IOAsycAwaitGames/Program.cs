Console.WriteLine($"App started {Thread.CurrentThread.ManagedThreadId}");
_ = TestAsync1();
Console.WriteLine("app code completed");
Console.ReadLine();


async Task TestAsync1()
{
    for (int i = 0; i < 100; i++)
    {
        Console.WriteLine("Start working on TestAsync1 function");
    }
    Console.WriteLine($"TestAsync1 {Thread.CurrentThread.ManagedThreadId}");

    //await Task.Delay(1000);
    //await TestAsync2();
    Console.WriteLine("TestAsync1 completed"); //3
}

async Task TestAsync2()
{
    for (int i = 0; i < 5; i++)
    {
        await Task.Delay(500);
        Console.WriteLine($"TestAsync2 iteration {i + 1} completed"); //2
    }
}