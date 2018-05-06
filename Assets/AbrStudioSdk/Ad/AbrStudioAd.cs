using System;
using AbrStudioSdk.Core;
using UnityEngine;

namespace AbrStudioSdk.Ad
{
    public abstract class AbrStudioAd
    {
        private static GameObject _adManager;
        private static AbrStudioMessageHandler _messageHandler;
        private static AbrStudioShowAdMessageHandler _showAdMessageHandler;

        private const string ShowAdAction = "showAd";
        private const string RequestAdAction = "requestAd";
        private const string AdManagerName = "AbrStudioAdManager";

        private static readonly AndroidJavaClass AbrStudioAdClass = new AndroidJavaClass ("co.abrtech.game.unity.ad.AbrStudioAdUnity");

        private static void Initialize(AbrStudioMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
            _messageHandler.AddHandler(RequestAdAction, new AbrStudioRequestAdMessageHandler());

            _showAdMessageHandler = new AbrStudioShowAdMessageHandler();
            _messageHandler.AddHandler(ShowAdAction, _showAdMessageHandler);
        }
        
        private static void InitAdManager()
        {
            if (_adManager != null)
            {
                return;
            }
		
            _adManager = new GameObject(AdManagerName);
            UnityEngine.Object.DontDestroyOnLoad(_adManager);
            var adMessageHandler = _adManager.AddComponent<AbrStudioMessageHandler>();
		
            Initialize(adMessageHandler);
        }
    
        public static void RequestAd(string abrZoneId, Action<string> onAdready, Action<long, string> onError, Action onNoAdAvailable, Action onNoNetwork)
        {
            if (_adManager == null)
            {
                InitAdManager();
            }
        
            int id = _messageHandler.NewItem(new RequestAdCallbackData(onAdready, onError, onNoAdAvailable, onNoNetwork));
            AbrStudioAdClass.CallStatic("requestAd", abrZoneId, id, AdManagerName, AbrStudioMessageHandler.MethodName);
        }

        public static void Show(string adId, Action<long, string> onError, Action<bool> onAdClosed, Action<string, string> onReward)
        {
            if (_adManager == null)
            {
                InitAdManager();
            }
        
            int id = _messageHandler.NewItem(new ShowAdCallbackData(onError, onAdClosed, onReward));
            AbrStudioAdClass.CallStatic("show", adId, id, AdManagerName, AbrStudioMessageHandler.MethodName);
        }

        public static void SetRewardCallback(Action<string, string> rewardAction)
        {
            if (_adManager == null)
            {
                InitAdManager();
            }
            _showAdMessageHandler.SetRewardCallback(rewardAction);
        }
    
    }
}