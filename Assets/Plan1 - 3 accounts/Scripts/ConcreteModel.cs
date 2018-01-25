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
                string xmlLocation = System.IO.Path.Combine(Application.persistentDataPath, "User1Data.xml");
                Debug.Log(xmlLocation);
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
        API.Instance.DataWrapper.LocalData.Refresh();
    }
	
    public void SetLastAccountClickedIndex(int index)
    {
        API.Instance.Logic.lastAccountPanelIndex = index;
    }
}
