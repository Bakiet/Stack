using UnityEngine;
using System.Collections;
using DG.Tweening;

/// <summary>
/// Attached to the "GameManager" Game Object in the hierarchy. In charge to all the logic of the game
/// </summary>
public class GameManager : AppAdvisoryHelper
{
	/// <summary>
	/// Cube prefab spawn during the game
	/// </summary>
	public GameObject cubePrefab;
	/// <summary>
	/// Reference to the material use for the background
	/// </summary>
	public Material materialBackground;
	/// <summary>
	/// Reference to the material use for the foreground
	/// </summary>
	public Material materialForeground;
	/// <summary>
	/// The scale Y use for the cube
	/// </summary>
	public float scaleY = 0.15f;
	/// <summary>
	/// How many step to change the main color
	/// </summary>
	int stepColor = 7;


	public AudioClip soundCut;
	public AudioClip soundFail;

	int _count = 0;
	int count
	{
		get
		{
			return _count;
		}
		set
		{
			canvasManager.SetScore(value);
			_count = value;
		}
	}

	Tweener tween;

	bool isSpawned
	{
		get
		{
			return this.tween != null;
		}
	}

	bool isGameOver = false;


	public Transform lastCube;

	public Color colorFrom;
	public Color colorTo;

	void Awake()
	{
		DOTween.Init();

		colorFrom = Utils.GetRandomColor();
		colorTo = Utils.GetRandomColor();

		lastCube.GetComponent<Renderer>().material.SetColor("_Color",colorFrom);

		GameObject lastCubeClone = Instantiate(lastCube.gameObject) as GameObject;

		lastCubeClone.GetComponent<Renderer>().material.SetColor("_Color",colorFrom);

		lastCube.localScale = new Vector3(1,scaleY,1);

		isGameOver = false;
	}

	public void DoStart()
	{
		if(!isGameOver)
		{
			StopAllCoroutines();
			isGameOver = false;
			StartCoroutine("_DoStart");
		}
		else
		{
			canvasManager.DoRestart();
		}
	}

	IEnumerator _DoStart()
	{
		while(true)
		{
			if(!isSpawned)
			{
				SpawnNewCube();
			}
			else
			{
				if(Input.GetMouseButtonDown(0))
				{
					this.tween.Kill(true);
					this.tween = null;
				}
			
			}

			yield return 0;
		}
	}

	GameObject go;

	/// <summary>
	/// Create a new cube. Decide the way it will move. 
	/// </summary>
	void SpawnNewCube()
	{
		go = Instantiate(cubePrefab) as GameObject;

		if(count == 0)
		{
			lastCube.GetComponent<Renderer>().material.SetColor("_Color",colorFrom);
			colorTo = Utils.GetRandomColor();
		}
		else
		{
			if(count % stepColor == 0)
			{
				colorFrom = colorTo;
				colorTo = Utils.GetRandomColor();
			}
		}

		Color c = Color.Lerp(colorFrom,colorTo,((float)(count % stepColor))/((float)(stepColor - 1f)));
		Color c2 = c;
		c2.a = 0f;

		go.GetComponent<Renderer>().material.SetColor("_Color",c);

		materialBackground.DOColor(c,"_Color2",1);
		materialForeground.DOColor(c2,"_Color2",1);

		Vector3 p = go.transform.position;

		p = lastCube.transform.position;

		if(count%2 == 0)
		{
			p.x -= 2;
		}
		else
		{
			p.z += 2;
		}
	
		p.y = (1 + count) * scaleY;

		go.transform.position = p;

		count++;

		go.transform.localScale = lastCube.localScale;

		if(count%2 == 0)
		{
			this.tween = go.transform.DOMoveZ(-2,2).SetEase(Ease.InOutQuad).SetLoops(-1,LoopType.Yoyo)
				.OnKill(DOSize);
		}
		else
		{
			this.tween = go.transform.DOMoveX(2,2).SetEase(Ease.InOutQuad).SetLoops(-1,LoopType.Yoyo)
				.OnKill(DOSize);
		}
	}

	/// <summary>
	/// Called when the current moving cube is stoping. We well check if there is a block below. If not: Game Over, If yes, we make some calculation to cut it.
	/// </summary>
	void DOSize()
	{
		Vector3 deltaPos = go.transform.position - lastCube.transform.position;

		if(Mathf.Abs(deltaPos.z) > go.transform.localScale.z || Mathf.Abs(deltaPos.x) > go.transform.localScale.x)
		{
			isGameOver = true;

			StopAllCoroutines();

			go.AddComponent<Rigidbody>();

			canvasManager.OnGameOver();





			audioSource.PlayOneShot(soundFail);





		}
		else
		{
			Vector3 scaleSave = go.transform.localScale;

			Vector3 positionSave = go.transform.position;

			go.transform.localScale  = lastCube.localScale - new Vector3(Mathf.Abs(deltaPos.x), 0, Mathf.Abs(deltaPos.z));

			go.transform.position = lastCube.transform.position + new Vector3(deltaPos.x,2*scaleY,deltaPos.z) / 2f;

			lastCube = go.transform;

			Camera.main.transform.DOMoveY(Camera.main.transform.position.y + scaleY,2);

			GameObject fallingCube = Instantiate(cubePrefab) as GameObject;
			fallingCube.name = "fallingCube";

			fallingCube.transform.localScale = scaleSave;
			fallingCube.transform.position = positionSave;

			if(count%2 == 0)
			{
				fallingCube.transform.localScale =  new Vector3(lastCube.localScale.x, lastCube.localScale.y, Mathf.Abs(deltaPos.z));
				fallingCube.transform.position = lastCube.transform.position + Mathf.Sign(deltaPos.z) * Vector3.forward * scaleSave.z / 2f;
			}
			else
			{
				fallingCube.transform.localScale =  new Vector3(Mathf.Abs(deltaPos.x), lastCube.localScale.y, lastCube.localScale.z);
				fallingCube.transform.position = lastCube.transform.position + Mathf.Sign(deltaPos.x) * Vector3.right * scaleSave.x / 2f;
			}

			fallingCube.AddComponent<Rigidbody>();
			fallingCube.GetComponent<Renderer>().material.color = lastCube.GetComponent<Renderer>().material.color;





			audioSource.PlayOneShot(soundCut);



		}
	}
}
