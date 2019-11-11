using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour {

    public int RotateSpeed;
    private LineOfSight lineofsight;
    private Shooting shooting1;
    private LineOfSightVisual lineOfSightVisual;
    int BarrelRotateAngle;
    bool RotateLeft;
    public int AlertedViewRange;
    public int MaxViewAngle;
    public int MinViewAngle;

    // Use this for initialization
    void Awake ()
    {
        lineOfSightVisual = GetComponent<LineOfSightVisual>();
        lineofsight = GetComponent<LineOfSight>();
        shooting1 = GetComponent<Shooting>();
    }

    private void Start()
    {
        BarrelRotateAngle = 90;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!lineofsight.Spoted())
        {

            lineOfSightVisual.DrawFieldOfView();
            lineofsight.CurrFov = lineofsight.Fov;
            lineofsight.CurrRange = lineofsight.range;
            LookPatrol();
            shooting1.endShooting();

        }
        else if(lineofsight.Spoted())
        {
            lineOfSightVisual.viewMesh.Clear();
            lineofsight.CurrRange = AlertedViewRange;
            lineofsight.CurrFov = 110;
            shooting1.startShooting();
            shooting1.Point();
        }

    }

    void LookPatrol()
    {
        if (RotateLeft)
        {
            if (BarrelRotateAngle < MinViewAngle)
            {
                RotateLeft = false;
            }
            BarrelRotateAngle -= 1;
            Quaternion rotation = Quaternion.AngleAxis(BarrelRotateAngle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, RotateSpeed * Time.deltaTime);
        }
        else if (!RotateLeft)
        {
            if (BarrelRotateAngle > MaxViewAngle)
            {
                RotateLeft = true;
            }
            BarrelRotateAngle += 1;
            Quaternion rotation = Quaternion.AngleAxis(BarrelRotateAngle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, RotateSpeed * Time.deltaTime);
        }
    }
}
