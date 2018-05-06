using UnityEngine;

namespace AbrStudioSdk.Core
{
	public abstract class AbrStudio
	{
		private static readonly AndroidJavaClass AbrStudioClass = new AndroidJavaClass ("co.abrtech.game.unity.AbrStudioUnity");

		public static void EnableDebug()
		{
			AbrStudioClass.CallStatic("enableLog");
		}
		
		public static void Initialize(string apiKey, string sign)
		{
			Debug.Log("Initializing AbrStudio.");
			AbrStudioClass.CallStatic("initialize", apiKey, sign);
		}
	}
}
