using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
	PlayerControl player;
	public Sprite[] items;
	private int[] holding; //how many of each item are we holding;
	GameObject invUI;
	GameObject InventoryCanvas;
	Canvas canvas;
	public GameObject contentView;
    // Start is called before the first frame update'
	void Start()
    {
		InventoryCanvas = gameObject;
		canvas = gameObject.GetComponent<Canvas>();
		canvas.enabled = false;
		player = GameObject.Find("player").GetComponent<PlayerControl>();

	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.I)&&!canvas.enabled)
		{
			canvas.enabled = true;
			CreateUI();
		}
		if (Input.GetKeyDown(KeyCode.Escape)&&canvas.enabled)
		{
			foreach(Transform child in contentView.transform)
			{
				Destroy(child.gameObject);
			}
			canvas.enabled = false;
		}
    }

	void CreateUI()
	{
		int i = 0;
		//need to add images and text to the viewport so we know how much stuff we have
		foreach(var item in items)
		{
			GameObject element = new GameObject(item.name);
			element.transform.parent = contentView.transform;
			RectTransform rect = element.AddComponent<RectTransform>();
			rect.anchorMin = new Vector2(0, 1);
			rect.anchorMax = new Vector2(0, 1);
			rect.pivot = new Vector2(0,1);
			rect.localPosition = new Vector2(10,-10 -100*i);
			//rect.anchoredPosition = new Vector2(0,1);
			//rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
			//rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
			Image im = element.AddComponent<Image>();
			im.sprite = item;
			i += 1;
		}
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		//item pick up control - I think it makes sense to do it from this script

	}
}
