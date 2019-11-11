using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFire : MonoBehaviour
{

    [System.NonSerialized] public float Speed;
    [System.NonSerialized] public float GrowthRate;
    [System.NonSerialized] public float ChargeTime;
    [System.NonSerialized] public float TurnSpeed;
    [System.NonSerialized] public float Smooth;

    [SerializeField] Animator anim;

    private Rigidbody2D rb;
    private Transform Player;
    private bool Charching;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Charching = true;
        StartCoroutine("ChargeFireBall");
    }

    // Update is called once per frame
    void Update()
    {
        if (!Charching)
        {
            rb.velocity = transform.right * Speed;
            Point(false);
        }
    }

    public void Point(bool defaultTurnSpeed)
    {
        Vector2 direction = Player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (defaultTurnSpeed)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 7 * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, TurnSpeed * Time.deltaTime);
        }
    }

    IEnumerator ChargeFireBall()
    {
        while (ChargeTime >= 0)
        {
            Debug.Log(ChargeTime);
            transform.localScale += new Vector3(GrowthRate, GrowthRate, 0);
            Point(true);
            yield return new WaitForSeconds(Smooth);
            ChargeTime -= 1;

        }
        anim.SetTrigger("IsMoving");
        Charching = false;
        
    }
}
