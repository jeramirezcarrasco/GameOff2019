using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyfollowprototype : MonoBehaviour
{
    GameObject player;
    GameObject enemy;
    public float radius = 1f;
    public float diveTime = 1f;

    float startTime = 0f;
    bool inLoop = false;

    void Start()
    {
        player = GameObject.Find("Player");
        enemy = gameObject;
        startTime = Time.time;
    }

    float Dist(Vector3 a, Vector3 b)
    {
        return Mathf.Sqrt(Mathf.Pow(a.x - b.x,2) + Mathf.Pow(a.y - b.y,2));
    }
    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.PI / 2;
        float t = Time.time - startTime;
        Vector3 playerPos = player.transform.position;

        Vector3 centre = playerPos + Vector3.up*radius; //the centre of the circle the enemy is orbiting around
        Vector3 direction = centre - enemy.transform.position;
        float speed = angle * radius / diveTime;

        if (Dist(enemy.transform.position, centre) > radius)
        {
            enemy.transform.position += Vector3.Normalize(direction) * Time.deltaTime * speed; //at the moment movement per second time is (Pi*radius/2)/divetime
        }
        else if (!inLoop)
        {
            Debug.Log("setting positions");
            //if we are hitting the loop for the first time then we need to make a change to the starting position on the circle
            float distX = enemy.transform.position.x - playerPos.x;
            float timing = Mathf.Acos(distX / radius);
            startTime = Time.time - timing;
            inLoop = true;
        }
        else 
        {
            float enemyX = playerPos.x + radius * (Mathf.Cos(angle * (t / diveTime)));
            float enemyY = playerPos.y + radius - Mathf.Abs(radius * Mathf.Sin(angle * t / diveTime));
            //alternative dive
            Vector3 target = enemy.transform.position + Vector3.Normalize(direction) * Time.deltaTime * speed;
            enemyY = playerPos.y + radius - Mathf.Pow(Mathf.Abs(Mathf.Sin(angle * t / diveTime)), 2) * radius;

            transform.position = new Vector3(enemyX, enemyY);
        }

    }
}
