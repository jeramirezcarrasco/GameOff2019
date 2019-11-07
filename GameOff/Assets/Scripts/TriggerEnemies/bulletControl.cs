using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //empty comment
    }

	//temporary measure to avoid too many bullets while creation is being done
	private void OnCollisionEnter2D(Collision2D collision)
	{
		Destroy(gameObject);
	}
}
