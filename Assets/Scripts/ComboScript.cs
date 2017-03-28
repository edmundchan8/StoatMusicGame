using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ComboScript : MonoBehaviour 
{
	[Header ("Accessor")]
	[SerializeField]
	TouchPanel m_TouchPanel;
	[SerializeField]
	Text m_Text;

	void Start()
	{
		gameObject.SetActive(false);
	}
	// Update is called once per frame
	void Update () 
	{
		m_Text.text = m_TouchPanel.GetCombo() + " hit combo!";
	}
}
