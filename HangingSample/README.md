# Hanging Sample

This is a console application that demonstrates a hanging process scenario.

## Description

When executed, the application:
- Acts as a **parent process** that starts itself as a child process
- The **child process** hangs forever in an infinite loop with `Thread.Sleep(10000)`
- The **parent process** waits indefinitely for the child process to exit (which never happens)

## How to Run

### Using dotnet run
```bash
cd HangingSample
dotnet run
```

### Using the compiled executable
```bash
cd HangingSample
dotnet build
./bin/Debug/net9.0/HangingSample
```

## Expected Behavior

When you run the application, you should see output similar to:
```
Parent process starting child...
Child process started with PID: [process_id]
Parent process waiting for child to exit...
Child process started. Hanging forever...
```

Both processes will then hang indefinitely. You'll need to manually terminate them (e.g., using Ctrl+C or killing the processes).

## Implementation Details

- The application uses a command-line argument `--child` to differentiate between parent and child processes
- Parent process: No arguments → starts a child process and waits
- Child process: `--child` argument → enters infinite loop
- The implementation handles both `dotnet run` execution (using .dll) and direct executable execution
