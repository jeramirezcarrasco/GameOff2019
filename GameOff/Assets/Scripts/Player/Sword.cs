﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
	public GameObject killEffect;
	private PlayerControl player;
    // Start is called before the first frame update
    void Start()
    {
		player = GameObject.Find("Player").GetComponent<PlayerControl>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Enemy" && !player.HoldingWeapon)
		{
            GameObject itemSpawner = new GameObject("Item Spawner");
            ItemDrop items = itemSpawner.AddComponent<ItemDrop>();
            items.SpawnItems(collision.gameObject.transform.position);
            Destroy(itemSpawner);
			Instantiate(killEffect, collision.gameObject.transform.position, Quaternion.identity);
			Destroy(collision.gameObject);
            
		}
	}
	public void Swing()
	{
		StartCoroutine(Throwing());
	}
	private IEnumerator Throwing()
	{
		player.HoldingWeapon = false;
		player.SetAnimationStatus(3);
		yield return new WaitForSeconds(0.4f);
		player.SetAnimationStatus(0, true);
		player.HoldingWeapon = true;
	}
}
