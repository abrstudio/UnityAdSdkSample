using System;
using AbrStudioSdk.Core;

namespace AbrStudioSdk.Ad
{
    public class RequestAdCallbackData : ICallbackData
    {
        public readonly Action<string> AdReadyAction;
        public readonly Action<long, string> ErrorAction;
        public readonly Action NoAdAction;
        public readonly Action NoNetworkAction;

        public RequestAdCallbackData(Action<string> adReadyAction, Action<long, string> errorAction, Action noAdAction, Action noNetworkAction)
        {
            AdReadyAction = adReadyAction;
            ErrorAction = errorAction;
            NoAdAction = noAdAction;
            NoNetworkAction = noNetworkAction;
        }
    }
}