using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform Player;
    public float GunBarrelAimSpeed;
    public float attackSpeedSeconds;
    public GameObject GunBarrelObject;
    public GameObject BulletObject;
    public float BulletSpread;
    private bool GunIsOn;
    private bool Busy;
    public float WakeUp;
    public float BulletObjectGap;
    public float BulletObjectSpeedGap;


    private void Start()
    {
        
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        GunIsOn = false;
    }

  
    public void startShooting()
    {

        if (!GunIsOn && !Busy)
        {
            GunIsOn = true;
            StartCoroutine(Shoot());
        }
    }

    public void endShooting()
    {
        GunIsOn = false;
    }

    public void Point()
    {
        Vector2 direction = Player.position - GunBarrelObject.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        GunBarrelObject.transform.rotation = Quaternion.Slerp(GunBarrelObject.transform.rotation, rotation, GunBarrelAimSpeed * Time.deltaTime);
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(WakeUp);
        while(GunIsOn)
        {
            GameObject NewBullet = (GameObject)Instantiate(BulletObject, GunBarrelObject.transform.position, GunBarrelObject.transform.rotation);
            NewBullet.transform.Rotate(0, 0, Random.Range(-BulletSpread, BulletSpread));
            BulletMove bulletMove = NewBullet.GetComponent<BulletMove>();
            bulletMove.speed = bulletMove.speed + Random.Range(-BulletObjectSpeedGap,0);
            float Gap = attackSpeedSeconds + Random.Range(0, BulletObjectGap);
            Busy = true;
            yield return new WaitForSeconds(Gap);
            Busy = false;
        }
    }
}
