using UnityEngine;
using System.Collections;

/// <summary>
/// Attached to like button
/// </summary>
public class ButtonLike : AppAdvisoryButonBase 
{
	public string facebookApp = "fb://profile/515431001924232" ;
	public string facebookAddress = "https://www.facebook.com/appadvisory";

	public override void OnClicked()
	{
		print("OnClicked " + name);
		float startTime;
		startTime = Time.timeSinceLevelLoad;

		//open the facebook app
		Application.OpenURL(facebookApp);

		if (Time.timeSinceLevelLoad - startTime <= 1f)
		{
			//fail. Open safari.
			Application.OpenURL(facebookAddress);
		}
	}
}
