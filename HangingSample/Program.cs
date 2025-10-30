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
        FileName = Environment.ProcessPath ?? "dotnet",
        Arguments = $"\"{Environment.ProcessPath}\" --child",
        UseShellExecute = false
    };
    
    // If running with dotnet run, we need to use the dll path
    var currentProcess = Process.GetCurrentProcess();
    var assemblyLocation = typeof(Program).Assembly.Location;
    
    if (!string.IsNullOrEmpty(assemblyLocation) && assemblyLocation.EndsWith(".dll"))
    {
        startInfo.FileName = "dotnet";
        startInfo.Arguments = $"\"{assemblyLocation}\" --child";
    }
    else if (!string.IsNullOrEmpty(Environment.ProcessPath))
    {
        startInfo.FileName = Environment.ProcessPath;
        startInfo.Arguments = "--child";
    }
    
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
