using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// drops items for the player to pick up
public class ItemDrop : MonoBehaviour
{
	public Sprite[] drops;
	public float force = 200f;
	public float itemScale = 0.3f;
	public PhysicsMaterial2D itemMaterial;
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
				GameObject item = new GameObject(drop.name);
				item.tag = drop.name;
				item.transform.position = position;
				item.transform.localScale = new Vector3(itemScale,itemScale, 1);
				SpriteRenderer itemSprite = item.AddComponent<SpriteRenderer>();
				itemSprite.sprite = drop;
				Rigidbody2D RB = item.AddComponent<Rigidbody2D>();
				CircleCollider2D poly = item.AddComponent<CircleCollider2D>();
				poly.sharedMaterial = itemMaterial;

				Vector3 relativePosition = position - GameObject.Find("Player").transform.position;
				float relx = relativePosition.x;
				RB.AddForce(new Vector2(Random.Range(relx*0.5f,relx*2.5f), Random.Range(0f, 3f)) * force);
            }
        }
    }
}
