using UnityEngine;

public class AndroidAppCloser : MonoBehaviour
{
    /* Caution : Calling the app close method on iOS will result in a forcefully app close
     * with some error codes set. So we let user close app on iOS only by phone's task manager.
     */

    bool bCanSafelyExit;

    void Start ()
    {
        bCanSafelyExit = false;

        if (Application.platform == RuntimePlatform.Android)
            bCanSafelyExit = true;
    
    }

    public void CloseApplication()
    {
        if(bCanSafelyExit)
            Application.Quit();
    }

}
