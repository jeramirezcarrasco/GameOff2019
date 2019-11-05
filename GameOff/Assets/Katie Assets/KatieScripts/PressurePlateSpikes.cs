using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateSpikes : MonoBehaviour
{
	public bool Right;
	public GameObject bullet;
	public int numBullets;
	public float bulletForce = 200f;
	private Vector3 direction;
	// Start is called before the first frame update
	void Start()
	{
		direction = Right ? Vector3.right : Vector3.left;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Fire()
	{
		for (int i = 0; i < numBullets; i++)
		{
			float newx = gameObject.transform.position.x;
			float newy = gameObject.transform.position.y - transform.localScale.y / 4 + (transform.localScale.y/2) * ((float) i / (numBullets - 1));
			var newBullet = Instantiate(bullet, new Vector3(newx,newy,0), Quaternion.identity);
			newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletForce * (Right? 1:-1), 0));
			Destroy(newBullet, 2f);
		}
	}
}
