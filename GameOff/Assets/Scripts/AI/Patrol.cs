using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{

    [SerializeField] float Speed;
    [SerializeField] float DepthPercetion;
    [SerializeField] float WallDepthPerception;
    [SerializeField] Transform GroundDetection1;
    [SerializeField] Transform GroundDetection2;
    [SerializeField] float Acceleration;
    [SerializeField] float DeAcceleration;
    [SerializeField] LayerMask obstacleMaskWall;
    [SerializeField] bool DebugMode;

    private bool MovRight = true, hitWall = false;
    [System.NonSerialized] public float CurrSpeed;
    private Rigidbody2D EnemyRigibody;
    private Transform EnemyTransform;


    // Start is called before the first frame update
    void Start()
    {
        EnemyRigibody = GetComponent<Rigidbody2D>();
        EnemyTransform = GetComponent<Transform>();
        CurrSpeed = Speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (DebugMode)
        {
            StartPatrol();
        }
    }

    public void StartPatrol()
    {
        transform.Translate(Vector2.right * CurrSpeed * Time.deltaTime);
        RaycastHit2D groundSlowDown = Physics2D.Raycast(GroundDetection2.position, Vector2.down, DepthPercetion, obstacleMaskWall);
        RaycastHit2D ground = Physics2D.Raycast(GroundDetection1.position, Vector2.down, DepthPercetion, obstacleMaskWall);
        RaycastHit2D wallRight = Physics2D.Raycast(GroundDetection1.position, Vector2.right, WallDepthPerception, obstacleMaskWall);
        RaycastHit2D wallLeft = Physics2D.Raycast(GroundDetection1.position, Vector2.left, WallDepthPerception, obstacleMaskWall);
        if (DebugMode)
        {
            Debug.DrawLine(GroundDetection2.position, GroundDetection2.position + Vector3.down, Color.red);
            Debug.DrawLine(GroundDetection1.position, GroundDetection1.position + Vector3.down, Color.red);
            Debug.DrawLine(GroundDetection1.position, GroundDetection1.position + Vector3.right, Color.red);
            Debug.DrawLine(GroundDetection1.position, GroundDetection1.position + Vector3.left, Color.red);
        }
        if (groundSlowDown.collider == false )
        {
            if (CurrSpeed > (Speed / 3))
            {
                CurrSpeed -= DeAcceleration;
            }
        }
        else
        {
            if (CurrSpeed < Speed)
            {
                CurrSpeed += Acceleration;
            }
        }
        //if (ground.collider == false || hitWall)
        if (ground.collider == false || wallRight || wallLeft)
        {
            Vector3 Rotate = EnemyTransform.eulerAngles;
            Rotate.y += 180;

            //hitWall = false;
            if (MovRight)
            {
                EnemyTransform.eulerAngles = Rotate;
                MovRight = false;
            }
            else
            {
                EnemyTransform.eulerAngles = Rotate;
                MovRight = true;

            }

        }
    }

    private void DetectWall()
    {
        RaycastHit2D wallHit = Physics2D.Raycast( new Vector2(MovRight ? GetComponent<BoxCollider2D>().bounds.max.x - 0.05f : GetComponent<BoxCollider2D>().bounds.min.x + 0.05f, GetComponent<BoxCollider2D>().bounds.center.y), MovRight ? Vector2.right : Vector2.left, Mathf.Infinity, obstacleMaskWall );
        Debug.DrawRay(new Vector2(MovRight ? GetComponent<BoxCollider2D>().bounds.max.x - 0.05f : GetComponent<BoxCollider2D>().bounds.min.x + 0.05f, GetComponent<BoxCollider2D>().bounds.center.y), MovRight ? Vector2.right : Vector2.left, Color.green);
        if (wallHit)
        {
            if (wallHit.distance <= 0.05f) hitWall = true;
        }
    }
}
