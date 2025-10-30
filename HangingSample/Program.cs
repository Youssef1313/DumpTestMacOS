using System.Diagnostics;

if (args.Length > 0 && args[0] == "--child")
{
    // Child process - hang forever
    Console.WriteLine("Child process started. Hanging forever...");
    while (true)
    {
        Thread.Sleep(10000);
    }
}
else
{
    // Parent process - start child and wait
    Console.WriteLine("Parent process starting child...");
    
    var startInfo = new ProcessStartInfo
    {
        FileName = Environment.ProcessPath!,
        Arguments = "--child",
        UseShellExecute = false
    };
    
    var childProcess = Process.Start(startInfo);
    
    if (childProcess != null)
    {
        Console.WriteLine($"Child process started with PID: {childProcess.Id}");
        Console.WriteLine("Parent process waiting for child to exit...");
        childProcess.WaitForExit();
        Console.WriteLine("Child process exited.");
    }
    else
    {
        Console.WriteLine("Failed to start child process.");
    }
}
