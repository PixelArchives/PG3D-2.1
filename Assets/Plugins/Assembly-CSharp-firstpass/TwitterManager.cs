using System;
using System.Collections.Generic;
using Prime31;
using UnityEngine;

public class TwitterManager : MonoBehaviour
{
	public static event Action loginSucceededEvent;

	public static event Action<string> loginFailedEvent;

	public static event Action postSucceededEvent;

	public static event Action<string> postFailedEvent;

	public static event Action<List<object>> homeTimelineReceivedEvent;

	public static event Action<string> homeTimelineFailedEvent;

	public static event Action<object> requestDidFinishEvent;

	public static event Action<string> requestDidFailEvent;

	public static event Action<bool> tweetSheetCompletedEvent;

	static TwitterManager()
	{
		AbstractManager.initialize(typeof(TwitterManager));
	}

	public void twitterLoginSucceeded(string empty)
	{
		if (TwitterManager.loginSucceededEvent != null)
		{
			TwitterManager.loginSucceededEvent();
		}
	}

	public void twitterLoginDidFail(string error)
	{
		if (TwitterManager.loginFailedEvent != null)
		{
			TwitterManager.loginFailedEvent(error);
		}
	}

	public void twitterPostSucceeded(string empty)
	{
		if (TwitterManager.postSucceededEvent != null)
		{
			TwitterManager.postSucceededEvent();
		}
	}

	public void twitterPostDidFail(string error)
	{
		if (TwitterManager.postFailedEvent != null)
		{
			TwitterManager.postFailedEvent(error);
		}
	}

	public void twitterHomeTimelineDidFail(string error)
	{
		if (TwitterManager.homeTimelineFailedEvent != null)
		{
			TwitterManager.homeTimelineFailedEvent(error);
		}
	}

	public void twitterHomeTimelineDidFinish(string results)
	{
		if (TwitterManager.homeTimelineReceivedEvent != null)
		{
			List<object> obj = results.listFromJson();
			TwitterManager.homeTimelineReceivedEvent(obj);
		}
	}

	public void twitterRequestDidFinish(string results)
	{
		if (TwitterManager.requestDidFinishEvent != null)
		{
			TwitterManager.requestDidFinishEvent(Json.jsonDecode(results, true));
		}
	}

	public void twitterRequestDidFail(string error)
	{
		if (TwitterManager.requestDidFailEvent != null)
		{
			TwitterManager.requestDidFailEvent(error);
		}
	}

	public void tweetSheetCompleted(string oneOrZero)
	{
		if (TwitterManager.tweetSheetCompletedEvent != null)
		{
			TwitterManager.tweetSheetCompletedEvent(oneOrZero == "1");
		}
	}
}
