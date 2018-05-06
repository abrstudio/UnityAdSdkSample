namespace AbrStudioSdk.Core
{
    public interface IMessageHandler
    {
        void handleMessage(AbrStudioMessage message, ICallbackData data);
    }
}