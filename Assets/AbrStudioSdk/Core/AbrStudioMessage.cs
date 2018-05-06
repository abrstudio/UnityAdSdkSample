using System;
using SimpleJSON;

namespace AbrStudioSdk.Core
{
    [Serializable]
    public class AbrStudioMessage
    {
        private const string RequestIdKey = "requestId";
        private const string ActionKey = "action";
        private const string EventTypeKey = "eventType";
        private const string DataKey = "data";
    
        public readonly long requestId;
        public readonly string action;
        public readonly string eventType;
        public readonly JSONClass data;

        public AbrStudioMessage()
        {
        }

        public AbrStudioMessage(JSONNode dataNode)
        {
            requestId = dataNode[RequestIdKey].AsInt;
            action = dataNode[ActionKey].Value;
            eventType = dataNode[EventTypeKey].Value;
            data = dataNode[DataKey].AsObject;
        }
    }
}