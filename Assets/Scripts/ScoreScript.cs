using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour 
{
	[SerializeField]
	Text m_ScoreText;
	[SerializeField]
	int m_Score;

	void Update()
	{
		m_ScoreText.text = "Current Score: " + m_Score;
	}

	public void IncreaseScore(int amount)
	{
		m_Score += amount;
	}
}
