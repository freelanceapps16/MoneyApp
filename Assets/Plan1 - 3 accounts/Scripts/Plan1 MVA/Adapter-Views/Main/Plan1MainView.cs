using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Plan1MainView : MonoBehaviour
{
    public Text necessarySpendingsCash;
    public Text otherSpendingsCash;
    public Text futureSpendingsCash;
    public Text monthlyIncomeCash;

    public Text monthlyTotalCash;

    private bool updateFinished;
    private bool fastLoadedAtStartup;

    void Start()
    {
        updateFinished = false;
        fastLoadedAtStartup = false;
        StartCoroutine(UpdateView());
    }

    void Update()
    {
        if (updateFinished)
        {
            updateFinished = false;
            StartCoroutine(UpdateView());
        }
    }

    IEnumerator UpdateView()
    {
        string startString = "Cash : ";

        necessarySpendingsCash.text = startString
            + (API.Instance.DataWrapper.LocalData.NecesarryAccount.AccountMoney - 
            NecessarySpendingsCashThisMonth()).ToString()
            + "/" + API.Instance.DataWrapper.LocalData.NecesarryAccount.AccountMoney.ToString();
        otherSpendingsCash.text = startString
            + (API.Instance.DataWrapper.LocalData.ShoppingsAccount.AccountMoney -
            OtherSpendingsCashThisMonth()).ToString()
            + "/" + API.Instance.DataWrapper.LocalData.ShoppingsAccount.AccountMoney.ToString();
        futureSpendingsCash.text = startString
            + (API.Instance.DataWrapper.LocalData.CirculationAccount.AccountMoney -
            FutureSpendingsCashThisMonth()).ToString()
            + "/" + API.Instance.DataWrapper.LocalData.CirculationAccount.AccountMoney.ToString();
        monthlyIncomeCash.text = startString + IncomeCashThisMonth().ToString();

        monthlyTotalCash.text = "\nTotal cash : " + API.Instance.DataWrapper.LocalData.CurrencyAmount.ToString();

        if (!fastLoadedAtStartup)
        {
            yield return new WaitForSeconds(0.5f);
            fastLoadedAtStartup = false;
        }
        else
        {
            yield return new WaitForSeconds(5.0f);
        }
        updateFinished = true;
        yield return null;
    }

    private float NecessarySpendingsCashThisMonth()
    {
        float total = 0.0f;
        int currentMonth = DateTime.Now.Month;

        foreach (Transaction tr in API.Instance.DataWrapper.LocalData.NecesarryAccount.Transactions)
        {
            if (tr.DateTime.Month == currentMonth)
                total += tr.Amount;
        }

        return total;
    }

    private float OtherSpendingsCashThisMonth()
    {
        float total = 0.0f;
        int currentMonth = DateTime.Now.Month;

        foreach (Transaction tr in API.Instance.DataWrapper.LocalData.ShoppingsAccount.Transactions)
        {
            if (tr.DateTime.Month == currentMonth)
                total += tr.Amount;
        }

        return total;
    }

    private float FutureSpendingsCashThisMonth()
    {
        float total = 0.0f;
        int currentMonth = DateTime.Now.Month;

        foreach (Transaction tr in API.Instance.DataWrapper.LocalData.CirculationAccount.Transactions)
        {
            if (tr.DateTime.Month == currentMonth)
                total += tr.Amount;
        }

        return total;
    }

    private float IncomeCashThisMonth()
    {
        float total = 0.0f;
        int currentMonth = DateTime.Now.Month;

        foreach (Transaction tr in API.Instance.DataWrapper.LocalData.IncomeAccount.Transactions)
        {
            if (tr.DateTime.Month == currentMonth)
                total += tr.Amount;
        }

        return total;
    }

}
