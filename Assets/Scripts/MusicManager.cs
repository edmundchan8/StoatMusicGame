using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour 
{
	[Header ("Accessor")]
	//Textasset notepad files to hold the length of the List, and the times which the instantiate the play notes
	[SerializeField]
	TextAsset EASY_LEVEL_01;
	[SerializeField]
	AudioSource m_Audiosource;
	[SerializeField]
	GameOverScript m_GameOverScript;
	[SerializeField]
	StoatScript m_StoatScript;

	[Header ("Music Constants")]
	//Because the instantiate gameobject is further away from where we want the touch the note, instantiate the music note 2.6f early.
	float NOTE_INSTANTIATE_OFFSET = 2.6f;
	float DELAY_INSTANTIATE_DURATION = 0.3f;
	float MUSIC_FADE_AMOUNT = 0.015f;

	[Header("AudioClips")]
	[SerializeField]
	AudioClip MENU_AUDIO;
	[SerializeField]
	AudioClip LEVEL1_AUDIO;
	[SerializeField]
	AudioClip LEVEL2_AUDIO;
	[SerializeField]
	AudioClip LEVEL3_AUDIO;
	float DEFAULT_VOLUME = 0.2f;

	[Header ("Music Variables")]
	public static MusicManager instance;
	[SerializeField]
	string[] m_MusicTimeText = new string[] {};
	//List to hold the music note instantiate times
	[SerializeField]
	public List<float> m_MusicPlayTimeList = new List<float>();
	[SerializeField]
	int m_NoteIndexToPlay = 0;
	//The note prefab to instantiate
	[SerializeField]
	GameObject m_Notes;
	//Position where the notes to touch are generated
	[SerializeField]
	GameObject m_NoteInstantiatePosition;
	[SerializeField]
	Timer m_MusicTimer = new Timer();
	bool m_CanInstantiateNote = false;
	bool m_CanEndLevel = true;

	bool m_SetLevel1Music = true;

	public enum eLevelMusic
	{
		Splash, Menu, Level1, Level2, Level3, GameOver
	};

	private eLevelMusic m_MusicState;

	void Awake()
	{	
		m_MusicState = eLevelMusic.Splash;
	}

	void Start()
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

	void Update() 
	{
		if (LevelManager.instance.GetCurrentScene() == 1)
		{
			m_MusicState = eLevelMusic.Menu;
			//variable to set level 1 music is true, then other music levels = false
			m_SetLevel1Music = true;
		}
		else if (LevelManager.instance.GetCurrentScene() == 2)
		{
			if (m_GameOverScript == null)
			{
				GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
				m_GameOverScript = gameManager.GetComponent<GameManager>().ReturnGameOverScript();
			}
			m_MusicState = eLevelMusic.Level1;
			if (m_NoteInstantiatePosition == null)
			{
				GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
				m_NoteInstantiatePosition = gameManager.GetComponent<GameManager>().GetNoteInstantiatePos();
			}
		}
			
		if (m_MusicState == eLevelMusic.Menu)
		{
			m_Audiosource.clip = MENU_AUDIO;
			if (!m_Audiosource.isPlaying)
			{
				m_Audiosource.PlayOneShot(m_Audiosource.clip);
			}
		}

		if (m_MusicState != eLevelMusic.Menu && m_MusicState != eLevelMusic.Splash)
		{
			m_StoatScript = GameObject.Find("Stoat").GetComponent<StoatScript>();
			if (m_MusicState == eLevelMusic.Level1)
			{
				m_Audiosource.clip = LEVEL1_AUDIO;
				m_MusicTimeText = EASY_LEVEL_01.text.Split('\n');
				//At the start of the game, put all values from the EASY_LEVEL_01 into the List for m_MusicPlayTimeList
				if(m_SetLevel1Music)
				{
					for (int i = 0; i < m_MusicTimeText.Length; i++)
					{
						m_MusicPlayTimeList.Add(float.Parse(m_MusicTimeText[i]));
					}
					m_SetLevel1Music = false;
					GameObject touchPanel = GameObject.FindGameObjectWithTag("TouchPanel");
					touchPanel.GetComponent<TouchPanel>().SetMusicList(m_MusicPlayTimeList);
				}	
			}
		}

		//Music Timer
		m_MusicTimer.Update(Time.deltaTime);
		if (!m_MusicTimer.HasCompleted())
		{
			m_Audiosource.volume -= MUSIC_FADE_AMOUNT;
		}

		float audioTime = m_Audiosource.time;

		if (m_CanInstantiateNote && !LevelManager.instance.IsGameOver())
		{
			//n.b. music note takes 2.4f seconds to get to target from instantiate point.
			if (m_NoteIndexToPlay < m_MusicTimeText.Length)
			{
				//take current music time to check against from the music list, minus instantiate offset time, convert to 2 decimal places
				float InstantiateMusicNoteTime = (m_MusicPlayTimeList[m_NoteIndexToPlay] - NOTE_INSTANTIATE_OFFSET);
				//round the current audio time to 2 decimal places, then output to 2 decimal places
				float RoundedCurrentAudioTime = (float)System.Math.Round(audioTime, 2);
				//Comparing both STRINGS from above
				if (RoundedCurrentAudioTime >= InstantiateMusicNoteTime)
				{
					m_NoteIndexToPlay++;
					InstantiateMusicNotes();
					//This is not good, need to fix this as it is causinng very close music taps to not instantiate
					Invoke("SetCanInstantiateTrue", DELAY_INSTANTIATE_DURATION);
				}
			}
			else if (m_NoteIndexToPlay >= m_MusicTimeText.Length)
			{
				//End of music, stoat jumps to kill rabbit
				if (m_CanEndLevel)
				{
					m_StoatScript.EndOfLevel();
					m_CanEndLevel = false;
				}
			}
		}
	}

	public void SetCanInstantiateTrue () 
	{
		m_CanInstantiateNote = true;
	}

	//TODO: This isn't being called sometimes
	void InstantiateMusicNotes () 
	{
		m_CanInstantiateNote = false;
		GameObject theNote = Instantiate(m_Notes, m_NoteInstantiatePosition.transform.position, transform.rotation) as GameObject;
		theNote.transform.SetParent(m_NoteInstantiatePosition.transform, false);
	}

	public float GetCurrentMusicTime()
	{
		return m_Audiosource.time;
	}

	//TODO:Unsure if still needed.  Now, our game will create the music list then send it to the touchpanel script.
	public List<float> GetList()
	{
		return m_MusicPlayTimeList;
	}

	public void FadeOutMusic()
	{
		m_MusicTimer.SetTimer(MUSIC_FADE_AMOUNT);
	}

	public void PauseMusic()
	{
		m_Audiosource.Pause();
	}

	public void PlayMusic()
	{
		m_Audiosource.Play();
		SetCanInstantiateTrue();
	}

	public void StopMusic()
	{
		m_Audiosource.Stop();
	}

	public bool AudioPlaying()
	{
		return m_Audiosource.volume > 0f;
	}

	public void SetStateToGameOver()
	{
		m_MusicState = eLevelMusic.GameOver;
	}

	public void SetMusicTimerZero()
	{
		Invoke("SetMusicVolume", 0.5f);
	}

	public void SetMusicVolume()
	{
		m_Audiosource.volume = DEFAULT_VOLUME;
	}
}