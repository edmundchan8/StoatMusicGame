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
	int m_CurrentScene = 1;
	int m_CurrentLevel;
	[SerializeField]
	string m_LevelToLoad;

	//check if levelmanager exist, if it does, destroy, else set this to instance
	void Awake()
	{
		ResetLevelCount();
		if (!instance)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
		
	void Update()
	{
		m_CurrentScene = SceneManager.GetActiveScene().buildIndex;
		if (Time.timeSinceLevelLoad > DURATION_TILL_LOAD && m_CurrentScene == 0)
		{
			m_LevelToLoad = "Menu";
			LoadNextScene(m_LevelToLoad);
		}
	}
		
	public void LoadNextScene(string level)
	{	
		SceneManager.LoadScene(level);
	}

	public int GetCurrentLevel()
	{
		return m_CurrentLevel;
	}

	public int IncrementLevel()
	{
		return m_CurrentLevel++;
	}

	public void RestartLevel(int level)
	{
		print("Need to take in which music to play, that will determine which level we are loading.");
		SceneManager.LoadScene("GameScene");
	}

	public void ResetLevelCount()
	{
		m_CurrentLevel = 1;
	}
}
