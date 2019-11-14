using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyprototwo : MonoBehaviour
{ 
    GameObject player;
    GameObject enemy;
    public float radius = 1f;
    public float diveTime = 1f;

    float startTime = 0f;
    bool inLoop = false;
    float timeDamp = 3f;
    float timeDampTrack;
    bool damping = true;
    public float diveForce = 50f;

    Rigidbody2D enemyRB;
    Vector3 target;
    void Start()
    {
        player = GameObject.Find("Player");
        enemy = gameObject;
        startTime = Time.time;
        enemyRB = enemy.AddComponent<Rigidbody2D>();
        enemyRB.drag = 1;
        enemyRB.gravityScale = 0;
    }

    float Dist(Vector3 a, Vector3 b)
    {
        return Mathf.Sqrt(Mathf.Pow(a.x - b.x, 2) + Mathf.Pow(a.y - b.y, 2));
    }
    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.PI / 2;
        float t = Time.time - startTime;
        float maxDist = Mathf.Sqrt(2) * radius;
        Vector3 playerPos = player.transform.position;

        Vector3 centre = playerPos + Vector3.up * radius; //the centre of the circle the enemy is orbiting around
        Vector3 direction = centre - enemy.transform.position;
        float speed = angle * radius / diveTime;



        if (Dist(enemy.transform.position, centre) > radius)
        {
            enemy.transform.position += Vector3.Normalize(direction) * Time.deltaTime * speed; //at the moment movement per second time is (Pi*radius/2)/divetime
        }
        else if (!inLoop)
        {
            inLoop = true;
            target = player.transform.position;
        }
        else
        {
            direction = target - enemy.transform.position;
            if(target.y > enemy.transform.position.y)
            {
                direction.y *= 1.5f;
            }
            
            //Debug.Log("target "+target);
            //Debug.Log("direction "+direction);
            enemyRB.AddForce(Vector3.Normalize(direction) * diveForce*Mathf.Sqrt(Dist(enemy.transform.position,target)));
            //Debug.Log(Dist(enemy.transform.position, player.transform.position));
            //if (Dist(enemy.transform.position,player.transform.position)<0.3f)
            //{
            //    Debug.Log("Hit player");
            //    if(enemyRB.velocity.x < 0)
            //    {

            //        target = player.transform.position + new Vector3(-radius, radius, 0);
            //        Debug.Log("NEW TARGET "+target);
            //    }
            //    else if (enemyRB.velocity.x > 0)
            //    {
            //        target = player.transform.position + new Vector3(radius, radius, 0);
            //    }
            player.transform.position += Input.GetAxis("Horizontal") * Time.deltaTime * 10f*Vector3.right;    
            //}
            //if(Dist(enemyRB.transform.position,player.transform.position+new Vector3(radius,radius,0))<0.3f||Dist(enemy.transform.position,player.transform.position+new Vector3(-radius, radius, 0))<0.3f)
            //{
            //    target = player.transform.position;
            //}
        }

    }
}

