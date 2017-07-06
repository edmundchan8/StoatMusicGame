using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour 
{
	[SerializeField]
	bool m_CanInstantiateNote = false;

	//Position where the notes to touch are generated
	[SerializeField]
	GameObject m_NoteInstantiatePosition;

	//The note prefab to instantiate
	[SerializeField]
	GameObject m_Notes;

	[Header ("Accessor")]
	//Textasset notepad files to hold the length of the List, and the times which the instantiate the play notes
	[SerializeField]
	TextAsset EASY_LEVEL_01;
	[SerializeField]
	string[] m_MusicTimeText = new string[] {};
	[SerializeField]
	AudioSource m_Audiosource;
	[SerializeField]
	GameObject m_GameOverText;

	//List to hold the music note instantiate times
	[SerializeField]
	public List<float> m_MusicPlayTimeList = new List<float>();
	[SerializeField]
	int m_NoteIndexToPlay = 0;
	//Because the instantiate gameobject is further away from where we want the touch the note, instantiate the music note 2.4f early.
	[SerializeField]
	float NOTE_INSTANTIATE_OFFSET = 2.6f;

	[SerializeField]
	float DELAY_INSTANTIATE_DURATION = 0.1f;

	[SerializeField]
	float FADE_VOLUME_AMOUNT = 0.02f;

	void Awake()
	{	//TODO: Switch statement later.  Depending on the level, set the LEVEL_TEXT To use 
		m_MusicTimeText = EASY_LEVEL_01.text.Split('\n');
	}

	void Start()
	{
		m_GameOverText = GameObject.FindObjectOfType<GameOverScript>().gameObject;
		if (m_GameOverText == null)
		{
			return;
		}
		//At the start of the game, put all values from the EASY_LEVEL_01 into the List for m_MusicPlayTimeList
		for (int i = 0; i < m_MusicTimeText.Length; i++)
		{
			m_MusicPlayTimeList.Add(float.Parse(m_MusicTimeText[i]));
		}
	}

	void Update() 
	{
		float audioTime = m_Audiosource.time;

		if (m_CanInstantiateNote && !IsGameOver())
		{
			//n.b. music note takes 2.4f seconds to get to target from instantiate point.
			if (m_NoteIndexToPlay < m_MusicTimeText.Length)
			{
				//take current music time to check against from the music list, minus instantiate offset time, convert to 2 decimal places
				float InstantiateMusicNoteTime = (m_MusicPlayTimeList[m_NoteIndexToPlay] - NOTE_INSTANTIATE_OFFSET);//.ToString("F2");
				//round the current audio time to 2 decimal places, then output to 2 decimal places
				float RoundedCurrentAudioTime = (float)System.Math.Round(audioTime, 2);//.ToString("F2");
				print(InstantiateMusicNoteTime + " InstantiateMusicNoteTime");
				print(RoundedCurrentAudioTime + " RoundedCurrentAudioTime");

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
				print("End of music" + Time.time);
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
		if (m_Audiosource.volume > 0)
		{
			m_Audiosource.volume -= FADE_VOLUME_AMOUNT;
		}
	}

	public bool IsGameOver()
	{ 
		return m_GameOverText.activeInHierarchy;
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
}