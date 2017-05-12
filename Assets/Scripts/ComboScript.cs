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
	void Start()
	{
		gameObject.SetActive(false);
	}
	// Update is called once per frame
	void Update () 
	{
		m_ComboCounterText.text = m_TouchPanel.GetCombo() + " hit combo!";
		m_HighestComboText.text = "Highest Combo: " + Mathf.Max(m_TouchPanel.GetCombo(), m_HighestComboValue);
	}

}
