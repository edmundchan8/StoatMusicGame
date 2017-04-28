using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class TouchPanel : MonoBehaviour 
{
	//bool to control if we can detect we are pressing a key
	bool m_IsPressed = false;

	[Header ("CONST")]
	[SerializeField]
	float EXCELLENT_MIN_MAX = 0.1f;
	[SerializeField]
	float GOOD_MIN_MAX = 0.2f;

	[Header ("Text pop ups")]
	//Hold the gameobjects that we will use to show the text in the game
	[SerializeField]
	Text m_Excellent;
	[SerializeField]
	Text m_Good;
	[SerializeField]
	Text m_Poor;

	[Header ("Accessor")]
	//Shows the text when we hit the touch panel
	[SerializeField]
	GameObject m_TextResult;
	//Position of text gameobject in game so that the Text gameobjects can appear on the panel.
	//Otherwise cannot see on the screen
	[SerializeField]
	GameObject m_TextPosition;
	[SerializeField]
	GameObject m_ComboText;
	[SerializeField]
	MusicManager m_MusicManager;
	[SerializeField]
	List<float> m_MusicList;

	[Header ("Timers")]
	[SerializeField]
	float m_MusicTime;

	[Header ("Combo Counters")]
	int m_NumExcellents = 0;
	int m_NumGoods = 0;
	int m_NumPoors = 0;
	int m_Combo = 0;

	void Start()
	{
		//I will remove items from the music list (list that helps instantiate music notes, so I want to create for this script a new unique list
		m_MusicList = new List<float>();
		//copy all the values from the musicmanager list to this list
		m_MusicList.AddRange(m_MusicManager.GetList());
		//Later on, everytime we check the timing of our input against the music list, remove the timing from our list here.
	}

	void Update () 
	{
		m_MusicTime = m_MusicManager.GetCurrentMusicTime();

		OnCombo();
		//TODO: Code here is to play game with keyboard space bar input only, like debug mode
		if (Input.GetKeyDown(KeyCode.Space))
		{

			//for debug purposes, when we hit space, disable the miss detected code


			CheckMusicAgainstTiming();
			/*
			//print(dictionary.Key);
			//between certain range = excellent
			if (CheckMusicAgainstTiming() > m_MusicTime - EXCELLENT_MIN_MAX && CheckMusicAgainstTiming() < m_MusicTime + EXCELLENT_MIN_MAX)
			{
				print(CheckMusicAgainstTiming() + " MusicMarker");
				print(m_MusicTime + "Exc");
				//For each excellent, increase combo by 1
				m_Combo++;
				//Increase m_NumExcellents by 1
				m_NumExcellents++;
				//Instantiate text alert
				m_TextResult = m_Excellent.gameObject;
				InstantiateTextGameObject();
				return;
			}
			//between certain range = good
			else if (CheckMusicAgainstTiming() > (m_MusicTime + EXCELLENT_MIN_MAX) && CheckMusicAgainstTiming() < (m_MusicTime + GOOD_MIN_MAX) || 
				CheckMusicAgainstTiming() < (m_MusicTime - EXCELLENT_MIN_MAX) && CheckMusicAgainstTiming() > (m_MusicTime - GOOD_MIN_MAX))
			{
				print(CheckMusicAgainstTiming() + " MusicMarker");
				print(m_MusicTime + "Goo");
				//Reset combo to 0
				m_Combo = 0;
				//Increase m_NumGoods by 1
				m_NumGoods++;
				//Instantiate text alert
				m_TextResult = m_Good.gameObject;
				InstantiateTextGameObject();
				return;
			}
			//Otherwise
			{
				print(CheckMusicAgainstTiming() + " MusicMarker");
				print(m_MusicTime + "poo");
				//TODO - Poor is being called regardless of touch...
				//lol, foreach goes through ALL arrays
				//Reset combo to 0
				m_Combo = 0;
				//Increase m_NumPoors by 1
				m_NumPoors++;
				//Instantiate text alert
				m_TextResult = m_Poor.gameObject;
				InstantiateTextGameObject();
			}
			*/
		}

		//TODO: code here is for touch input to play with mobile
		//Comment out for time being.
		/*
		if (Input.touchCount > 0)
		{
			//create an instance of the touch input, first touch
			Touch myTouch = Input.GetTouch(0);

			switch(myTouch.phase)
			{
				case TouchPhase.Began:
					if (!m_IsPressed)
					{
						//TODO It doesn't matter when the player touches the screen to tap to the music.
						//If the player taps within a certain distance of ANY of the times in the array, indicate the result
						//We are doing this instead of individual arrays because if you miss one, the code automatically
						//sets the next timed array as the one you need to hit, which can become inaccurate.
						//Probably need to use a foreach loop
						//The current arrayValue, give or take 0.15f seconds away from that is excellent
						//Give or take more than 0.15f - 0.4f second from that is good
						//more than 0.4f second from that is poor
						//DICTIONARY? instead?
						//if any of the arrays contains a value that is close to our touch time, call something.

						//print(dictionary.Key);
						//between certain range = excellent
						if (m_MusicMarker > m_MusicTime - EXCELLENT_MIN_MAX && m_MusicMarker < m_MusicTime + EXCELLENT_MIN_MAX)
						{
							print(m_MusicMarker + " MusicMarker");
							print(m_MusicTime + "Exc");
							//For each excellent, increase combo by 1
							m_Combo++;
							//Increase m_NumExcellents by 1
							m_NumExcellents++;
							//Instantiate text alert
							m_TextResult = m_Excellent.gameObject;
							InstantiateTextGameObject();
							return;
						}
						//between certain range = good
						else if (m_MusicMarker > (m_MusicTime + EXCELLENT_MIN_MAX) && m_MusicMarker < (m_MusicTime + GOOD_MIN_MAX) || 
							m_MusicMarker < (m_MusicTime - EXCELLENT_MIN_MAX) && m_MusicMarker > (m_MusicTime - GOOD_MIN_MAX))
						{
							print(m_MusicMarker + " MusicMarker");
							print(m_MusicTime + "Goo");
							//Reset combo to 0
							m_Combo = 0;
							//Increase m_NumGoods by 1
							m_NumGoods++;
							//Instantiate text alert
							m_TextResult = m_Good.gameObject;
							InstantiateTextGameObject();
							return;
						}
						//Otherwise
						{
							print(m_MusicMarker + " MusicMarker");
							print(m_MusicTime + "poo");
							//TODO - Poor is being called regardless of touch...
							//lol, foreach goes through ALL arrays
							//Reset combo to 0
							m_Combo = 0;
							//Increase m_NumPoors by 1
							m_NumPoors++;
							//Instantiate text alert
							m_TextResult = m_Poor.gameObject;
							InstantiateTextGameObject();
						}
						m_IsPressed = true;
					}
					break;
				case TouchPhase.Ended:
				case TouchPhase.Canceled:
					m_IsPressed = false;
					break;
			} */	
	}

	void InstantiateTextGameObject () 
	{
		GameObject theTextObject = Instantiate(m_TextResult, m_TextPosition.transform.position, transform.rotation) as GameObject;
		//We use SetParent so that we can set the world position to false
		theTextObject.transform.SetParent(m_TextPosition.transform, false);
	}

	void OnCombo()
	{
		//When we hit 3 on the combo counter, reveal the combo text
		m_ComboText.SetActive(m_Combo>=3);
	}

	//if you touch a music note, then destroy it 
	void OnTriggerStay2D(Collider2D other) 
	{
		//Check to make sure we are touching screen
		if (Input.touchCount > 0)
		{
			//When the player touches the screen, and the touch has just begun
			if (Input.GetTouch(0).phase == TouchPhase.Began)
			{
				//when the other object touched is the music note prefab 
				if (other.gameObject.tag == "MusicNote")
				{
					Destroy(other.gameObject);
				}
			}
		}
	}

	public void MissDetected()
	{
		//reset combo counter
		m_ComboText.SetActive(false);
		m_Combo = 0;
	}

	public int GetCombo()
	{
		return m_Combo;
	}

	public void CheckMusicAgainstTiming () 
	{
		//make note of time which you touched screen
		float hitTime = m_MusicTime;
		//go through list 
		for (int i = 0; i < m_MusicList.Count; i++)
		{
			if (hitTime > m_MusicList[i] - EXCELLENT_MIN_MAX && hitTime < m_MusicList[i] + EXCELLENT_MIN_MAX)
			{
				//For each excellent, increase combo by 1
				m_Combo ++;
				//Increase m_NumExcellents by 1
				m_NumExcellents++;
				//Instantiate text alert
				m_TextResult = m_Excellent.gameObject;
				InstantiateTextGameObject();
				//Remove the timing that we checked just now from the list
				//There was an error before where, if the timings were too close together (2.5f, 2.7f, 2.9f), the timing wouldn't know which to test against.
				//By removing a timing from the list, the code won't retest a timing we have tried before.
				m_MusicList.Remove(m_MusicList[i]);
				return;
			}
			else if (hitTime > m_MusicList[i] - GOOD_MIN_MAX && hitTime < m_MusicList[i] + GOOD_MIN_MAX)
			{
				//Reset combo to 0
				m_Combo = 0;
				//Increase m_NumGoods by 1
				m_NumGoods++;
				//Instantiate text alert
				m_TextResult = m_Good.gameObject;
				InstantiateTextGameObject();
				m_MusicList.Remove(m_MusicList[i]);
				return;
			}

			else
			{
				//once i is the last one in the list
				if (i == m_MusicList.Count - 1)
				{
					//Reset combo to 0
					m_Combo = 0;
					//Increase m_NumPoors by 1
					m_NumPoors++;
					//Instantiate text alert
					m_TextResult = m_Poor.gameObject;
					InstantiateTextGameObject();
				}
			}
		}


	}

}
