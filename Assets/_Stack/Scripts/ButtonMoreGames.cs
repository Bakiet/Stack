using UnityEngine;
using System.Collections;

/// <summary>
/// Attached to the button more games. 
/// </summary>
public class ButtonMoreGames : AppAdvisoryButonBase
{
	/// <summary>
	/// URL for iOS device, change the URL
	/// </summary>
	public string moreGamesIosURL = "http://barouch.fr/moregames.php";
	/// <summary>
	/// URL for Android device, change the URL
	/// </summary>
	public string moreGamesAndroidURL = "http://barouch.fr/moregames.php";
	/// <summary>
	/// URL for other platforms, change the URL
	/// </summary>
	public string moreGamesOtherPlatformsURL = "http://barouch.fr/moregames.php";
	/// <summary>
	/// Call when the button is pressed
	/// </summary>
	public override void OnClicked()
	{
		if(Application.platform == RuntimePlatform.IPhonePlayer )
		{
			Application.OpenURL(moreGamesIosURL);
		}
		else if(Application.platform == RuntimePlatform.Android)
		{
			Application.OpenURL(moreGamesAndroidURL);
		}
		else
		{
			Application.OpenURL(moreGamesOtherPlatformsURL);
		}
	}
}
