using System.Runtime.InteropServices;
using Prime31;
using UnityEngine;

public class SharingBinding
{
	[DllImport("__Internal")]
	private static extern void _sharingShareItems(string items, string excludedActivityTypes);

	public static void shareItems(string[] items)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_sharingShareItems(Json.jsonEncode(items), null);
		}
	}

	public static void shareItems(string[] items, string[] excludedActivityTypes)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_sharingShareItems(Json.jsonEncode(items), Json.jsonEncode(excludedActivityTypes));
		}
	}
}
