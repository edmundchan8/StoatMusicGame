using UnityEngine;
using System.Collections;
//TODO Need to get this to work from Level Manager script instead
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour 
{
	[Header ("Accessor")]
	[SerializeField]
	GameObject m_PauseCanvas;
	[SerializeField]
	GameObject m_GameBackground;
	[SerializeField]
	MusicManager m_MusicManager;
	[SerializeField]
	Timer m_Timer = new Timer();
	float LERP_DURATION = 4f;
	[SerializeField]
	int m_CurrentLevel = 1;
	Vector2 m_BackgroundPos;
	Vector2 m_StartLerpPos;
	[SerializeField]
	GameObject m_Rabbit;
	public GameOverScript m_GameOverScript;

	[SerializeField]
	bool m_Pause = false;

	//Make an instance of Gameobject so we can call this from anywhere
	public static GameManager instance;

	void Awake()
	{
		if (!instance)
		{
			instance = this;
			DontDestroyOnLoad(instance);
		}
		else
		{
			Destroy(instance);
		}
	}

	void Start () 
	{
		m_PauseCanvas = GameObject.Find("PauseCanvas");
		m_MusicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
		m_PauseCanvas.SetActive(m_Pause);
		m_BackgroundPos = m_GameBackground.transform.localPosition;
		m_StartLerpPos = m_BackgroundPos;
		m_BackgroundPos.x = GetGameBackgroundPos();
	}

	void Update()
	{
		m_Timer.Update(Time.deltaTime);
		if (!m_Timer.HasCompleted())
		{
			m_GameBackground.transform.localPosition = Vector2.Lerp(m_StartLerpPos, m_BackgroundPos, (LERP_DURATION - m_Timer.GetTimer()) / LERP_DURATION);
			if (m_Timer.GetTimer() <= 0f)
			{
				InstantiateRabbit();
			}
		}
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
		m_MusicManager.PauseMusic();
	}

	public void PlayGameAndMusic()
	{
		Time.timeScale = 1f;
		m_MusicManager.PlayMusic();
	}

	//TODO Need to get this to work from Level Manager script instead
	public void ReturnToTitle()
	{
		PlayGameAndMusic();
		SceneManager.LoadScene("Menu");
	}

	public void IncrementCurrentLevel()
	{
		m_CurrentLevel++;
		m_StartLerpPos = m_BackgroundPos;
		m_BackgroundPos.x = GetGameBackgroundPos();
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
		return Instantiate(m_Rabbit, transform.position, transform.rotation) as GameObject;
	}
}
