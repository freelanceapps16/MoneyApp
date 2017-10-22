using UnityEngine;
using System.Collections;

public class Plan2Account : Account
{
    private Plan2Account() : base(0,0)
    { }

    public Plan2Account(int percentFrom, float totalMoney) : base(percentFrom, totalMoney)
    {

    }

}

public class Plan2MySqlData
{
    private string xmlPath;//Or add ONE parameter in constructor

    //One of the possible data sources.
    
    Plan2Account necesarryAccount = null;
    Plan2Account shoppingsAccount = null;
    Plan2Account circulationAccount = null;
    Plan2Account allAccounts = null;

    private Plan2MySqlData()
    {

    }

    public Plan2MySqlData(int percent1, int percent2, int percent3, float totalAmount)
    {
        allAccounts = new Plan2Account(100, totalAmount);
        necesarryAccount = new Plan2Account(percent1, totalAmount);
        shoppingsAccount = new Plan2Account(percent2, totalAmount);
        circulationAccount = new Plan2Account(percent3, totalAmount);
    }

    private void LoadLocalMySqlData()
    {

    }

}

public class Plan2XmlData
{
    //One of the possible data sources.
    private string xmlPath;//Or add ONE parameter in constructor

    Plan2Account necesarryAccount = null;
    Plan2Account shoppingsAccount = null;
    Plan2Account circulationAccount = null;
    Plan2Account allAccounts = null;

    private Plan2XmlData()
    {
    }

    //Remove parameters on the future and make non-parametrised constructor the only one.
    //The procents must be stored,not used as inputs.
    public Plan2XmlData(int percent1, int percent2, int percent3, float totalAmount)
    {
        allAccounts = new Plan2Account(100, totalAmount);
        necesarryAccount = new Plan2Account(percent1, totalAmount);
        shoppingsAccount = new Plan2Account(percent2, totalAmount);
        circulationAccount = new Plan2Account(percent3, totalAmount);
    }

    private void LoadXmlData()
    {

    }
}

public class Plan2Data
{
    //Declare here the data used. We start with an XML
    //We CAN declare more data models/providers inside this class

    private Plan2XmlData xmlData = null;
    private Plan2MySqlData mySqlData = null;
    private PreferedDataProvider dataProvider;

    public Plan2Data(PreferedDataProvider dataProvider)
    {
        //We can keep all databases,or instantiate only one and remove dataProvider member in this class.


    }


}

public class Plan2BusinessLogic
{
    //Write business logic in this class

    Plan2Data dataRef = null;

    public Plan2BusinessLogic(ref Plan2Data dataRef)
    {
        this.dataRef = dataRef;
    }

}

public class Plan2Model : MonoBehaviour
{
    //THIS is the model's "BINDER"

    //For application-level operations. Manipulates the next object. It is the "controller" of the database
    private Plan2BusinessLogic logic = null;

    //A binder between multiple databases.This class provides the "abstractions" to manipulate data.
    //"dataProvider" is created using a mix of "Adapter", "Decorator" and "Facade" design patterns(a Wrapper)

    private Plan2Data dataProvider = null;
	
    // Use this for initialization
	void Start ()
    {
        dataProvider = new Plan2Data(PreferedDataProvider.XmlDataProvider);
        logic = new Plan2BusinessLogic(ref dataProvider);

        //Desired usage (in Business Logic class methods) : 
        /*
         * logic->SortAllReports()
         * {
         * dataProvider->Give me all the reports(database is automatically selected by API)
	       
           //sort reports here
           
           .//return sorted data
           }
        */
    }
	


}
