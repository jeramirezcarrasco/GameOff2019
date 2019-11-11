using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSpike : MonoBehaviour
{

	public GameObject spike; //this is the prefab that will jut out
	public float spikeMoveSpeed; //the speed at which the spikes jut out
	public Vector3 directionOfSpike; // the offset of the spike from its current position to the jutting out position
	//public float size; //the height of the spike or the width if it is a wall spike, so we know how far it needs to travel

	public bool isTimedSpike; //is it a timed spike or a manually triggered one
	public float timeInterval; //if it is a timed spike we need to know how long to wait between it sticking out
	public GameObject[] optionalTriggers; // if not a timed spike - optionally add triggers.

	private float lastTime;
	private bool moving;

	private Vector3 inPosition;
	private Vector3 outPosition;

	void Start()
	{
		inPosition = spike.transform.position;
		outPosition = inPosition + directionOfSpike;
		moving = false;
		lastTime = Time.time;
	}

	private void Update()
	{
		if (isTimedSpike)
		{
			ManageSpike();
		}
	}

	public void Trigger()
	{
		if (spike.transform.position == inPosition)
		{
			StartCoroutine(MoveOutThenIn(spike));
		}
	}

	private void ManageSpike()
	{
		if (moving)
		{
			lastTime = Time.time;
		}
		else
		{
			if (Time.time - lastTime > timeInterval)
			{
				StartCoroutine(MoveSpike(spike, spike.transform.position == inPosition ? outPosition : inPosition));
			}
		}

	}
	IEnumerator MoveOutThenIn(GameObject obj)
	{
		StartCoroutine(MoveSpike(spike, outPosition));
		while (moving)
		{
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForSeconds(timeInterval);
		StartCoroutine(MoveSpike(spike, inPosition));
	}

	IEnumerator MoveSpike(GameObject obj, Vector3 targetPosition)
	{
		moving = true;
		Vector3 initPosition = obj.transform.position;
		float t = 0;
		while (obj.transform.position != targetPosition)
		{
			t += Time.deltaTime * spikeMoveSpeed;
			obj.transform.position = Vector3.Lerp(initPosition, targetPosition, t);
			yield return new WaitForEndOfFrame();
		}
		moving = false;
	}
}
