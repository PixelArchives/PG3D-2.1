using System.IO;
using Prime31;
using UnityEngine;

public class SharingGUIManager : MonoBehaviourGUI
{
	public static string screenshotFilename = "someScreenshot.png";

	private void Start()
	{
		SharingManager.sharingFinishedWithActivityTypeEvent += delegate(string activityType)
		{
			Debug.Log("sharingFinishedWithActivityTypeEvent: " + activityType);
		};
		SharingManager.sharingCancelledEvent += delegate
		{
			Debug.Log("sharingCancelledEvent");
		};
		ScreenCapture.CaptureScreenshot(screenshotFilename);
	}

	private void OnGUI()
	{
		beginColumn();
		if (GUILayout.Button("Share URL and Text"))
		{
			SharingBinding.shareItems(new string[2] { "http://prime31.com", "Here is some text with the URL" });
		}
		if (GUILayout.Button("Share Screenshot"))
		{
			string text = Path.Combine(Application.persistentDataPath, screenshotFilename);
			if (!File.Exists(text))
			{
				Debug.LogError("there is no screenshot avaialable at path: " + text);
				return;
			}
			SharingBinding.shareItems(new string[1] { text });
		}
		if (GUILayout.Button("Share Screenshot and Text"))
		{
			string text2 = Path.Combine(Application.persistentDataPath, screenshotFilename);
			if (!File.Exists(text2))
			{
				Debug.LogError("there is no screenshot avaialable at path: " + text2);
				return;
			}
			SharingBinding.shareItems(new string[2] { text2, "Here is some text with the image" });
		}
		endColumn();
		if (bottomRightButton("Facebook..."))
		{
			Application.LoadLevel("FacebookTestScene");
		}
	}
}
