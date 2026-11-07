using System;
using System.Threading;

namespace Vocup;

public class SingleInstanceApp
{
    // TODO: Add activation handler. Could be Action<Channel<LaunchRequest>> for example.
    public static void Run(string[] args, Action<object> primaryInstance, string mutexName)
    {
        using Mutex mutex = new(true, mutexName, out bool isFirstInstance);
        if (isFirstInstance)
        {
            // TODO: Add named pipe server listener to handle activation requests from other instances.
            primaryInstance(args);
        }
        else
        {
            // TODO: Add named pipe client to send activation request to primary instance.
            // TODO: Call SetForegroundWindow on primary instance's window handle if running on Windows.
        }
    }
}

public class LaunchRequest
{
    public string[] Args { get; }
    public string xdgActivationToken { get; }
    public void Accept(nint desiredForegroundWindow)
    {
        
    }
}
