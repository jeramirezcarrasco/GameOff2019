using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteRotate : MonoBehaviour
{
    [SerializeField] float FlipDelay;

    private Transform Player;
    private bool bussy;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        bussy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.position.x < transform.position.x)
        {
            if (FlipDelay <= 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (!bussy)
            {
                StartCoroutine("RotateDelayLeft");
            }
        }
        else if (Player.position.x > transform.position.x)
        {
            if (FlipDelay <= 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if(!bussy)
            {
                StartCoroutine("RotateDelayRight");
            }
        }
    }

    IEnumerator RotateDelayRight()
    {
        bussy = true;
        yield return new WaitForSeconds(FlipDelay);
        GetComponent<SpriteRenderer>().flipX = true;
        bussy = false;
    }

    IEnumerator RotateDelayLeft()
    {
        bussy = true;
        yield return new WaitForSeconds(FlipDelay);
        GetComponent<SpriteRenderer>().flipX = false;
        bussy = false;


    }
}
