using System;
using AbrStudioSdk.Core;

namespace AbrStudioSdk.Ad
{
    public class AbrStudioShowAdMessageHandler : IMessageHandler
    {
        private const string ErrorEvent = "error";
        private const string AdClosedEvent = "adClosed";
        private const string RewardEvent = "reward";
    
        private const string IdKey = "id";
        private const string CodeKey = "code";
        private const string MessageKey = "message";
        private const string CompletedKey = "completed";
        private const string RewardKey = "reward";
    
        private Action<string, string> _rewardAction;

        public void handleMessage(AbrStudioMessage message, ICallbackData data)
        {
            var showAdData = data as ShowAdCallbackData;

            if (showAdData == null)
            {
                return;
            }

            string eventType = message.eventType;
            switch (eventType)
            {
                case null:
                    return;
                case AdClosedEvent:
                    _handleAdClosed(message, showAdData.AdClosedAction);
                    return;
                case ErrorEvent:
                    _handleError(message, showAdData.ErrorAction);
                    return;
                case RewardEvent:
                    _handleReward(message, showAdData.RewardAction);
                    break;
            }
        }

        public void SetRewardCallback(Action<string, string> rewardAction)
        {
            _rewardAction = rewardAction;
        }

        private void _handleAdClosed(AbrStudioMessage message, Action<bool> action)
        {
            if (action != null)
            {
                action(message.data[CompletedKey].AsBool);   
            }
        }
    
        private void _handleError(AbrStudioMessage message, Action<long, string> action)
        {
            if (action != null)
            {
                action(message.data[CodeKey].AsInt, message.data[MessageKey].Value);   
            }
        }
    
        private void _handleReward(AbrStudioMessage message, Action<string, string> action)
        {
            var id = message.data[IdKey].Value;
            string reward = message.data[RewardKey].Value;

            if (action != null)
            {
                action(id, reward);   
            }
            if (_rewardAction != null)
            {
                _rewardAction(id, reward);
            }
        }
    
    }
}