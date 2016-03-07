using UnityEngine;
using System.Collections;

/// <summary>
/// Attached to the button share 
/// </summary>
public class ButtonShare : AppAdvisoryButonBase
{
	public string URL = "http://app-advisory.com";

	public Texture2D textureForPost;

	public override void OnClicked()
	{
		int bestScore = PlayerPrefs.GetInt("BEST_SCORE");

		string s = "My best score: " + bestScore + ". Get it for free: " + URL;


		NativeShare m = GetComponent<NativeShare>();

		if(m == null)
		{
			gameObject.AddComponent<NativeShare>();
		}

		m = GetComponent<NativeShare>();

		m.Share("COLOR SWIPE","",URL,s);
//		UM_ShareUtility.ShareMedia("Color swipe","My best score: " + bestScore + ". Get it for free: " + "http://barouch.fr/colorswipe.php",textureForPost);
	}
}
