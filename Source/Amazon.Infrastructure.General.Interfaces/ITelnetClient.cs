namespace Amazon.Infrastructure.General.Interfaces
{
    public interface ITelnetClient
    {
        void Connect(string hostName);
        
        void WaitFor(string pattern);

        string Execute(string command, string waitForPrompt);

        short Port { get; }
        
        bool IsConnected { get; }
    }
}
