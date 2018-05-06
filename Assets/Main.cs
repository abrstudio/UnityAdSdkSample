using AbrStudioSdk.Ad;
using AbrStudioSdk.Core;
using UnityEngine;

public class Main : MonoBehaviour {

	// Your application's API key that you got from abrstudio.
	private const string ApiKey = "PUT_YOUR_API_KEY_HERE";
	
	// Your application's Sign key that you got from abrstudio.
	private const string SignKey = "PUT_YOUR_SIGN_KEY_HERE";
	
	// sample ad zone id.
	private const string SampleAdZoneId = "<PUT_YOUR_AD_ZONE_ID_HERE>";

	private string _adId;

	public void Start() 
	{
		AndroidJNIHelper.debug = false;
		
		// Just for debug mode and testing
		AbrStudio.EnableDebug(); // Comment this line for production
		
		// First of all you have to initialize the AbrStudio sdk using your app's API & Sign key.
		AbrStudio.Initialize(ApiKey, SignKey);
		
		/*
		 * You have two options for getting notified about ad reward:
		 * 	1. Setting one global reward callback action (This function)
		 * 	2. Provide callback action for each show ad separately.
		 *
		 */
		AbrStudioAd.SetRewardCallback(globalRewardAction);
	}
	
	private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10f, 10f, Screen.width - 15f, Screen.height - 15f));
        GUI.skin.button.fixedHeight = 50;
        GUI.skin.button.fontSize = 20;

        if (Button("Request Ad"))
	    {
		    // User clicked the "Request Ad" button
		    
		    // launching the request ad flow
		    // You will be notified of request ad result via one of the actions that you provide.
		    AbrStudioAd.RequestAd(SampleAdZoneId, adReadyAction, errorAction, noAdAction, noNetworkAction);
	    }
	    
	    if (Button("Show Ad"))
	    {
		    // User clicked the "Show Ad" button
		    
		    // Showing the ad
		    // You can show the ad whenever you want by providing adId and result actions.
		    AbrStudioAd.Show(_adId, errorAction, adClosedAction, rewardAction);
	    }

        GUILayout.EndArea();
    }

    bool Button(string label)
    {
        GUILayout.Space(5);
        return GUILayout.Button(label);
    }

	void adReadyAction(string adId)
	{
		// The requested ad is ready
		Debug.Log("The requested ad is ready, AdId = " + adId);

		_adId = adId;
	}

	void errorAction(long code, string message)
	{
		// Error happened during request ad or show ad
		
		Debug.Log("code: " + code + ", message: " + message);
	}

	void noAdAction()
	{
		// No ad was available for your ad request.
		
		Debug.Log("No Ad Available!");
	}

	void noNetworkAction()
	{
		// Ad request failed, because the device is not connected to internet.
		
		Debug.Log("No Network!");
	}

	void adClosedAction(bool completed)
	{
		// Ad was closed.
		// completed indicates if the video completed before close or not.
		
		Debug.Log("Ad Closed, completed: " + completed);
	}

	void rewardAction(string id, string reward)
	{
		// The ad is rewarded.
		
		Debug.Log("The ad is rewarded. (" + "adId: " + id + ", reward: " + reward + ")");
	}
	
	// The global callback action for ad rewards.
	void globalRewardAction(string id, string reward)
	{
		// The ad is rewarded.
		
		Debug.Log("The ad is rewarded. (" + "adId: " + id + ", reward: " + reward + ")");
	}
	

}
