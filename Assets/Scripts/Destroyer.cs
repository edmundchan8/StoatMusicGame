using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour 
{
	//access to the touchpanel so that, if we miss a note, we'll let the touch panel know.
	[SerializeField]
	TouchPanel m_TouchPanel;
	[SerializeField]
	bool m_EnableMissDetection;

	void OnTriggerEnter2D (Collider2D other)
	{
		//If the code reaches this stage, it means the note was not touched by player, this counts as a miss
		//m_TouchPanel.MissDetected();

		Destroy(other.gameObject);
		//TODO - because the note was destroyed by the barrier, it means the player didn't touch it.
		//if the player doesn't touch it, counts as a miss
		//also increments the array value int he the touch panel.
		if (m_EnableMissDetection)
		{
			m_TouchPanel.MissDetected();
		}
	}
}
