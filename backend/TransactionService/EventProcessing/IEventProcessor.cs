namespace TransactionService.EventProcessing
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}