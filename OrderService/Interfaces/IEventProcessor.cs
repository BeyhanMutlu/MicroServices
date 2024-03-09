namespace OrderService.Interfaces
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}