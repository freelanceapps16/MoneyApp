using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuItem : MonoBehaviour
{
    public Text textControlReference;
    public bool overrideText;
    public string itemText;

	// Use this for initialization
	void Start ()
    {
        if(overrideText)
            textControlReference.text = itemText;
	}



}
