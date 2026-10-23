using System.Collections;
using System.Collections.Generic;
using System.IO;
using Prime31;
using UnityEngine;

public class FacebookGUIManager : MonoBehaviourGUI
{
	public GameObject cube;

	private string _userId;

	private bool _canUserUseFacebookComposer;

	private bool _hasPublishPermission;

	private bool _hasPublishActions;

	public static string screenshotFilename = "someScreenshot.png";

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

	private void Start()
	{
		FacebookManager.graphRequestCompletedEvent += delegate(object result)
		{
			Utils.logObject(result);
		};
		FacebookManager.sessionOpenedEvent += delegate
		{
			_hasPublishPermission = FacebookBinding.getSessionPermissions().Contains("publish_stream");
			_hasPublishActions = FacebookBinding.getSessionPermissions().Contains("publish_actions");
		};
		FacebookManager.reauthorizationSucceededEvent += delegate
		{
			_hasPublishPermission = FacebookBinding.getSessionPermissions().Contains("publish_stream");
			_hasPublishActions = FacebookBinding.getSessionPermissions().Contains("publish_actions");
		};
		ScreenCapture.CaptureScreenshot(screenshotFilename);
		_canUserUseFacebookComposer = FacebookBinding.canUserUseFacebookComposer();
	}

	private void OnGUI()
	{
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		beginColumn();
		if (GUILayout.Button("Initialize Facebook"))
		{
			FacebookBinding.init();
		}
		if (GUILayout.Button("Login"))
		{
			string[] permissions = new string[1] { "email" };
			FacebookBinding.loginWithReadPermissions(permissions);
		}
		if (GUILayout.Button("Reauth with Publish Permissions"))
		{
			string[] permissions2 = new string[2] { "publish_actions", "publish_stream" };
			FacebookBinding.reauthorizeWithPublishPermissions(permissions2, FacebookSessionDefaultAudience.OnlyMe);
		}
		if (GUILayout.Button("Enable Frictionless Requests"))
		{
			FacebookBinding.enableFrictionlessRequests();
		}
		if (GUILayout.Button("Logout"))
		{
			FacebookBinding.logout();
		}
		if (GUILayout.Button("Is Session Valid?"))
		{
			bool flag = FacebookBinding.isSessionValid();
			Debug.Log("Facebook is session valid: " + flag);
		}
		if (GUILayout.Button("Get Access Token"))
		{
			string accessToken = FacebookBinding.getAccessToken();
			Debug.Log("access token: " + accessToken);
		}
		if (GUILayout.Button("Get Granted Permissions"))
		{
			List<object> sessionPermissions = FacebookBinding.getSessionPermissions();
			foreach (object item in sessionPermissions)
			{
				Debug.Log(item);
			}
		}
		endColumn(true);
		if (toggleButtonState("Show OG Buttons"))
		{
			secondColumnButtonsGUI();
		}
		else
		{
			secondColumnAdditionalButtonsGUI();
		}
		toggleButton("Show OG Buttons", "Toggle Buttons");
		endColumn(false);
		if (bottomRightButton("Twitter..."))
		{
			Application.LoadLevel("TwitterTestScene");
		}
	}

