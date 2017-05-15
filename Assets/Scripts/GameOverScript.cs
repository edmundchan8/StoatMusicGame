using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour 
{
	void Start () 
	{
		gameObject.SetActive(false);
	}

	public void SetLoseTextActive()
	{
		gameObject.SetActive(true);
	}
}
