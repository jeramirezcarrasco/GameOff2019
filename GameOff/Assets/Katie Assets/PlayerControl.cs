using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	public float moveSpeed = 5f;
	public float jumpForce = 200f;
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
			if (Input.GetKeyUp(KeyCode.UpArrow))
			{
				Jump();
			}
		}
		else //they are in the air
		{
			SetAnimationStatus(2);
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
	private void SetAnimationStatus(int index)
	{
		// 0 = Idle
		// 1 = Walking
		// 2 = Jumping
		player.GetComponent<Animator>().SetInteger("AnimationState", index);
	}
}
