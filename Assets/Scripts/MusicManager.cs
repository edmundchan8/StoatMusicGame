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
	GameObject m_GameOverText;
	[SerializeField]
	StoatScript m_StoatScript;

	[Header ("Music Constants")]
	//Because the instantiate gameobject is further away from where we want the touch the note, instantiate the music note 2.6f early.
	float NOTE_INSTANTIATE_OFFSET = 2.6f;
	[SerializeField]
	float DELAY_INSTANTIATE_DURATION = 0.1f;
	float MUSIC_FADE_DURATION = 4f;

	[Header("AudioClips")]
	[SerializeField]
	AudioClip MENU_AUDIO;
	[SerializeField]
	AudioClip LEVEL1_AUDIO;
	[SerializeField]
	AudioClip LEVEL2_AUDIO;
	[SerializeField]
	AudioClip LEVEL3_AUDIO;

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

	public enum eLevelMusic
	{
		Splash, Menu, Level1, Level2, Level3
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
		//At the start of the game, put all values from the EASY_LEVEL_01 into the List for m_MusicPlayTimeList
		for (int i = 0; i < m_MusicTimeText.Length; i++)
		{
			m_MusicPlayTimeList.Add(float.Parse(m_MusicTimeText[i]));
		}
	}

	void Update() 
	{
		if (LevelManager.instance.GetCurrentScene() == 1)
		{
			m_MusicState = eLevelMusic.Menu;
		}

		if (m_MusicState == eLevelMusic.Menu)
		{
			m_Audiosource.clip = MENU_AUDIO;
			if (!m_Audiosource.isPlaying)
			{
				print(m_Audiosource.isPlaying);
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
			}

			if (m_GameOverText == null)
			{
				print("no gameover");
				m_GameOverText = GameObject.FindObjectOfType<GameOverScript>().gameObject;
			}

			m_MusicTimer.Update(Time.deltaTime);
			if (!m_MusicTimer.HasCompleted())
			{
				m_Audiosource.volume *= m_MusicTimer.GetTimer()/MUSIC_FADE_DURATION;
			}

			float audioTime = m_Audiosource.time;

			if (m_CanInstantiateNote && !IsGameOver())
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

	}

	public void SetCanInstantiateTrue () 
	{
		m_CanInstantiateNote = true;
	}

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

	public List<float> GetList()
	{
		return m_MusicPlayTimeList;
	}

	public void FadeOutMusic()
	{
		m_MusicTimer.SetTimer(MUSIC_FADE_DURATION);
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

	public bool IsGameOver()
	{ 
		return m_GameOverText.activeInHierarchy;
	}

	public bool AudioPlaying()
	{
		return m_Audiosource.volume > 0f;
	}
}