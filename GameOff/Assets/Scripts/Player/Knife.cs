using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
	PlayerControl player;
	Rigidbody2D knifeRB;
	public float throwSpeed = 200f;
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

	private IEnumerator Throwing()
	{
		Debug.Log("animating");
		player.SetAnimationStatus(3);
		yield return new WaitForSeconds(0.5f);
		gameObject.transform.parent.DetachChildren();

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
			Destroy(gameObject.GetComponent<Rigidbody2D>());
			player.HoldingWeapon = true;
			gameObject.transform.position = player.weaponHoldPoint.transform.position;
			gameObject.transform.SetParent(player.weaponHoldPoint.transform);
			held = true;
		}
		else if(!held)
		{
			Destroy(gameObject.GetComponent<Rigidbody2D>());
			gameObject.transform.SetParent(collision.gameObject.transform);
		}
	}
}
