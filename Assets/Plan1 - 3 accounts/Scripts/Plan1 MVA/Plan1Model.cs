using UnityEngine;
using System.Collections;
using AppXmlCommon;

public class Plan1MySqlData : Plan1Data
{
    private string dbPath;//Or add ONE parameter in constructor

    public Plan1MySqlData(string dbPath) : base("Money", 0.0f)
    {
        this.dbPath = dbPath;
        LoadLocalMySqlData();
    }

    public Plan1MySqlData(string currencyName, float totalAmount, string dbPath) : base(currencyName, totalAmount)
    {
        this.dbPath = dbPath;
        LoadLocalMySqlData();
    }

    private void LoadLocalMySqlData()
    {
        
    }

}

public class Plan1XmlData : Plan1Data
{
    //One of the possible data sources.
    private string xmlPath;
    private MainXmlData xData;

    public Plan1XmlData(string xmlPath) : base("Money", 0.0f)
    {
        this.xmlPath = xmlPath;
        xData = new MainXmlData();
    }

    public Plan1XmlData(string currencyName, float totalAmount, string xmlPath):base(currencyName, totalAmount)
    {
        this.xmlPath = xmlPath;
        xData = new MainXmlData();
    }

    public override void Refresh()
    {
        XmlDataHandler.LoadDataFromFile(xmlPath, ref xData);
        XmlToInternalData();
    }

    public override void Save()
    {
        InternalDataToXml();
        XmlDataHandler.SaveDataToFile(xmlPath, ref xData);
    }

    //To put data in our class,from the MainXmlData object
    private void XmlToInternalData()
    {
        CurrencyName = xData.currencyName;
        CurrencyAmount = xData.currencyAmount;
        LastTransactionID = xData.lastTransactionID;

        if(xData.accounts.Count == 4)
        {
            NecesarryAccount.ResetToValueOf(xData.accounts[0]);
            ShoppingsAccount.ResetToValueOf(xData.accounts[1]);
            CirculationAccount.ResetToValueOf(xData.accounts[2]);
            IncomeAccount.ResetToValueOf(xData.accounts[3]);

        }
    }

    //To put data in the MainXmlData object,from our class
    private void InternalDataToXml()
    {
        //We cannot pass "this" as reference parameter so we use a method described above

        xData.currencyName = CurrencyName;
        xData.currencyAmount = CurrencyAmount;
        xData.lastTransactionID = LastTransactionID;
        xData.accounts.Clear();

        xData.accounts.Add(XmlAccountFromPlan1Account(NecesarryAccount));
        xData.accounts.Add(XmlAccountFromPlan1Account(ShoppingsAccount));
        xData.accounts.Add(XmlAccountFromPlan1Account(CirculationAccount));
        xData.accounts.Add(XmlAccountFromPlan1Account(IncomeAccount));
    }

    public XmlAccount XmlAccountFromPlan1Account(Plan1Account account)
    {
        XmlAccount xAccount = new XmlAccount();

        xAccount.accountMoney = account.AccountMoney;
        xAccount.accountPercent = account.AccountPercent;

        foreach(Transaction tr in account.Transactions)
        {
            XmlTransaction tmpTrans = new XmlTransaction();

            tmpTrans.amount = tr.Amount;
            tmpTrans.description = tr.Description;
            tmpTrans.transactionType = tr.TransactionType;
            tmpTrans.dateTime = Transaction.DateTimeSecondsFromEpochFor(tr.DateTime);
            tmpTrans.id = tr.ID;

            xAccount.transactions.Add(tmpTrans);
        }


        return xAccount;
    }

}

public class Plan1DataWrapper
{
    //Declare here the data used. We start with an XML
    //We CAN declare more data models/providers inside this class. TODO(in future) : Add online sockets data providers

    private Plan1Data localData;

    public Plan1Data LocalData
    {
        get { return localData;}
    }

    private PreferedDataProvider dataProvider;

    public Plan1DataWrapper(PreferedDataProvider dataProvider, string locationParam)
    {
        //We can keep all databases,or instantiate only one and remove dataProvider member in this class.
        this.dataProvider = dataProvider;

        switch (dataProvider)
        {
            case PreferedDataProvider.XmlDataProvider: { localData = new Plan1XmlData(locationParam); } break;

            default: { localData = new Plan1Data(); } break;
        }
    }

    public PreferedDataProvider GetPreferedDataProvider()
    {
        return dataProvider;
    }

}

public class Plan1BusinessLogic
{
    //Write business logic in this class

    Plan1DataWrapper dataRef = null;

    public int lastAccountPanelIndex;//0=necessary,1=next...

    public Plan1BusinessLogic(ref Plan1DataWrapper dataRef)
    {
        this.dataRef = dataRef;
    }

    public int NextTransactionID()
    {
        dataRef.LocalData.LastTransactionID++;
        return dataRef.LocalData.LastTransactionID;
    }

}

public class Plan1ModelWrapper
{
    //THIS is the model's "BINDER"

    //For application-level operations. Manipulates the next object. It is the "controller" of the database
    private Plan1BusinessLogic logic = null;

    public Plan1BusinessLogic Logic
    {
        get { return logic; }
    }

    //A binder between multiple databases.This class provides the "abstractions" to manipulate data.
    //"dataProvider" is created using a mix of "Adapter", "Decorator" and "Facade" design patterns(a Wrapper)

    private Plan1DataWrapper dataProvider = null;

    public Plan1DataWrapper DataWrapper
    {
        get { return dataProvider; }
    }

    // Use this for initialization
    public Plan1ModelWrapper(PreferedDataProvider preferedDataProvider, string locationParam)
    {
        dataProvider = new Plan1DataWrapper(preferedDataProvider, locationParam);
        logic = new Plan1BusinessLogic(ref dataProvider);

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

public class Plan1Model : MonoBehaviour
{
   
}
