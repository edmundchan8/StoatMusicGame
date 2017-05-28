using UnityEngine;
using System.Collections;

public class FlashCode : MonoBehaviour 
{
	//When attached to a gameobject, I want this script to find all the sprites on this gameobject and cause them to flash via the timer script
	//Call SetFlashTimer to begin flashing.

	[SerializeField]
	float FLASH_DURATION = 3f;
	[SerializeField]
	float TIME_TO_FLASH_DURATION = 0.5f;

	[SerializeField]
	SpriteRenderer m_SpriteRenderer;
	[SerializeField]
	bool m_CanFlash = false;
	[SerializeField]
	Timer m_FlashTimer;
	[SerializeField]
	Timer m_DurationTimer;

	void Start()
	{
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update ()
	{
		m_FlashTimer.Update(Time.deltaTime);
		m_DurationTimer.Update(Time.deltaTime);

		if (m_FlashTimer.HasCompleted() && m_CanFlash)
		{
			Flash();
			m_FlashTimer.SetTimer(TIME_TO_FLASH_DURATION);
		}
		if (m_DurationTimer.HasCompleted() && m_CanFlash)
		{
			m_SpriteRenderer.enabled = true;
			m_CanFlash = false;
		}
	}

	void Flash()
	{
		m_SpriteRenderer.enabled = !m_SpriteRenderer.enabled;
	}

	public void SetFlashTimer()
	{
		m_CanFlash = true;
		m_FlashTimer.SetTimer(TIME_TO_FLASH_DURATION);
		m_DurationTimer.SetTimer(FLASH_DURATION);
	}
}
