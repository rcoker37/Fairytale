using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

	public GameObject loadingOverlay;

	public string FirstLevelName = "Hub";

	private void Start()
	{
	}

	public void Continue()
	{
		loadingOverlay.SetActive(true);
	}

	public void NewGame()
	{

		loadingOverlay.SetActive(true);
		//load intro
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
