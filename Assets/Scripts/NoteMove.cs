using UnityEngine;
using System.Collections;

public class NoteMove : MonoBehaviour 
{
	float m_Speed = 2.1f;

	void Update() 
	{
		transform.Translate(Vector2.left * m_Speed * Time.deltaTime, Space.Self);
	}

}
