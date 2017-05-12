using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ComboScript : MonoBehaviour 
{
	[Header ("Accessor")]
	[SerializeField]
	TouchPanel m_TouchPanel;
	[SerializeField]
	Text m_ComboCounterText;
	[SerializeField]
	Text m_HighestComboText;
	[SerializeField]
	int m_HighestComboValue;

	[Header ("Combo Counters")]
	int m_Combo = 0;

	// Update is called once per frame
	void Update () 
	{
		m_ComboCounterText.GetComponent<Text>().enabled = IsComboGreaterTwo();
		 
		m_ComboCounterText.text = m_Combo + " hit combo!";
		m_HighestComboValue = Mathf.Max(m_Combo, m_HighestComboValue);
		m_HighestComboText.text = "Highest Combo: " + m_HighestComboValue;
	}

	public bool IsComboGreaterTwo()
	{
		return (m_Combo > 2);
	}

	public void ResetCombo()
	{
		m_Combo = 0;
	}

	public void IncreaseCombo(int amount)
	{
		m_Combo += amount;
	}
}
