using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
	PlayerControl player;
	public Sprite[] items;
	public int[] holding; //how many of each item are we holding;
	GameObject invUI;
	GameObject InventoryCanvas;
	Canvas canvas;
	public GameObject contentView;
    public Font UIFont;
    // Start is called before the first frame update'
	void Start()
    {

		InventoryCanvas = gameObject;
		canvas = gameObject.GetComponent<Canvas>();
		canvas.enabled = false;
		player = GameObject.Find("Player").GetComponent<PlayerControl>();
        holding = new int[items.Length];
        for(int i = 0; i < items.Length; i++)
        {
            holding[i] = 0;
        }
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

	public void Refresh()
	{
		if (canvas.enabled)
		{
			foreach (Transform child in contentView.transform)
			{
				Destroy(child.gameObject);
			}
			CreateUI();
		}
		
	}


	void CreateUI()
	{
		int i = 0;
		//need to add images and text to the viewport so we know how much stuff we have
		foreach(var item in items)
		{
            AddImageAtPosition(item.name+" im",item, new Vector2(0, 1), 10, -10 - 100 * i);
            AddTextAtPosition(item.name + " pic", item.name+"s: "+holding[i], new Vector2(0, 1), 110, -10 - 100 * i);
            i += 1;
		}
	}

    void NewCanvasObject(string name, Vector2 anchor, out GameObject element, out RectTransform rect)
    {
        element = new GameObject(name);
        element.transform.parent = contentView.transform;
        rect = element.AddComponent<RectTransform>();
        rect.anchorMin = anchor;
        rect.anchorMax = anchor;
        rect.pivot = anchor;
    }

    void AddImageAtPosition(string name, Sprite sprite, Vector2 anchor, int x,int y)
    {
        GameObject element;
        RectTransform rect;
        NewCanvasObject(name, anchor,out element, out rect);
        rect.localPosition = new Vector2(x, y);
        Image im = element.AddComponent<Image>();
        im.sprite = sprite;
    }

    void AddTextAtPosition(string name, string text, Vector2 anchor, int x, int y)
    {
        GameObject element;
        RectTransform rect;
        NewCanvasObject(name, anchor,out element, out rect);
        Text itemText = element.AddComponent<Text>();
        itemText.alignment = TextAnchor.MiddleLeft;
        itemText.color = Color.white;
        itemText.horizontalOverflow = HorizontalWrapMode.Overflow;
        itemText.verticalOverflow = VerticalWrapMode.Overflow;
        itemText.text = text;
        itemText.font = UIFont;
        itemText.fontSize = 35;
        rect.localPosition = new Vector2(x, y);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
    }

}
