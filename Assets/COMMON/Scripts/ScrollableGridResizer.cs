using UnityEngine;
using System.Collections;

public class ScrollableGridResizer : MonoBehaviour
{
    public float childItemsHeight;
    public int childItemsCount;
    public float spacing;
    public float paddingTop;
    public float paddingBottom;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ResizeUsingCurrentInfo()
    {
        Vector2 currentSizeDelta = gameObject.GetComponent<RectTransform>().sizeDelta;

        float totalHeight = (childItemsHeight * childItemsCount) + ((childItemsCount - 1) * spacing) + paddingTop + paddingBottom;

        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(currentSizeDelta.x, totalHeight);

    }

}
