using UnityEngine;
using System.Collections.Generic;
using System;

#region Common definitions (global namespace)



/// <summary>
/// Common : Data provider enumeration
/// </summary>

public enum PreferedDataProvider
{
    None,
    XmlDataProvider,
    MySqlDataProvider,
    MsSqlServerDataProvider
    //and so on
}

/// <summary>
/// Common : Generic Application Transaction definition
/// </summary>

public class Transaction
{
    string transactionType;// "none" / "income" / "spending"
    float amount;
    DateTime dateTime;
    string description;
    long id;

    private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    #region Method Properties

    public string TransactionType
    {
        get { return transactionType; }
        set { if (value != "none")
              {
                    transactionType = value;
              }
              else
              {
                transactionType = "none"; 
              }
                
            }
    }

    public float Amount
    {
        get { return amount; }
        set { if (value > 0.0f) { amount = value; } }
    }

    public DateTime DateTime
    {
        get { return dateTime; }
        set { if (value != null) dateTime = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public long ID
    {
        get { return id; }
        set { id = value; }
    }

    #endregion

    public Transaction()
    {
        transactionType = "none";
        amount = 0.0f;
        description = "";
        id = 0;
        dateTime = DateTime.Now;
    }

    public Transaction(string transactionType, float amount)
    {
        this.transactionType = transactionType;
        this.amount = amount;
        description = "";
        id = 0;
        dateTime = DateTime.Now;
    }

    public static DateTime DateTimeFromUnixTime(long unixTime)
    {
        return epoch.AddSeconds(unixTime);
    }
}

/// <summary>
/// Common : Generic Application Account definition
/// </summary>

public class Account
{
    private float accountMoney;
    private int accountPercent;

    List<Transaction> transactions;

    private Account()
    {
        transactions = new List<Transaction>();
    }

    public Account(int percentFrom, float totalMoney)
    {
        transactions = new List<Transaction>();

        accountMoney = (totalMoney * accountPercent) / 100;
        accountPercent = percentFrom;
    }

    public float AccountMoney
    {
        get
        {
            return accountMoney;
        }

        set
        {
            if (value > 0)
                accountMoney = value;
        }
    }

    public int AccountPercent
    {
        get
        {
            return accountPercent;
        }

        set
        {
            if (value > 0)
                accountMoney = value;
        }
    }

    public List<Transaction> Transactions
    {
        get { return transactions; }
        set { if (value != null) transactions = value; }
    }

    public void ResetTo(int percentFrom, float totalMoney)
    {
        accountMoney = (totalMoney * accountPercent) / 100;
        accountPercent = percentFrom;
    }

}

/// <summary>
/// Common : Base account for "Plan1" type of logic.
/// </summary>


public class Plan1Account : Account
{
    private Plan1Account() : base(0, 0)
    {
    }

    public Plan1Account(int percentFrom, float totalMoney) : base(percentFrom, totalMoney)
    {

    }

    public void ResetToValueOf(AppXmlCommon.XmlAccount xAccount)
    {
        AccountMoney = xAccount.accountMoney;
        AccountPercent = xAccount.accountPercent;

        Transactions.Clear();
        List<Transaction> tmpTransactions = new List<Transaction>();

        foreach(AppXmlCommon.XmlTransaction xTransaction in xAccount.transactions)
        {
            tmpTransactions.Add(TransactionFromXmlTransaction(xTransaction));
        }

        Transactions = tmpTransactions;
    }

    private Transaction TransactionFromXmlTransaction(AppXmlCommon.XmlTransaction xTransaction)
    {
        Transaction tmp = new Transaction();

        tmp.TransactionType = xTransaction.transactionType;
        tmp.Amount = xTransaction.amount;
        tmp.Description = xTransaction.description;
        tmp.ID = xTransaction.id;
        tmp.DateTime = Transaction.DateTimeFromUnixTime(xTransaction.dateTime);

        return tmp;
    }

}

/// <summary>
/// Common : Base data provider with all Plan1 accounts stored inside
/// </summary>

public class Plan1Data
{
    Plan1Account necesarryAccount = null;
    Plan1Account shoppingsAccount = null;
    Plan1Account circulationAccount = null;
    Plan1Account allAccounts = null;

    string currencyName;
    float currencyAmount;

#region Properties for private variables

    public Plan1Account NecesarryAccount
    {
        get { return necesarryAccount; }
    }

    public Plan1Account ShoppingsAccount
    {
        get { return shoppingsAccount; }
    }

    public Plan1Account CirculationAccount
    {
        get { return circulationAccount; }
    }

    public Plan1Account AllAccounts
    {
        get { return allAccounts; }
    }

    public string CurrencyName
    {
        get { return currencyName; }
        set {
                if(value.Length > 0)
                {
                    currencyName = value;
                }
            }
    }

    public float CurrencyAmount
    {
        get { return currencyAmount; }
        set {
                if (value > 0.0f)
                {
                    currencyAmount = value;
                }
            
            }
    }
#endregion

    //NOT used preferably. Protected to allow inheritance
    protected Plan1Data()
    {
        
    }

    public virtual void Refresh()
    {

    }
    

    public virtual void Save()
    {

    }

    public Plan1Data(string currencyName = "Money", float totalAmount = 0.0f)
    {
        this.currencyName = currencyName;
        currencyAmount = totalAmount;

        necesarryAccount = new Plan1Account(0, 0);
        shoppingsAccount = new Plan1Account(0, 0);
        circulationAccount = new Plan1Account(0, 0);
        allAccounts = new Plan1Account(0, 0);
    }

    public void ResetAccountsPercentTo(int percent1, int percent2, int percent3, float totalAmount)
    {
        currencyAmount = totalAmount;

        necesarryAccount.ResetTo(percent1, currencyAmount);
        shoppingsAccount.ResetTo(percent2, currencyAmount);
        circulationAccount.ResetTo(percent3, currencyAmount);
        allAccounts.ResetTo(100, currencyAmount);
    }

    

}

#endregion

















public class CommonModel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
