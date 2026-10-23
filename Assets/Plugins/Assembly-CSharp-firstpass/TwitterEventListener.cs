using System.Collections.Generic;
using Prime31;
using UnityEngine;

public class TwitterEventListener : MonoBehaviour
{
	private void OnEnable()
	{
		TwitterManager.loginSucceededEvent += loginSucceeded;
		TwitterManager.loginFailedEvent += loginFailed;
		TwitterManager.postSucceededEvent += postSucceeded;
		TwitterManager.postFailedEvent += postFailed;
		TwitterManager.homeTimelineReceivedEvent += homeTimelineReceived;
		TwitterManager.homeTimelineFailedEvent += homeTimelineFailed;
		TwitterManager.requestDidFinishEvent += requestDidFinishEvent;
		TwitterManager.requestDidFailEvent += requestDidFailEvent;
		TwitterManager.tweetSheetCompletedEvent += tweetSheetCompletedEvent;
	}

	private void OnDisable()
	{
		TwitterManager.loginSucceededEvent -= loginSucceeded;
		TwitterManager.loginFailedEvent -= loginFailed;
		TwitterManager.postSucceededEvent -= postSucceeded;
		TwitterManager.postFailedEvent -= postFailed;
		TwitterManager.homeTimelineReceivedEvent -= homeTimelineReceived;
		TwitterManager.homeTimelineFailedEvent -= homeTimelineFailed;
		TwitterManager.requestDidFinishEvent -= requestDidFinishEvent;
		TwitterManager.requestDidFailEvent -= requestDidFailEvent;
		TwitterManager.tweetSheetCompletedEvent -= tweetSheetCompletedEvent;
	}

	private void loginSucceeded()
	{
		Debug.Log("Successfully logged in to Twitter");
	}

	private void loginFailed(string error)
	{
		Debug.Log("Twitter login failed: " + error);
	}

	private void postSucceeded()
	{
		Debug.Log("Successfully posted to Twitter");
	}

	private void postFailed(string error)
	{
		Debug.Log("Twitter post failed: " + error);
	}

	private void homeTimelineFailed(string error)
	{
		Debug.Log("Twitter HomeTimeline failed: " + error);
	}

	private void homeTimelineReceived(List<object> result)
	{
		Debug.Log("received home timeline with tweet count: " + result.Count);
		Utils.logObject(result);
	}

	private void requestDidFailEvent(string error)
	{
		Debug.Log("requestDidFailEvent: " + error);
	}

	private void requestDidFinishEvent(object result)
	{
		if (result != null)
		{
			Debug.Log("requestDidFinishEvent: " + result.GetType().ToString());
			Utils.logObject(result);
		}
		else
		{
			Debug.Log("twitterRequestDidFinishEvent with no data");
		}
	}

	private void tweetSheetCompletedEvent(bool didSucceed)
	{
		Debug.Log("tweetSheetCompletedEvent didSucceed: " + didSucceed);
	}
}
