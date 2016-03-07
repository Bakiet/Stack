using UnityEngine;
using System.Collections;

/// <summary>
/// To change the alpha of the podium start
/// </summary>
public class SetAlphaPodium : MonoBehaviour
{
	public float alpha = 1f;

	void Start()
	{
		Color c = FindObjectOfType<GameManager>().lastCube.GetComponent<Renderer>().material.color;
		c.a = alpha;
		GetComponent<Renderer>().material.SetColor("_Color",c);
	}
}
