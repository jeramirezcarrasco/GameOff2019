using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimedSpikes : MonoBehaviour
{
	public GameObject spike; //this is the prefab that will jut out
	public float spikeInterval; //the interval of time between spikes
	public float timeToExtend; //the time it takes for the spikes to jut out

	public bool wallSpike; //is it coming out of the floor or the wall.
	public float size; //the height of the spike or the width if it is a wall spike, so we know how far it needs to travel

	private Vector3 startPosition;
	private Vector3 endPosition;

	private float lastTime;
	private bool extending;
	private bool moving;
    // Start is called before the first frame update
    void Start()
    {
		extending = true; //initially we want the spike moving out, not in
		moving = false;
		lastTime = Time.time;
		startPosition = spike.transform.position;
		endPosition = startPosition + new Vector3(wallSpike ? size:0, wallSpike? 0:size, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }

	private void FixedUpdate()
	{
		ManageSpike();
	}

	private void ManageSpike()
	{
		//Debug.Log("Data dump " + moving + " " + extending + " " + lastTime);
		if (moving)
		{
			Vector3 start = extending ? startPosition : endPosition;
			Vector3 target = extending ? endPosition : startPosition;
			spike.transform.position = Vector3.Lerp(start, target, (Time.time-lastTime)/ timeToExtend); ;
			if (spike.transform.position == target)
			{
				moving = false;
				lastTime = Time.time;
				extending = !extending;
			}
			
		}
		else
		{
			if (Time.time - lastTime > spikeInterval)
			{
				lastTime = Time.time;
				moving = true;
			}
		}
	}

	private void Extend()
	{

	}

	private void MoveOverTime(GameObject obj, Vector3 newPos, float moveTime)
	{
		if (obj.transform.position == newPos)
		{
			//we have successfully reached the destination
		}
	}
}
