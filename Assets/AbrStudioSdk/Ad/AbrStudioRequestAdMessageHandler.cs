using System;
using AbrStudioSdk.Core;

namespace AbrStudioSdk.Ad
{
    public class AbrStudioRequestAdMessageHandler : IMessageHandler
    {
        private const string AdReadyEvent = "adReady";
        private const string ErrorEvent = "error";
        private const string NoAdEvent = "noAdAvailable";
        private const string NoNetworkEvent = "noNetwork";
    
        private const string IdKey = "id";
        private const string CodeKey = "code";
        private const string MessageKey = "message";
    
    
        public void handleMessage(AbrStudioMessage message, ICallbackData data)
        {
            var requestAdData = data as RequestAdCallbackData;

            if (requestAdData == null)
            {
                return;
            }
        
            string eventType = message.eventType;
            switch (eventType)
            {
                case null:
                    return;
                case AdReadyEvent:
                    _handleAdReady(message, requestAdData.AdReadyAction);
                    return;
                case ErrorEvent:
                    _handleError(message, requestAdData.ErrorAction);
                    return;
                case NoAdEvent:
                    var noAdAction = requestAdData.NoAdAction;
                    if (noAdAction != null)
                    {
                        noAdAction();
                    }
                    return;
                case NoNetworkEvent:
                    var noNetAction = requestAdData.NoNetworkAction;
                    if (noNetAction != null)
                    {
                        noNetAction();
                    }
                    return;
            }
        }

        private void _handleAdReady(AbrStudioMessage message, Action<string> action)
        {
            if (action != null)
            {
                action(message.data[IdKey].Value);   
            }
        }

        private void _handleError(AbrStudioMessage message, Action<long, string> action)
        {
            if (action != null)
            {
                action(message.data[CodeKey].AsInt, message.data[MessageKey].Value);   
            }
        }
    }
}
