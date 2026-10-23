using System.Collections.Generic;
using Prime31;
using UnityEngine;

public class TwitterGUIManager : MonoBehaviourGUI
{
	public bool canUseTweetSheet;

	private void Start()
	{
		canUseTweetSheet = TwitterBinding.isTweetSheetSupported() && TwitterBinding.canUserTweet();
		ScreenCapture.CaptureScreenshot(FacebookGUIManager.screenshotFilename);
	}

	private void completionHandler(string error, object result)
	{
		if (error != null)
		{
			Debug.LogError(error);
		}
		else
		{
			Utils.logObject(result);
		}
	}

	private void OnGUI()
	{
		beginColumn();
		if (GUILayout.Button("Initialize Twitter"))
		{
			TwitterBinding.init("INSERT_YOUR_INFO_HERE", "INSERT_YOUR_INFO_HERE");
		}
		if (GUILayout.Button("Login with Oauth"))
		{
			TwitterBinding.showOauthLoginDialog();
		}
		if (GUILayout.Button("Logout"))
		{
			TwitterBinding.logout();
		}
		if (GUILayout.Button("Is Logged In?"))
		{
			bool flag = TwitterBinding.isLoggedIn();
			Debug.Log("Twitter is logged in: " + flag);
		}
		if (GUILayout.Button("Logged in Username"))
		{
			string text = TwitterBinding.loggedInUsername();
			Debug.Log("Twitter username: " + text);
		}
		endColumn(true);
		if (GUILayout.Button("Post Status Update"))
		{
			TwitterBinding.postStatusUpdate("im posting this from Unity: " + Time.deltaTime);
		}
		if (GUILayout.Button("Post Status Update + Image"))
		{
			string pathToImage = Application.persistentDataPath + "/" + FacebookGUIManager.screenshotFilename;
			TwitterBinding.postStatusUpdate("I'm posting this from Unity with a fancy image: " + Time.deltaTime, pathToImage);
		}
		if (canUseTweetSheet && GUILayout.Button("Show Tweet Sheet"))
		{
			string pathToImage2 = Application.persistentDataPath + "/" + FacebookGUIManager.screenshotFilename;
			TwitterBinding.showTweetComposer("I'm posting this from Unity with a fancy image: " + Time.deltaTime, pathToImage2);
		}
		if (GUILayout.Button("Custom Request"))
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("status", "word up with a boogie boogie update");
			TwitterBinding.performRequest("POST", "1.1/statuses/update.json", dictionary);
		}
		endColumn(false);
		if (bottomRightButton("Sharing..."))
		{
			Application.LoadLevel("SharingTestScene");
		}
	}
}
