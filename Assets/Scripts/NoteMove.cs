using UnityEngine;
using System.Collections;

public class NoteMove : MonoBehaviour 
{
	[SerializeField]
	float m_Speed = 2f;

	void Update() 
	{
		transform.Translate(Vector2.left * m_Speed * Time.deltaTime, Space.Self);
	}

	public void SelfDestruct()
	{
		Destroy(gameObject);
	}
}
