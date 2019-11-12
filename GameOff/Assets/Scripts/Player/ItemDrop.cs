using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// drops items for the player to pick up
public class ItemDrop : MonoBehaviour
{
	public GameObject[] drops;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnItems(Vector3 position)
    {
        foreach(var drop in drops)
        {
            for(int i = 0; i < Random.Range(0, 3); i++)
            {
                Instantiate(drop, position, Quaternion.identity);
            }
        }
    }
}
