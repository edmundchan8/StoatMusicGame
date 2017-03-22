using UnityEngine;
using System.Collections;

public class TextRise : MonoBehaviour {

	[SerializeField]
	float SPEED = 1f;

	[SerializeField]
	float DEATH_DURATION = 1f;
	// Use this for initialization

	void Start()
	{
		Destroy(gameObject, DEATH_DURATION);
	}

	void FixedUpdate () 
	{
		transform.Translate(Vector3.up * SPEED * Time.deltaTime);
	}



}
