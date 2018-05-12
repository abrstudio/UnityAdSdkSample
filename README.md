# Trivial Drive

## WHAT IS THIS SAMPLE?
This project is a sample for AbrStudio Unity Ad SDK.
It's a very simple Unity application, where you can request and show video advertisement.


## HOW TO RUN THIS SAMPLE?
This sample can't be run as-is. Here is what you should do:

1. First you have to contact us for publishing your game in Iran. Visit [AbrStudio Website][website] or send an Email to [info@abrstudio.ir](mailto:info@abrstudio.ir) for more information.

[website]: http://abrstudio.ir "AbrStudio Website"

2. Get your application's credentials (including API key, Sign key and CafeBazaar public key) from us.
3. Change the sample's package name to your package name.

## WHAT DOES THIS SAMPLE DO?
This project is a sample for AbrStudio Unity Ad SDK.

### INITIALIZATION
In order to use Ad SDK you have to first initialize the SDK. The following code shows how this sample initializes the SDK.


```csharp
public class Main : MonoBehaviour {
	// ...

    // Your application's API key that you got from abrstudio.
	private const string ApiKey = "PUT_YOUR_API_KEY_HERE";

	// Your application's Sign key that you got from abrstudio.
	private const string SignKey = "PUT_YOUR_SIGN_KEY_HERE";

    public void Start()
	{
		// First of all you have to initialize the AbrStudio sdk using your app's API & Sign key.
		AbrStudio.Initialize(ApiKey, SignKey);

        // ...
	}
    // ...
}
```

### ADVERTISING SDK
Using advertising sdk has 2 main steps:
1. Requesting Ad:

    ```csharp
    private string _adId;

    private void OnGUI()
    {
        // ...

        if (Button("Request Ad"))
	    {
		    // User clicked the "Request Ad" button

		    // launching the request ad flow
		    // You will be notified of request ad result via one of the actions that you provide.
		    AbrStudioAd.RequestAd(SampleAdZoneId, adReadyAction, errorAction, noAdAction, noNetworkAction);
	    }

	    // ...
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
    ```
2. Showing Ad:

    ```csharp
    private string _adId;

    private void OnGUI()
    {
    	// ...
        if (Button("Show Ad"))
	    {
		    // User clicked the "Show Ad" button

		    // Showing the ad
		    // You can show the ad whenever you want by providing adId and result actions.
		    AbrStudioAd.Show(_adId, errorAction, adClosedAction, rewardAction);
	    }

        // ...
    }

    void errorAction(long code, string message)
	{
		// Error happened during request ad or show ad

		Debug.Log("code: " + code + ", message: " + message);
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
    ```

For more information about AbrStudio Ad SDK and how to use it, please visit [AbrStudio Website][website].
