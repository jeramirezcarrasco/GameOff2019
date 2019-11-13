using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	GameObject player;
	float minHeight = 3;
	float maxHeight = 4.5f;
	float minWidth = -2.5f;
	float maxWidth = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
		player = GameObject.Find("Player");
		//minHeight = player.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 position = gameObject.transform.position;
		position.x = Mathf.Max(Mathf.Min(player.transform.position.x,maxWidth),minWidth);
		position.y = Mathf.Max(Mathf.Min(player.transform.position.y+2, maxHeight), minHeight);
		transform.position = position;
    }
}
