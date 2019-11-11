using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallEnemy : MonoBehaviour
{

    
    [SerializeField] float TimeBetweenFireBalls;
    [SerializeField] bool Debugger;
    [SerializeField] GameObject FireBallPrefab;
    [SerializeField] GameObject[] Transmitters;
    [SerializeField] float Speed;
    [SerializeField] float GrowthRate;
    [SerializeField] float ChargeTime;
    [SerializeField] float TurnSpeed;
    [SerializeField] float Smooth;
    [SerializeField] float AttackDelay;
    [SerializeField] float CoolDown;

    private Flying FlyingAction;
    private bool BussyOrCooldown;
    // Update is called once per frame

    private void Start()
    {
        FlyingAction = GetComponent<Flying>();
    }

    void Update()
    {
        if (Debugger)
        {
            if (Input.GetKeyDown("f"))
            {
                SpawnFireBalls();
            }
        }

        if (!BussyOrCooldown && FlyingAction.CloseForAction() )
        {
            StartCoroutine("StartAttackDelayAndCooldown");
        }


    }

    public void SpawnFireBalls()
    {
        StartCoroutine("FireFireBalls");
    }

    IEnumerator FireFireBalls()
    {
        for (int i = 0; i < Transmitters.Length; i++)
        {
            GameObject BallOfFire = (GameObject)Instantiate(FireBallPrefab, Transmitters[i].transform.position, Transmitters[i].transform.rotation);
            MovingFire MovingScript = BallOfFire.GetComponent<MovingFire>();
            MovingScript.ChargeTime = ChargeTime;
            MovingScript.GrowthRate = GrowthRate;
            MovingScript.Speed = Speed;
            MovingScript.TurnSpeed = TurnSpeed;
            MovingScript.Smooth = Smooth;
            yield return new WaitForSeconds(TimeBetweenFireBalls);

        }
    }

    IEnumerator StartAttackDelayAndCooldown()
    {
        BussyOrCooldown = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(AttackDelay);
        SpawnFireBalls();
        yield return new WaitForSeconds(CoolDown);
        BussyOrCooldown = false;
    }

   











}
