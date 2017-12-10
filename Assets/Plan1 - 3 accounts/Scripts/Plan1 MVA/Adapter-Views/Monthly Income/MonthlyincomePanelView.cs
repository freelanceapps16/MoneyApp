using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MonthlyincomePanelView : MonoBehaviour {

    public Text currentMoneyText;
    public Text totalMoneyText;
    public Text timeLeftText;

    private bool updateFinished;

    // Use this for initialization
    void Start()
    {
        updateFinished = false;
        StartCoroutine(UpdateFutureSpendingsView());
    }

    void Update()
    {
        if (updateFinished)
        {
            updateFinished = false;
            StartCoroutine(UpdateFutureSpendingsView());
        }
    }

    IEnumerator UpdateFutureSpendingsView()
    {
        float lastMonthIncome = IncomeCashThisMonth();
        float totalIncome = API.Instance.DataWrapper.LocalData.CurrencyAmount;
        string currencyName = API.Instance.DataWrapper.LocalData.CurrencyName;
        int daysLeft = 0;

        currentMoneyText.text = "Total income this month : " + lastMonthIncome.ToString() + " " + currencyName;
        totalMoneyText.text = "Total income : " + (totalIncome + lastMonthIncome).ToString() + " " + currencyName;
        timeLeftText.text = "Time left until next income : " + daysLeft + " day(s)";

        updateFinished = true;

        yield return null;
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
