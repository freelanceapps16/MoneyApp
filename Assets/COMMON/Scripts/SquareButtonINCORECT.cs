using UnityEngine;
using System.Collections;

public class SquareButton : MonoBehaviour
{ 

	// Use this for initialization
	void Start () {
        RectTransform rt = gameObject.transform as RectTransform;

        float max = rt.rect.width > rt.rect.height ? rt.rect.width : rt.rect.height;
        rt.sizeDelta = new Vector2(100, 100);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
