using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameLoading : MonoBehaviour {

	[SerializeField]
	private Image LoadingBar;

	void Start () {
		LoadingBar.fillAmount = 0;
		StartCoroutine(LoadSceneAsync());
	}

	IEnumerator LoadSceneAsync()
	{
		AsyncOperation loading = SceneManager.LoadSceneAsync("V2");
		while(!loading.isDone)
		{
			yield return null;
			LoadingBar.fillAmount = loading.progress / 0.9f;
		}
	}
}
