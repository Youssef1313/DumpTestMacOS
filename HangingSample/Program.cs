using System.Diagnostics;
using Microsoft.Diagnostics.NETCore.Client;

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
        Arguments = Environment.CommandLine + " --child",
        UseShellExecute = false
    };
    
    var childProcess = Process.Start(startInfo);
    
    if (childProcess != null)
    {
        Console.WriteLine($"Child process started with PID: {childProcess.Id}");
        
        // Wait for 10 seconds
        Console.WriteLine("Waiting for 10 seconds...");
        Thread.Sleep(10000);
        
        // Dump the hanging child process
        Console.WriteLine("Dumping the child process...");
        try
        {
            var client = new DiagnosticsClient(childProcess.Id);
            string dumpPath = $"child_process_{childProcess.Id}.dmp";
            client.WriteDump(DumpType.Normal, dumpPath);
            Console.WriteLine($"Dump created at: {Path.GetFullPath(dumpPath)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to dump process: {ex.Message}");
        }
        
        // Kill the child process
        Console.WriteLine("Killing the child process...");
        childProcess.Kill();
        childProcess.WaitForExit();
        Console.WriteLine("Child process killed.");
    }
    else
    {
        Console.WriteLine("Failed to start child process.");
    }
}
