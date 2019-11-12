using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	public float moveSpeed = 5f;
	public float airMoveSpeed = 2f;
	public float jumpForce = 200f;
	public float bulletSpeed = 2000f;
	public string currentWeapon = "knife";

	public GameObject weaponPrefab;
	public GameObject bulletPrefab;
	public GameObject bulletSpawnPosition;
	public GameObject weaponHoldPoint;
    private GameObject weaponConnector;

	private GameObject player;
	private GameObject weapon;
	public GameObject animatedChild; //do we have a game-object attached to the player with the animated sprites?

	public bool HoldingWeapon = true;
	public bool AlternativeMovementType = true;

	Rigidbody2D playerRB;
	Knife knife;

	void Start()
	{
		player = gameObject;
		playerRB = player.GetComponent<Rigidbody2D>();
        //spawn in the knife
        weaponConnector = new GameObject();
        weaponConnector.transform.parent = weaponHoldPoint.transform;
        weapon = Instantiate(weaponPrefab, weaponHoldPoint.transform.position, Quaternion.identity, weaponConnector.transform);

		if (currentWeapon == "knife")
		{
			knife = weapon.GetComponent<Knife>();
		}
		else if (currentWeapon == "sword")
		{
			weapon.transform.eulerAngles += new Vector3(0, 0, -30);
		}
		
		//Physics2D.IgnoreCollision(player.GetComponent<EdgeCollider2D>(), knife.GetComponent<PolygonCollider2D>());		
	}

	void Update()
	{
		MovementControl();
		if (Input.GetMouseButtonUp(0) && HoldingWeapon)
		{
			Attack();
		}
	}
	private void MovementControl()
	{
		Vector3 direction = Mathf.Sign(Input.GetAxis("Horizontal")) < 0 ? Vector3.left : Vector3.right;
		bool RayDirection = CheckRay(direction);
		//is the player on the ground?
		if (IsGrounded())
		{
			if (!RayDirection)
			{
				if (AlternativeMovementType)
				{
					player.transform.position += Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal")*moveSpeed/100;
				}
				else
				{
					playerRB.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, playerRB.velocity.y);
				}
				
				SetAnimationStatus((Input.GetAxis("Horizontal") != 0) ? 1 : 0);
			}
			if (Input.GetAxis("Horizontal") != 0)
			{
				SetRotation(y: (Input.GetAxis("Horizontal") < 0 ? 180 : 0));
			}
			if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
			{
				Jump();
			}
		}
		else //they are in the air, still allowed to move, but slower
		{
			if (!RayDirection && AlternativeMovementType)
			{
				player.transform.position += Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal")*moveSpeed/100;
			}
			else
			{
				//if the player is moving slower than the air-move-speed
				bool Condition1 = Mathf.Abs(playerRB.velocity.x) < Mathf.Abs(Input.GetAxis("Horizontal") * airMoveSpeed * Time.deltaTime);
				//or if the player wants to move the opposite direction to the current one
				bool Condition2 = Mathf.Sign(Input.GetAxis("Horizontal")) != Mathf.Sign(playerRB.velocity.x);
				//then allow the slower movement speed in air
				if (!RayDirection && (Condition1 || Condition2))
				{
					playerRB.velocity = new Vector2(Input.GetAxis("Horizontal") * airMoveSpeed * Time.deltaTime, playerRB.velocity.y);
				}
				SetAnimationStatus(2);
			}
		}
	}

	private bool IsGrounded()
	{
		//if we are touching the floor then the raycast will hit it so we can return true
		Debug.DrawRay(player.transform.position + Vector3.down * player.transform.localScale.y / 2, Vector3.down * 0.1f, Color.blue);
		return Physics2D.Raycast(player.transform.position + Vector3.down * player.transform.localScale.y / 2, Vector3.down, 0.1f);
	}
	private bool CheckRay(Vector3 direction)
	{
		GameObject raycastObject = player; //this is just in case we wanted to raycast from something else later.
		Vector3 rayPos = raycastObject.transform.position;
		Vector3 rayScale = raycastObject.transform.localScale;
		//this checks whether the player is about to hit something in the given direction, has 3 rays to check the top middle and bottom of the player so all possibilities are covered.
		Vector3 topRay = rayPos + Vector3.Scale(direction,rayScale/2) + Vector3.up*rayScale.y/2;
		Vector3 middleRay = rayPos + Vector3.Scale(direction,rayScale / 2);
		Vector3 bottomRay = rayPos + Vector3.Scale(direction, rayScale / 2) + Vector3.down * rayScale.y / 2;
		Vector3[] rays = { topRay, middleRay, bottomRay };
		bool hasHit = false;
		foreach (var ray in rays)
		{
			Debug.DrawRay(ray, direction * 0.1f, Color.blue);
			if (Physics2D.Raycast(ray, direction, 0.1f))
			{
				hasHit = true;
			}
		}
		return hasHit;
	}
	public Vector3 ClickPos()
	{
		Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		clickPos.z = 0;
		return clickPos;
	}
	private void Attack()
	{
		Vector3 clickPos = ClickPos();
		
		if (currentWeapon == "knife")
		{
			knife.Throw();
			HoldingWeapon = false;
		}
		else if (currentWeapon == "gun")
		{
			Vector3 direction = clickPos - bulletSpawnPosition.transform.position;
			Vector3 bulletRotation = new Vector3(0, 0, 180 - player.transform.eulerAngles.z + Mathf.Rad2Deg * Mathf.Atan((clickPos.y - bulletSpawnPosition.transform.position.y) / (clickPos.x - bulletSpawnPosition.transform.position.x)));
			GameObject newBullet = Instantiate(bulletPrefab, bulletSpawnPosition.transform.position, Quaternion.identity);
			newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x, direction.y) * bulletSpeed);
			newBullet.transform.eulerAngles = bulletRotation;
			Destroy(newBullet, 2f);
		}
		else if (currentWeapon == "sword")
		{
			Sword sword = weapon.GetComponent<Sword>();
			sword.Swing();
		}
		
	}
	private void SetRotation(float? x=null, float? y=null,float? z=null)
	{
		Vector3 currentRotation = player.transform.eulerAngles;
		currentRotation.x = (x != null) ? (float) x : currentRotation.x;
		currentRotation.y = (y != null) ? (float) y : currentRotation.y;
		currentRotation.z = (z != null) ? (float) z : currentRotation.z;
		player.transform.eulerAngles = currentRotation;
	}
	private void Jump()
	{
		playerRB.AddForce(new Vector3(0, jumpForce, 0));
	}
	public void SetAnimationStatus(int index, bool overriding=false)
	{
		if (index == 3) { Debug.Log("animating to " + index + " from player"); }
		// 0 = Idle
		// 1 = Walking
		// 2 = Jumping
		// 3 = Throwing
		if (animatedChild != null)
		{
			if (animatedChild.GetComponent<Animator>() != null)
			{
				if (animatedChild.GetComponent<Animator>().GetInteger("AnimationState") != 3 || overriding)
				{
					if (index == 3) { Debug.Log("Made it"); }
					animatedChild.GetComponent<Animator>().SetInteger("AnimationState", index);
				}
				
			}
		}
	}
}
