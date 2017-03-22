using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour {

	[SerializeField]
	string m_GameLevel;

	public void LoadGameScene ()
	{			
		SceneManager.LoadScene(m_GameLevel);
	}
}
