using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour 
{
	[SerializeField]
	AudioSource m_Audiosource;

	[SerializeField]
	bool m_StartMusic = false;

	//Position where the notes to touch are generated
	[SerializeField]
	GameObject m_NoteHolderPosition;

	//The note prefab to instantiate
	[SerializeField]
	GameObject m_Notes;

	//Array to hold the music note instantiate times
	[SerializeField]
	float [] PlayNotePos = new float[] {};
	[SerializeField]
	int NoteArrayToPlay = 0;
	//Because the instantiate gameobject is further away from where we want the touch the note, instantiate the music note 2.9f early.
	[SerializeField]
	float NOTE_INSTANTIATE_OFFSET = 2.9f;

	[Header ("DEBUG")]
	[SerializeField]
	bool m_SpacePressed = false;
	[SerializeField]
	float time;

	void Update() 
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			InstantiateMusicNotes();
			Invoke("ReenableStartMusicAfterTime", 0.5f);
			m_SpacePressed = true;
		}
			
		//DEBUG MODE
		//Test firing music note to target
		/*if (m_SpacePressed)
		{
			time += Time.deltaTime;
			print(time.ToString("F1"));
		}
		*/

		float audioTime = m_Audiosource.time;

		if (m_StartMusic)
		{
			/*print("started music");
			print(System.Math.Round(audioTime,1));*/
			//n.b. music note takes 2.6f seconds to get to target from instantiate point.

			//note - array value = float, but rounding the audiotime to 1 decimal place makes it a DOUBLE.
			//CANNOT compare double == float, so below, I have made them both STRING variables
			//THEN, I compare both strings.
			float arrayValue = PlayNotePos[NoteArrayToPlay];
			string newFloatValue = (arrayValue - NOTE_INSTANTIATE_OFFSET).ToString("F2");
			string newAudioTime = System.Math.Round(audioTime, 1).ToString("F2");
			print("new float " + newFloatValue);
			print("new audio " + newAudioTime);

			//Comparing both STRINGS
			if (newAudioTime == newFloatValue) //&& NoteArrayToPlay <= PlayNotePos.Length)
			{
				print("condition met");
				InstantiateMusicNotes();
				Invoke("ReenableStartMusicAfterTime", 0.5f);
				NoteArrayToPlay++;
			}
		}
	}

	public void StartMusic () 
	{
		m_StartMusic = true;
	}

	public void ReenableStartMusicAfterTime () 
	{
		StartMusic();
	}


	void InstantiateMusicNotes () 
	{
		m_StartMusic = false;
		GameObject theNote = Instantiate(m_Notes, m_NoteHolderPosition.transform.position, transform.rotation) as GameObject;
		theNote.transform.SetParent(m_NoteHolderPosition.transform, false);
	}
}
