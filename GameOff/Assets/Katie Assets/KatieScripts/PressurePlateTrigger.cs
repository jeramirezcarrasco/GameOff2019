using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateTrigger : MonoBehaviour
{
	public GameObject[] triggers;
	private void OnCollisionEnter2D(Collision2D hit)
	{
		if (hit.gameObject.tag == "Player")
		{
			foreach (var trigger in triggers)
			{
				trigger.GetComponent<PressurePlateSpikes>().Fire();
			}
		}
		
	}
}
