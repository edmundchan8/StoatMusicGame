using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TouchPanel : MonoBehaviour 
{
	//the music note prefab instantiated in our game
	public Image m_PrefabMusicNote;

	//bool to contorl if we can detect we are pressing a key
	bool m_IsPressed = false;

	//float values to check the sqrmagnitude, and output the achievement for each press
	[SerializeField]
	float EXCELLENT = 8f;

	[SerializeField]
	float GOOD = 17f;


	//Checks the music note is over / on top of our touch panel
	void OnTriggerStay2D (Collider2D other)
	{
		//if what is on top of our touchpanel is the music note
		if (other.gameObject.tag == "MusicNote")
		{
			//if we perform an action
			if (Input.GetKeyDown(KeyCode.Space) && !m_IsPressed)
			{
				//work out the sqrmagnitude between our touch panel and the music note
				float sqrMagnitude = (transform.position - other.transform.position).sqrMagnitude; 
				//Less than 5f-8f? = Excellent
				if (sqrMagnitude < EXCELLENT) 
				{
					print("Excellent!");
				}
				//between 8f and 17f = Good
				else if (sqrMagnitude > EXCELLENT && sqrMagnitude < GOOD) 
				{
					print("Good");
				}
				//Outside 17f is bad
				else 
				{
					print("poor");
				}
				m_IsPressed = true;
			}
			else if (Input.GetKeyUp(KeyCode.Space))
			{
				m_IsPressed = false;
			}
		}
	}
}
