using UnityEngine;
using System.Collections;

/// <summary>
/// Attached to tthe "big" cube in the scene. In charge to change the coor of this "big cube" according to the start color
/// </summary>
public class SetColorPodium : MonoBehaviour 
{
	void Start()
	{
		Color c = FindObjectOfType<GameManager>().lastCube.GetComponent<Renderer>().material.color;
		GetComponent<Renderer>().material.SetColor("_Color",c);
	}
}
