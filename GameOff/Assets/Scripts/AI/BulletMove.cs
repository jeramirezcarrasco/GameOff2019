using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{

    public float speed;
    public Rigidbody2D rb;
    public GameObject Impact;

    // Start is called before the first frame update
    void Start()
    {
        //rb.velocity = new Vector2(0,0);
        rb.velocity = transform.right * speed;

        Destroy(gameObject, 5);
        //Invoke("Disappear", 5);
    }
    

}
