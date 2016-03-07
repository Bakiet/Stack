using UnityEngine;
using System.Collections;

/// <summary>
/// An helper to avoid duplicate code
/// </summary>
public class AppAdvisoryHelper : MonoBehaviour
{
	private GameManager _gameManager;
	public GameManager gameManager
	{
		get
		{
			if(_gameManager == null)
				_gameManager = FindObjectOfType<GameManager>();

			return _gameManager;
		}
	}

	private CanvasManager _canvasManager;
	public CanvasManager canvasManager
	{
		get
		{
			if(_canvasManager == null)
				_canvasManager = FindObjectOfType<CanvasManager>();

			return _canvasManager;
		}
	}

	private AudioSource _audioSource;
	public AudioSource audioSource
	{
		get
		{
			if(_audioSource == null)
				_audioSource = FindObjectOfType<AudioSource>();

			return _audioSource;
		}
	}

}
