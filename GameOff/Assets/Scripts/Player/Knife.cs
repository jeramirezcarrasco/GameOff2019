﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
	PlayerControl player;
    public GameObject killEffect;
	Rigidbody2D knifeRB;
	public float throwSpeed = 2000f;
    public float throwRotation = 1800f;
	private bool held = true;
    // Start is called before the first frame update
    void Start()
    {
		player = GameObject.Find("Player").GetComponent<PlayerControl>();
		
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Throw()
	{
		player.SetAnimationStatus(3);
		StartCoroutine(Throwing());
	}

    private void AttachToObject(GameObject parent)
    {
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        GameObject connector = new GameObject();
        connector.transform.parent = parent.transform;
        gameObject.transform.parent = connector.transform;
    }

    private void DetachFromParent()
    {
        GameObject parent = gameObject.transform.parent.gameObject;
        gameObject.transform.parent.parent.DetachChildren(); //remove the attached parent
        gameObject.transform.parent.DetachChildren(); //then remove the connector
        Destroy(parent); //then delete the connector
    }

	private IEnumerator Throwing()
	{
		Debug.Log("animating");
		player.SetAnimationStatus(3);
		yield return new WaitForSeconds(0.5f);
        DetachFromParent();

		Vector3 clickPos = player.ClickPos();
		Vector3 direction = Vector3.Normalize(clickPos - gameObject.transform.position);

		knifeRB = gameObject.AddComponent<Rigidbody2D>();
		knifeRB.AddForce(direction * throwSpeed);
		knifeRB.AddTorque(1500f);
		player.SetAnimationStatus(0, true);

		yield return new WaitForSeconds(0.1f);
		held = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player" && !held)
		{
			gameObject.transform.position = player.weaponHoldPoint.transform.position;
            gameObject.transform.eulerAngles = collision.gameObject.transform.eulerAngles;
            AttachToObject(player.weaponHoldPoint);
            player.HoldingWeapon = true;
            held = true;
		}
        else if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Instantiate(killEffect, collision.gameObject.transform.position, Quaternion.identity);
        }
        else if(!held)
		{
            AttachToObject(collision.gameObject);
		}
        
	}
}
