using System;
using Prime31;

public class SharingManager : AbstractManager
{
	public static event Action<string> sharingFinishedWithActivityTypeEvent;

	public static event Action sharingCancelledEvent;

	static SharingManager()
	{
		AbstractManager.initialize(typeof(SharingManager));
	}

	private void sharingFinishedWithActivityType(string activityType)
	{
		SharingManager.sharingFinishedWithActivityTypeEvent.fire(activityType);
	}

	private void sharingCancelled(string empty)
	{
		SharingManager.sharingCancelledEvent.fire();
	}
}
