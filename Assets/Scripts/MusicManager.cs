using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour 
{
	[SerializeField]
	bool m_CanInstantiateNote = false;

	//Position where the notes to touch are generated
	[SerializeField]
	GameObject m_NoteHolderPosition;

	//The note prefab to instantiate
	[SerializeField]
	GameObject m_Notes;

	[Header ("Accessor")]
	//Textasset notepad files to hold the length of the List, and the times which the instantiate the play notes
	[SerializeField]
	TextAsset EASY_LEVEL_01;
	[SerializeField]
	string[] m_TextLines = new string[] {};
	[SerializeField]
	AudioSource m_Audiosource;

	//List to hold the music note instantiate times
	[SerializeField]
	public List<float> PlayNotePos = new List<float>();
	[SerializeField]
	int NoteArrayToPlay = 0;
	//Because the instantiate gameobject is further away from where we want the touch the note, instantiate the music note 2.6f early.
	[SerializeField]
	float NOTE_INSTANTIATE_OFFSET = 2.6f;

	[SerializeField]
	float DELAY_INSTANTIATE_DURATION = 0.3f;

	void Awake()
	{//TODO: Switch statement later.  Depending on the level, set the LEVEL_TEXT To use 
		m_TextLines = EASY_LEVEL_01.text.Split('\n');
	}

	void Start()
	{
		//At the start of the game, put all values from the EASY_LEVEL_01 into the List for PlayNotePos
		for (int i = 0; i < m_TextLines.Length; i++)
		{
			PlayNotePos.Add(float.Parse(m_TextLines[i]));
		}
	}

	void Update() 
	{
		float audioTime = m_Audiosource.time;

		if (m_CanInstantiateNote)
		{
			//n.b. music note takes 2.6f seconds to get to target from instantiate point.
			if (NoteArrayToPlay < m_TextLines.Length)
			{
				//arrayValue = float for when to tap on screen
				float arrayValue = PlayNotePos[NoteArrayToPlay];
				//take arrayValue, minus instantiate offset time, convert to 2 decimal places
				string newFloatValue = (arrayValue - NOTE_INSTANTIATE_OFFSET).ToString("F2");
				//round the current audio time to 1 decimal places, then output to 2 decimal places
				string newAudioTime = System.Math.Round(audioTime, 1).ToString("F2");

				print("new float " + newFloatValue);

				//Comparing both STRINGS from above
				if (newAudioTime == newFloatValue)
				{
					NoteArrayToPlay++;
					InstantiateMusicNotes();
					Invoke("SetCanInstantiateTrue", DELAY_INSTANTIATE_DURATION);
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
		GameObject theNote = Instantiate(m_Notes, m_NoteHolderPosition.transform.position, transform.rotation) as GameObject;
		theNote.transform.SetParent(m_NoteHolderPosition.transform, false);
	}

	public float GetCurrentMusicTime()
	{
		return m_Audiosource.time;
	}

	public List<float> GetList()
	{
		return PlayNotePos;
	}
}