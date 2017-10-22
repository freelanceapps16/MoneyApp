using UnityEngine;
using System.Collections;

public class ConcreteModel : MonoBehaviour
{
    public Plan1ModelWrapper API = null;

	// Use this for initialization
	void Start ()
    {
        API = new Plan1ModelWrapper(PreferedDataProvider.XmlDataProvider, "XML_LOCATION");

        API.DataWrapper.LocalData.CurrencyName = "Coco";
        API.DataWrapper.LocalData.CurrencyAmount = 10000;

        API.DataWrapper.LocalData.ResetAccountsPercentTo(50, 40, 10, API.DataWrapper.LocalData.CurrencyAmount);
        
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
