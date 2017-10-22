using UnityEngine;
using System.Collections;

public class GameObjectVisibilityChanger : MonoBehaviour
{
    public GameObject targetObject;
    public bool isActive;

	// Use this for initialization
	void Start ()
    {
        isActive = targetObject.activeSelf;
    }

    public void ChangeVisibilityState()
    {
        isActive = !targetObject.activeSelf;
        targetObject.SetActive(isActive);
    }

}