	private void secondColumnButtonsGUI()
	{
		if (_hasPublishPermission)
		{
			if (GUILayout.Button("Post Image"))
			{
				string text = Application.persistentDataPath + "/" + screenshotFilename;
				if (!File.Exists(text))
				{
					Debug.LogError("there is no screenshot avaialable at path: " + text);
					return;
				}
				byte[] image = File.ReadAllBytes(text);
				Facebook.instance.postImage(image, "im an image posted from iOS", completionHandler);
			}
			if (GUILayout.Button("Post Message"))
			{
				Facebook.instance.postMessage("im posting this from Unity: " + Time.deltaTime, completionHandler);
			}
			if (GUILayout.Button("Post Message & Extras"))
			{
				Facebook.instance.postMessageWithLinkAndLinkToImage("link post from Unity: " + Time.deltaTime, "http://prime31.com", "Prime31 Studios", "http://prime31.com/assets/images/prime31logo.png", "Prime31 Logo", completionHandler);
			}
		}
		else
		{
			GUILayout.Label("Reauthorize with publish_stream permissions to show posting buttons");
		}
		if (GUILayout.Button("Graph Request (me)"))
		{
			Facebook.instance.graphRequest("me", HTTPVerb.GET, delegate(string error, object obj)
			{
				if (error == null && obj != null)
				{
					Hashtable hashtable = obj as Hashtable;
					_userId = hashtable["id"].ToString();
					Debug.Log("me Graph Request finished: ");
					Utils.logObject(hashtable);
				}
			});
		}
		if (GUILayout.Button("Show stream.publish Dialog"))
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("link", "http://prime31.com");
			dictionary.Add("name", "link name goes here");
			dictionary.Add("picture", "http://prime31.com/assets/images/prime31logo.png");
			dictionary.Add("caption", "the caption for the image is here");
			Dictionary<string, string> options = dictionary;
			FacebookBinding.showDialog("stream.publish", options);
		}
		if (GUILayout.Button("Get Friends"))
		{
			Facebook.instance.getFriends(completionHandler);
		}
		if (_canUserUseFacebookComposer && GUILayout.Button("Show Facebook Composer"))
		{
			string text2 = Application.persistentDataPath + "/" + screenshotFilename;
			if (!File.Exists(text2))
			{
				text2 = null;
			}
			FacebookBinding.showFacebookComposer("I'm posting this from Unity with a fancy image: " + Time.deltaTime, text2, "http://prime31.com");
		}
	}

	private void secondColumnAdditionalButtonsGUI()
	{
		if (_hasPublishActions)
		{
			if (GUILayout.Button("Post Score"))
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("score", "5677");
				Dictionary<string, object> parameters = dictionary;
				Facebook.instance.graphRequest("me/scores", HTTPVerb.POST, parameters, completionHandler);
			}
			if (GUILayout.Button("Post Achievement via Open Graph"))
			{
				Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
				dictionary2.Add("achievement", "http://www.friendsmash.com/opengraph/achievement_50.html");
				Facebook.instance.graphRequest("me/achievements", HTTPVerb.POST, dictionary2, completionHandler);
			}
			if (GUILayout.Button("Post Open Graph Action"))
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("profile", "http://samples.ogp.me/390580850990722");
				Dictionary<string, object> parameters2 = dictionary;
				Facebook.instance.graphRequest("me/testiostestapp:smash", HTTPVerb.POST, parameters2, completionHandler);
			}
		}
		else
		{
			GUILayout.Label("Reauthorize with publish_actions permissions to show action buttons");
		}
		if (GUILayout.Button("Custom Graph Request: platform/posts"))
		{
			Facebook.instance.graphRequest("platform/posts", HTTPVerb.GET, completionHandler);
		}
		if (GUILayout.Button("Get Scores for me"))
		{
			Facebook.instance.getScores("me", completionHandler);
		}
		if (_userId != null)
		{
			if (GUILayout.Button("Show Profile Image"))
			{
				StartCoroutine(fetchAndShowProfileImage());
			}
		}
		else
		{
			GUILayout.Label("Call the me Graph request to show user specific buttons");
		}
	}

	private IEnumerator fetchAndShowProfileImage()
	{
		string url = string.Format("http://graph.facebook.com/{0}/picture?type=large", _userId);
		Debug.Log("fetching profile image from url: " + url);
		WWW www = new WWW(url);
		yield return www;
		if (www.error != null)
		{
			Debug.Log("Error attempting to load profile image: " + www.error);
			yield break;
		}
		Debug.Log("got texture: " + www.texture);
		cube.GetComponent<Renderer>().material.mainTexture = www.texture;
	}
}
