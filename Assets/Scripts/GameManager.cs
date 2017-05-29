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
	MusicManager m_MusicManager;

	[SerializeField]
	bool m_Pause = false;

	void Start () 
	{
		m_PauseCanvas = GameObject.Find("PauseCanvas");
		m_MusicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
		m_PauseCanvas.SetActive(m_Pause);
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
		SceneManager.LoadScene("Menu");
	}
}
