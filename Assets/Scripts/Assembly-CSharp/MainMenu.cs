using Prime31;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	public Texture fon;

	public Texture plashkaPodScore;

	public GUIStyle playStyle;

	public GUIStyle soundStyle;

	public GUIStyle bestScoreStyle;

	public GUIStyle facebookStyle;

	public GUIStyle gamecenterStyle;

	public GUIStyle labelStyle;

	private bool showMessagFacebook;

	private string _userId;

	private bool _canUserUseFacebookComposer;

	private bool _hasPublishPermission;

	private bool _hasPublishActions;

	private GameCenterSingleton _gc;

	public static float iOSVersion
	{
		get
		{
			float result = -1f;
			string text = SystemInfo.operatingSystem.Replace("iPhone OS ", string.Empty);
			float.TryParse(text.Substring(0, 1), out result);
			return result;
		}
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

	private void Start()
	{
		GlobalGameController.ResetParameters();
		GlobalGameController.Score = 0;
		if (iOSVersion > 5f)
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
			_canUserUseFacebookComposer = FacebookBinding.canUserUseFacebookComposer();
		}
		_gc = GameCenterSingleton.Instance;
	}

	private void OnGUI()
	{
		GUI.DrawTexture(new Rect(((float)Screen.width - 2048f * (float)Screen.height / 1154f) / 2f, 0f, 2048f * (float)Screen.height / 1154f, Screen.height), fon, ScaleMode.StretchToFill);
		if (GUI.Button(new Rect((float)(Screen.width / 2) - (float)Screen.height * 0.238f, (float)Screen.height * 0.8475f - (float)Screen.height * 0.17f, (float)Screen.height * 0.476f, (float)Screen.height * 0.17f), string.Empty, playStyle))
		{
			Application.LoadLevel("LoadingNoWait");
		}
		if (iOSVersion > 5f && GUI.Button(new Rect((float)Screen.width * 0.95f - (float)Screen.height * 0.25f, (float)Screen.height * 0.923f - (float)Screen.height * 0.0525f, (float)Screen.height * 0.105f, (float)Screen.height * 0.105f), string.Empty, facebookStyle))
		{
			if (FacebookBinding.isSessionValid())
			{
				if (_hasPublishPermission)
				{
					showMessagFacebook = true;
					Invoke("hideMessag", 3f);
					Facebook.instance.postMessage("I scored in PixlGun 3D " + PlayerPrefs.GetInt(Defs.BestScoreSett, 0) + " points! Try to beat my result! ", completionHandler);
				}
				else
				{
					string[] permissions = new string[2] { "publish_actions", "publish_stream" };
					FacebookBinding.reauthorizeWithPublishPermissions(permissions, FacebookSessionDefaultAudience.OnlyMe);
				}
			}
			else
			{
				FacebookBinding.init();
				string[] permissions2 = new string[1] { "email" };
				FacebookBinding.loginWithReadPermissions(permissions2);
			}
		}
		/*if (GUI.Button(new Rect((float)Screen.width * 0.95f - (float)Screen.height * 0.105f, (float)Screen.height * 0.923f - (float)Screen.height * 0.0525f, (float)Screen.height * 0.105f, (float)Screen.height * 0.105f), string.Empty, gamecenterStyle))
		{
			if (_gc.IsUserAuthenticated())
			{
				_gc.ShowLeaderboardUI();
			}
			else
			{
				_gc.updateGameCenter();
				MonoBehaviour.print("error Authenticated ");
			}
		}*/
		NewUtils.enableAudio = GUI.Toggle(new Rect((float)Screen.width * 0.05f, (float)Screen.height * 0.923f - (float)Screen.height * 0.0525f, (float)Screen.height * 0.105f, (float)Screen.height * 0.105f), NewUtils.enableAudio, string.Empty, soundStyle);
		bestScoreStyle.fontSize = Mathf.RoundToInt((float)Screen.height * 0.04f);
		GUI.DrawTexture(new Rect((float)(Screen.width / 2) - (float)Screen.height * 0.317f, (float)Screen.height * 0.923f - (float)Screen.height * 0.0525f, (float)Screen.height * 0.63525f, (float)Screen.height * 0.105f), plashkaPodScore);
		GUI.Label(new Rect((float)(Screen.width / 2) - (float)Screen.height * 0.272f, (float)Screen.height * 0.923f - (float)Screen.height * 0.045f, (float)Screen.height * 0.5445f, (float)Screen.height * 0.09f), "BEST SCORE " + PlayerPrefs.GetInt(Defs.BestScoreSett, 0), bestScoreStyle);
		if (showMessagFacebook)
		{
			labelStyle.fontSize = Mathf.RoundToInt((float)Screen.height * 0.03f);
			GUI.Label(new Rect((float)(Screen.width / 2) - (float)Screen.height * 0.5f, (float)Screen.height * 0.5f - (float)Screen.height * 0.0525f, Screen.height, (float)Screen.height * 0.105f), "Your best score was sent to Facebook", labelStyle);
		}
	}

	private void hideMessag()
	{
		showMessagFacebook = false;
	}
}
