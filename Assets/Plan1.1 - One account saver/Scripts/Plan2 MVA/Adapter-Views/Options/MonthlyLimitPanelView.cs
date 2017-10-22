using UnityEngine;
using UnityEngine.UI;

public class MonthlyLimitPanelView : MonoBehaviour {

    public GameObject monthlyLimitPanelRef;
    public Text placeholderFieldTextReference;//to display errors
    public Text inputFieldTextReference;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
    public void CloseMonthlyLimitPanel()
    {
        bool err = false;

        //todo :

        //if an error translating string into number occurs,put a message in the placeholder text.

        //Send message to the "Options" panel presenter to update monthly limit in database.

        if (!err)
            monthlyLimitPanelRef.SetActive(false);
    }
	
}
