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

    #region Method Properties

    public string TransactionType
    {
        get { return transactionType; }
        set { if (value != "none") { transactionType = value; } }
    }

    public float Amount
    {
        get { return amount; }
        set { if (value > 0.0f) { amount = value; } }
    }

    public DateTime DateTime
    {
        get { return dateTime; }
    }

    public string Description
    {
        get { return description; }
    }

    public long ID
    {
        get { return id; }
    }

    #endregion

    public Transaction()
    {
        transactionType = "none";
        amount = 0.0f;
    }

    public Transaction(string transactionType = "none", float amount = 0.0f)
    {
        this.transactionType = transactionType;
        this.amount = amount;
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

        //set
        //{
        //    if (value > 0)
        //        accountMoney = value;
        //}
    }

    public int AccountPercent
    {
        get
        {
            return accountPercent;
        }

    }

    public List<Transaction> Transactions
    {
        get { return transactions; }
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
