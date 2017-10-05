using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour 
{
	public static LevelManager instance;

	[Header("CONST")]
	[SerializeField]
	float DURATION_TILL_LOAD = 2f;

	[Header ("Variables")]
	[SerializeField]
	int m_CurrentScene;
	[SerializeField]
	string m_LevelToLoad;

	//check if levelmanager exist, if it does, destroy, else set this to instance
	void Awake()
	{
		if (instance)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}
		
	void Update()
	{
		m_CurrentScene = SceneManager.GetActiveScene().buildIndex;
		if (Time.timeSinceLevelLoad > DURATION_TILL_LOAD && m_CurrentScene == 0)
		{
			m_LevelToLoad = "Menu";
			LoadNextLevel(m_LevelToLoad);
		}
	}
		
	public void LoadNextLevel(string level)
	{	
		SceneManager.LoadScene(level);
	}
}
