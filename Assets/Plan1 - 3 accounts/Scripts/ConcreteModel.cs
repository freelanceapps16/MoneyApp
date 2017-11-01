using UnityEngine;
using System.Collections;

//TEMPORARY FOR TEST PURPOSE :
using AppXmlCommon;

public class ConcreteModel : MonoBehaviour
{
    public Plan1ModelWrapper API = null;

	// Use this for initialization
	void Start ()
    {
        string xmlLocation = System.IO.Path.Combine(Application.streamingAssetsPath, "User1Data.xml");
        API = new Plan1ModelWrapper(PreferedDataProvider.XmlDataProvider, xmlLocation);

        API.DataWrapper.LocalData.CurrencyName = "Coco";
        API.DataWrapper.LocalData.CurrencyAmount = 10000;

        API.DataWrapper.LocalData.ResetAccountsPercentTo(50, 40, 10, API.DataWrapper.LocalData.CurrencyAmount);
        
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void TestWriteXml()
    {
        API.DataWrapper.LocalData.Save();
    }

    public void TestReadXml()
    {
        API.DataWrapper.LocalData.Refresh();
    }
}
