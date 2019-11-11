using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateTrigger : MonoBehaviour
{
	public GameObject[] triggers;
	private void OnTriggerEnter2D(Collider2D hit)
	{
		Debug.Log("FIRE");
		if (hit.gameObject.tag == "Player")
		{
			foreach (var trigger in triggers)
			{
				if (trigger.GetComponent<PressurePlateBullets>() != null)
				{
					trigger.GetComponent<PressurePlateBullets>().Trigger();
				}

				if (trigger.GetComponent<GeneralSpike>() != null)
				{
					trigger.GetComponent<GeneralSpike>().Trigger();
				}
				
			}
		}
		
	}
}
