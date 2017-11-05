using UnityEngine;
using System.Collections;
//TODO Need to get this to work from Level Manager script instead
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	[Header ("Accessor")]
	GameObject m_PauseCanvas;
	GameObject m_GameBackground;
	[SerializeField]
	GameObject m_Rabbit;
	GameObject m_Stoat;
	public GameOverScript m_GameOverScript;
	bool m_IsGameOver = false;
	GameObject m_NoteHolder;

	[Header ("Buttons")]
	GameObject m_PauseButton;
	GameObject m_ReturnGameButton;
	GameObject m_ReturnMenuButton;

	[Header("CONSTANTS")]
	float LERP_DURATION = 4f;

	[Header("GameManager Variables")]
	int m_CurrentLevel;
	Vector2 m_StartLerpPos;
	Vector2 m_BackgroundPos;
	bool m_CanInstantiateRabbit = true;
	bool m_Pause = false;
	bool m_NewLevel = true;

	[Header("Timer")]
	[SerializeField]
	Timer m_Timer = new Timer();

	void Update()
	{
		if (m_NewLevel)
		{
			SetNewLevel();
		}

		m_Timer.Update(Time.deltaTime);
		if (!m_Timer.HasCompleted() && m_CanInstantiateRabbit)
		{
			m_GameBackground.transform.localPosition = Vector2.Lerp(m_StartLerpPos, m_BackgroundPos, (LERP_DURATION - m_Timer.GetTimer()) / LERP_DURATION);
		}

		if (m_Timer.HasCompleted() && !IsGameOver())
		{
			if (m_CanInstantiateRabbit)
			{
				InstantiateRabbit();
			}
		}
	}

	public void SetNewLevel()
	{		
		m_NewLevel = false;
		if (GameObject.FindGameObjectWithTag("Rabbit") == null)
		{
			m_CanInstantiateRabbit = true;
		}
		if (m_Stoat == null)
		{
			m_Stoat = GameObject.FindGameObjectWithTag("Stoat");
		}
		m_CurrentLevel = LevelManager.instance.GetCurrentLevel();
		m_PauseCanvas = GameObject.FindGameObjectWithTag("PauseCanvas");
		if(m_PauseButton == null)
		{
			print("found pause button");
			m_PauseButton = GameObject.FindGameObjectWithTag("PauseButton");
		}
		if (m_ReturnGameButton == null)
		{
			m_ReturnGameButton = GameObject.FindGameObjectWithTag("ReturnGameButton");
		}

		if (m_ReturnMenuButton == null)
		{
			m_ReturnMenuButton = GameObject.FindGameObjectWithTag("MenuButton");
		}
		m_PauseButton.GetComponent<Button>().onClick.AddListener(() => OnPause());
		m_ReturnGameButton.GetComponent<Button>().onClick.AddListener(() => OnPause());
		m_ReturnMenuButton.GetComponent<Button>().onClick.AddListener(() => ReturnToTitle());
		m_PauseCanvas.SetActive(m_Pause);
		print(m_PauseCanvas.activeInHierarchy);
		if (m_GameOverScript == null)
		{
			m_GameOverScript = ReturnGameOverScript();
		}
		if(m_NoteHolder == null)
		{
			m_NoteHolder = GameObject.FindGameObjectWithTag("NoteHolder");
		}
		m_GameBackground = GameObject.FindGameObjectWithTag("GameBackGround");
		m_BackgroundPos = m_GameBackground.transform.localPosition;
		m_StartLerpPos = m_BackgroundPos;
		m_BackgroundPos.x = GetGameBackgroundPos();
	}

	public void OnPause()
	{
		m_Pause = !m_Pause;
		if (m_Pause)
		{
			PauseGameAndMusic();
		}
		else
		{
			PlayGameAndMusic();
		}
		m_PauseCanvas.SetActive(m_Pause);
	}

	public void PauseGameAndMusic()
	{
		Time.timeScale = 0f;
		MusicManager.instance.PauseMusic();
	}

	public void PlayGameAndMusic()
	{
		Time.timeScale = 1f;
		MusicManager.instance.PlayMusic();
	}

	//TODO Need to get this to work from Level Manager script instead
	public void ReturnToTitle()
	{
		PlayGameAndMusic();
		SceneManager.LoadScene("Menu");
	}

	public void SetBackgroundLerpPos()
	{
		m_CurrentLevel = LevelManager.instance.GetCurrentLevel();
		m_StartLerpPos = m_BackgroundPos;
		m_BackgroundPos.x = GetGameBackgroundPos();
		m_Stoat.GetComponent<StoatScript>().ReturnStartLerpPos();
		m_CanInstantiateRabbit = true;
	}

	public float GetGameBackgroundPos()
	{
		float pos;
		if (m_CurrentLevel == 1)
		{
			pos = 496f;
		}
		else if (m_CurrentLevel == 2)
		{
			pos = 140f;
		}
		else if (m_CurrentLevel == 3)
		{
			pos = -300;
		}
		else
		{
			Debug.Log("level count value not expected, this should not appear");
			pos = 0f;
		}
		m_Timer.SetTimer(LERP_DURATION);
		return pos;
	}
	public GameOverScript ReturnGameOverScript()
	{
		return m_GameOverScript;
	}

	public GameObject InstantiateRabbit()
	{
		m_CanInstantiateRabbit = false;
		return Instantiate(m_Rabbit, transform.position, transform.rotation) as GameObject;
	}

	public void OnLoseLevel()
	{
		GameOver();
		m_Stoat.GetComponent<StoatScript>().StopMoving();
	}

	public void GameOver()
	{
		m_IsGameOver = true;
	}

	public void NewGame()
	{
		m_IsGameOver = false;
	}

	public bool IsGameOver()
	{
		return m_IsGameOver;
	}

	public void OnRestart()
	{
		m_Pause = true;
		m_PauseCanvas.SetActive(m_Pause);
		LevelManager.instance.RestartLevel(LevelManager.instance.GetCurrentLevel());
		m_NewLevel = true;
	}

	public GameObject GetNoteInstantiatePos()
	{
		return m_NoteHolder;
	}

	public bool IsGamePaused()
	{
		return m_Pause;
	}
}
