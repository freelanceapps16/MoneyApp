using UnityEngine;
using System.Collections;

//TEMPORARY FOR TEST PURPOSE :
using AppXmlCommon;

//Singleton for the Application Programming Interface of the model
public class API
{
    private static Plan1ModelWrapper instance = null;

    public API()
    {
    }

    public static Plan1ModelWrapper Instance
    {
        get
        {
            if(null == instance)
            {
                string xmlLocation = System.IO.Path.Combine(Application.streamingAssetsPath, "User1Data.xml");
                instance = new Plan1ModelWrapper(PreferedDataProvider.XmlDataProvider, xmlLocation);
            }

            return instance;
        }
    }

}

public class ConcreteModel : MonoBehaviour
{
    // Use this for initialization
    void Start ()
    {
        API.Instance.DataWrapper.LocalData.CurrencyName = "Coco";
        API.Instance.DataWrapper.LocalData.CurrencyAmount = 10000;

        API.Instance.DataWrapper.LocalData.ResetAccountsPercentTo(50, 40, 10, API.Instance.DataWrapper.LocalData.CurrencyAmount);
        API.Instance.DataWrapper.LocalData.Refresh();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void TestWriteXml()
    {
        API.Instance.DataWrapper.LocalData.Save();
    }

    public void TestReadXml()
    {
        API.Instance.DataWrapper.LocalData.Refresh();
    }

    public void SetLastAccountClickedIndex(int index)
    {
        API.Instance.Logic.lastAccountPanelIndex = index;
    }
}
