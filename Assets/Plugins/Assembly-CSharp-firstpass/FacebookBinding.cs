using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Prime31;
using UnityEngine;

public class FacebookBinding
{
	static FacebookBinding()
	{
		FacebookManager.preLoginSucceededEvent += delegate
		{
			Facebook.instance.accessToken = getAccessToken();
		};
	}

	[DllImport("__Internal")]
	private static extern void _facebookInit();

	public static void init()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_facebookInit();
		}
		Facebook.instance.accessToken = getAccessToken();
	}

	[DllImport("__Internal")]
	private static extern string _facebookGetAppLaunchUrl();

	public static string getAppLaunchUrl()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return _facebookGetAppLaunchUrl();
		}
		return string.Empty;
	}

	[DllImport("__Internal")]
	private static extern void _facebookSetSessionLoginBehavior(int behavior);

	public static void setSessionLoginBehavior(FacebookSessionLoginBehavior loginBehavior)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_facebookSetSessionLoginBehavior((int)loginBehavior);
		}
	}

	[DllImport("__Internal")]
	private static extern void _facebookEnableFrictionlessRequests();

	public static void enableFrictionlessRequests()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_facebookEnableFrictionlessRequests();
		}
	}

	[DllImport("__Internal")]
	private static extern void _facebookRenewCredentialsForAllFacebookAccounts();

	public static void renewCredentialsForAllFacebookAccounts()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_facebookRenewCredentialsForAllFacebookAccounts();
		}
	}

	[DllImport("__Internal")]
	private static extern bool _facebookIsLoggedIn();

	public static bool isSessionValid()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return _facebookIsLoggedIn();
		}
		return false;
	}

	[DllImport("__Internal")]
	private static extern string _facebookGetFacebookAccessToken();

	public static string getAccessToken()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return _facebookGetFacebookAccessToken();
		}
		return string.Empty;
	}

	[DllImport("__Internal")]
	private static extern string _facebookGetSessionPermissions();

	public static List<object> getSessionPermissions()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			string json = _facebookGetSessionPermissions();
			return json.listFromJson();
		}
		return new List<object>();
	}

	[DllImport("__Internal")]
	private static extern void _facebookLoginUsingDeprecatedAuthorizationFlowWithRequestedPermissions(string perms, string urlSchemeSuffix);

	[Obsolete("Note that this auth flow has been deprecated by Facebook and could be removed at any time at Facebook's discretion")]
	public static void loginUsingDeprecatedAuthorizationFlowWithRequestedPermissions(string[] permissions)
	{
		loginUsingDeprecatedAuthorizationFlowWithRequestedPermissions(permissions, null);
	}

	[Obsolete("Note that this auth flow has been deprecated by Facebook and could be removed at any time at Facebook's discretion")]
	public static void loginUsingDeprecatedAuthorizationFlowWithRequestedPermissions(string[] permissions, string urlSchemeSuffix)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			string perms = string.Join(",", permissions);
			_facebookLoginUsingDeprecatedAuthorizationFlowWithRequestedPermissions(perms, urlSchemeSuffix);
		}
	}

	public static void login()
	{
		loginWithReadPermissions(new string[0]);
	}

	public static void loginWithReadPermissions(string[] permissions)
	{
		loginWithReadPermissions(permissions, null);
	}

	[DllImport("__Internal")]
	private static extern void _facebookLoginWithRequestedPermissions(string perms, string urlSchemeSuffix);

	public static void loginWithReadPermissions(string[] permissions, string urlSchemeSuffix)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			string text = null;
			text = ((permissions != null && permissions.Length != 0) ? string.Join(",", permissions) : string.Empty);
			_facebookLoginWithRequestedPermissions(text, urlSchemeSuffix);
		}
	}

	[DllImport("__Internal")]
	private static extern void _facebookReauthorizeWithReadPermissions(string perms);

	public static void reauthorizeWithReadPermissions(string[] permissions)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			string perms = string.Join(",", permissions);
			_facebookReauthorizeWithReadPermissions(perms);
		}
	}

	[DllImport("__Internal")]
	private static extern void _facebookReauthorizeWithPublishPermissions(string perms, int defaultAudience);

	public static void reauthorizeWithPublishPermissions(string[] permissions, FacebookSessionDefaultAudience defaultAudience)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			string perms = string.Join(",", permissions);
			_facebookReauthorizeWithPublishPermissions(perms, (int)defaultAudience);
		}
	}

	[DllImport("__Internal")]
	private static extern void _facebookLogout();

	public static void logout()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_facebookLogout();
		}
		Facebook.instance.accessToken = string.Empty;
	}

	[DllImport("__Internal")]
	private static extern void _facebookShowDialog(string dialogType, string json);

	public static void showDialog(string dialogType, Dictionary<string, string> options)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_facebookShowDialog(dialogType, options.toJson());
		}
	}

	[DllImport("__Internal")]
	private static extern void _facebookRestRequest(string restMethod, string httpMethod, string jsonDict);

	public static void restRequest(string restMethod, string httpMethod, Hashtable keyValueHash)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			string text = keyValueHash.toJson();
			if (text != null)
			{
				_facebookRestRequest(restMethod, httpMethod, text);
			}
		}
	}

	[DllImport("__Internal")]
	private static extern void _facebookGraphRequest(string graphPath, string httpMethod, string jsonDict);

	public static void graphRequest(string graphPath, string httpMethod, Hashtable keyValueHash)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			string text = keyValueHash.toJson();
			if (text != null)
			{
				_facebookGraphRequest(graphPath, httpMethod, text);
			}
		}
	}

	[DllImport("__Internal")]
	private static extern bool _facebookIsFacebookComposerSupported();

	public static bool isFacebookComposerSupported()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return _facebookIsFacebookComposerSupported();
		}
		return false;
	}

	[DllImport("__Internal")]
	private static extern bool _facebookCanUserUseFacebookComposer();

	public static bool canUserUseFacebookComposer()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return _facebookCanUserUseFacebookComposer();
		}
		return false;
	}

	[DllImport("__Internal")]
	private static extern void _facebookShowFacebookComposer(string message, string imagePath, string link);

	public static void showFacebookComposer(string message)
	{
		showFacebookComposer(message, null, null);
	}

	public static void showFacebookComposer(string message, string imagePath, string link)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_facebookShowFacebookComposer(message, imagePath, link);
		}
	}
}
