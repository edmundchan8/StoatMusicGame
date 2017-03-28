using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TouchPanel : MonoBehaviour 
{
	//bool to control if we can detect we are pressing a key
	bool m_IsPressed = false;

	[Header ("CONST")]
	//float values to check the sqrmagnitude, and output the achievement for each press
	[SerializeField]
	float EXCELLENT_MIN = 105000f;
	[SerializeField]
	float EXCELLENT_MAX = 111000f;
	[SerializeField]
	float GOOD_MIN = 98000f;
	[SerializeField]
	float GOOD_MAX = 114000f;

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
	//the music note prefab instantiated in our game
	public Image m_PrefabMusicNote;
	//Position of text gameobject in game so that the Text gameobjects can appear on the panel.
	//Otherwise cannot see on the screen
	[SerializeField]
	GameObject m_TextPosition;
	[SerializeField]
	GameObject m_ComboText;

	[Header ("Combo Counters")]
	int m_NumExcellents = 0;
	int m_NumGoods = 0;
	int m_NumPoors = 0;
	int m_Combo = 0;

	void Update () 
	{
		OnCombo();
	}

	//Checks the music note is over / on top of our touch panel
	void OnTriggerStay2D (Collider2D other)
	{
		//if what is on top of our touchpanel is the music note
		if (other.gameObject.tag == "MusicNote")
		{
			//if we perform an action
			if (Input.GetMouseButtonDown(0) && !m_IsPressed)
			{
				//work out the sqrmagnitude between our touch panel and the music note
				float sqrMagnitude = (transform.localPosition - other.transform.localPosition).sqrMagnitude; 
				print(sqrMagnitude);
				//between certain range = excellent
				if (sqrMagnitude > EXCELLENT_MIN && sqrMagnitude < EXCELLENT_MAX)
				{
					//For each excellent, increase combo by 1
					m_Combo++;
					//Increase m_NumExcellents by 1
					m_NumExcellents++;
					//Instantiate text alert
					m_TextResult = m_Excellent.gameObject;
					InstantiateTextGameObject();
				}
				//between certain range = good
				else if (sqrMagnitude > GOOD_MIN && sqrMagnitude < EXCELLENT_MIN || sqrMagnitude > EXCELLENT_MAX && sqrMagnitude < GOOD_MAX)
				{
					//Reset combo to 0
					m_Combo = 0;
					//Increase m_NumGoods by 1
					m_NumGoods++;
					//Instantiate text alert
					m_TextResult = m_Good.gameObject;
					InstantiateTextGameObject();
				}
				//Otherwise
				else
				{
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
			else if (Input.GetMouseButtonUp(0))
			{
				m_IsPressed = false;
			}
		}
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
		if (m_Combo >= 3)
		{
			m_ComboText.SetActive(true);
		}
		else if (m_Combo < 3)
		{
			m_ComboText.SetActive(false);
		}
	}

	public int GetCombo()
	{
		return m_Combo;
	}
}
