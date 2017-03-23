using UnityEngine;
using System.Collections;

public class NoteMove : MonoBehaviour 
{
	float m_Speed = 2f;

	void FixedUpdate() 
	{
		transform.Translate(Vector2.left * m_Speed * Time.deltaTime);
	}

}
