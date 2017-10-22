using UnityEngine;
using UnityEngine.UI;

public class AppStartupChecker : MonoBehaviour
{
    public GameObject exitButtonReference;
    public Text exitButtonTextObject;
    private bool iOSPlatformActive;

    void Start ()
    {
        iOSPlatformActive = false;

        if (Application.platform == RuntimePlatform.IPhonePlayer)
            iOSPlatformActive = true;

        if(iOSPlatformActive)
        {
            exitButtonTextObject.text = "";
            exitButtonReference.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        }

    }
}
