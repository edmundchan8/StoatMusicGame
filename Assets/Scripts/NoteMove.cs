using UnityEngine;
using System.Collections;

public class NoteMove : MonoBehaviour 
{
	[SerializeField]
	float m_Speed = 50f;

	void FixedUpdate() 
	{
		transform.Translate(Vector2.left * m_Speed * Time.deltaTime);
	}

}
