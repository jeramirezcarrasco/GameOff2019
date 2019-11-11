using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{
    [SerializeField] public float Speed;
    [SerializeField] public float TurnSpeed;
    [SerializeField] public float PlayerYOffSet;
    [SerializeField] public float ActivationRange;
    [SerializeField] public float SpotExtendedRange;
    [SerializeField] public float RangeForAction;
    [SerializeField] public Transform MovementGuide;
    [SerializeField] public bool FlowtyFollow;
    [SerializeField] bool Debugger;

    private Rigidbody2D rb;
    private float OriginalActivationRange;
    private Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        OriginalActivationRange = ActivationRange;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Debugger)
        {
            if(CloseForAction())
                Debug.Log("Close Enough");

        }
    }

    public bool CloseForAction()
    {
        if (Vector2.Distance(Player.position, transform.position) < RangeForAction)
        {
            return true;
        }
        else
        {
            Follow();
            return false;
        }
    }

    private void Follow()
    {
        if (FlowtyFollow)
        {
            if (Vector2.Distance(Player.position, transform.position) < ActivationRange)
            {
                Point();
                ActivationRange = SpotExtendedRange;
                Point();
                Vector2 GuideMovemnet = MovementGuide.right * Speed;
                rb.velocity = GuideMovemnet;
            }
        }
        else
        {
            rb.velocity = new Vector2(0,0);
            ActivationRange = SpotExtendedRange;
            Vector2 Target = new Vector2(Player.position.x, Player.position.y + PlayerYOffSet);
            transform.position = Vector2.MoveTowards(transform.position, Target, Speed * Time.deltaTime);
        }
        
    }

    public void Point()
    {
      
        Vector2 direction = Player.position - MovementGuide.position;
        direction = new Vector2(direction.x, direction.y + PlayerYOffSet);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        MovementGuide.rotation = Quaternion.Slerp(MovementGuide.rotation, rotation, TurnSpeed * Time.deltaTime);
    }


}
