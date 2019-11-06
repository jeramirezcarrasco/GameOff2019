using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScale : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
		    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void FixedUpdate()
	{
		checkScale();
	}

	void checkScale()
	{
		int width = 18;
		int height = 10;
		//what does height need to be to make width 16?
		GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize = Mathf.Max(height, (float) width * (float) Screen.height / (float) Screen.width);
	}
}
