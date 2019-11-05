using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	public float moveSpeed = 5f;
	public float airMoveSpeed = 2f;
	public float jumpForce = 200f;
	public float bulletSpeed = 2000f;
	public GameObject weapon;
	public GameObject bullet;
	public GameObject bulletSpawnPosition;
	private GameObject player;

	Rigidbody2D playerRB;

	void Start()
	{
		player = this.gameObject;
		playerRB = player.GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		MovementControl();
	}

	private bool IsGrounded()
	{
		//if we are touching the floor then the raycast will hit it so we can return true
		Debug.DrawRay(player.transform.position, Vector3.down * 0.67f, Color.blue);
		return Physics2D.Raycast(player.transform.position, Vector3.down, 0.67f);
	}
	private void MovementControl()
	{
		//is the player on the ground?
		if (IsGrounded())
		{
			playerRB.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, playerRB.velocity.y);
			SetAnimationStatus((Input.GetAxis("Horizontal") != 0) ? 1 : 0);
			if (Input.GetAxis("Horizontal") != 0)
			{
				SetRotation(y: (Input.GetAxis("Horizontal") < 0 ? 180 : 0));
			}
			if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
			{
				Jump();
			}
		}
		else //they are in the air
		{
			//still allow manipulation of velocity but reduce it when in the air
			//**NOTE** I'm aware this still needs fixing.
			playerRB.velocity += new Vector2(Input.GetAxis("Horizontal") * airMoveSpeed * Time.deltaTime,0);
			SetAnimationStatus(2);
		}
		if (Input.GetMouseButtonUp(0))
		{
			Shoot();
		}
	}
	private void Shoot()
	{
		GameObject newBullet = Instantiate(bullet, bulletSpawnPosition.transform.position, Quaternion.identity);
		Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		clickPos.z = 0;
		Vector3 direction = clickPos - bulletSpawnPosition.transform.position;
		Vector3 bulletRotation = new Vector3(0, 0, 180 - player.transform.eulerAngles.z + Mathf.Rad2Deg * Mathf.Atan((clickPos.y - newBullet.transform.position.y) / (clickPos.x - newBullet.transform.position.x)));
		newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x, direction.y) * bulletSpeed);
		newBullet.transform.eulerAngles = bulletRotation;
		Destroy(newBullet, 2f);

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
	private void SetAnimationStatus(int index)
	{
		// 0 = Idle
		// 1 = Walking
		// 2 = Jumping
		player.GetComponent<Animator>().SetInteger("AnimationState", index);
	}
}
