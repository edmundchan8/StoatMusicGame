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
	int m_CurrentLevel = 1;
	[SerializeField]
	string m_LevelToLoad;
	bool m_IsGameOver = false;

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
		
		m_CurrentScene = GetCurrentScene();
		if (Time.timeSinceLevelLoad > DURATION_TILL_LOAD && m_CurrentScene == 0)
		{
			m_LevelToLoad = "Menu";
			LoadNextScene(m_LevelToLoad);
		}
	}
		
	public void LoadNextScene(string level)
	{	
		MusicManager.instance.StopMusic();
		SceneManager.LoadScene(level);
	}

	public int GetCurrentLevel()
	{
		return m_CurrentLevel;
	}

	public int GetCurrentScene()
	{
		return SceneManager.GetActiveScene().buildIndex;
	}

	public int IncrementLevel()
	{
		return m_CurrentLevel++;
	}

	public void RestartLevel(int level)
	{
		print("Need to take in which music to play, that will determine which level we are loading.");
		GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
		gameManager.GetComponent<GameManager>().NewGame();
		SceneManager.LoadScene("GameScene");
	}

	public void ResetLevelCount()
	{
		m_CurrentLevel = 1;
	}

	public void GameOverTrue()
	{
		//Set the state to gameover so that, when the game reloads, the state is set back to the level again adn in theory,
		//the music list should refresh
		MusicManager.instance.SetStateToGameOver();
		m_IsGameOver = true;
	}

	public void GameOverFalse()
	{
		m_IsGameOver = false;
	}

	public bool IsGameOver()
	{
		return m_IsGameOver;
	}
}
