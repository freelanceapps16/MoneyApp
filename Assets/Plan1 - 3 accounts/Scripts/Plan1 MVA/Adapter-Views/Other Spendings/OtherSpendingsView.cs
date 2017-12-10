using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OtherSpendingsView : MonoBehaviour {

    public Text currentMoneyText;
    public Text totalMoneyText;
    public Text timeLeftText;

    private bool updateFinished;

    // Use this for initialization
    void Start()
    {
        updateFinished = false;
        StartCoroutine(UpdateOtherSpendingsView());
    }

    void Update()
    {
        if (updateFinished)
        {
            updateFinished = false;
            StartCoroutine(UpdateOtherSpendingsView());
        }
    }

    IEnumerator UpdateOtherSpendingsView()
    {
        float moneySpentThisMonth = OtherSpendingsCashThisMonth();
        float totalMoneyThisMonth = API.Instance.DataWrapper.LocalData.ShoppingsAccount.AccountMoney;
        string currencyName = API.Instance.DataWrapper.LocalData.CurrencyName;
        int daysLeft = 0;

        currentMoneyText.text = "Money left this month : " + (totalMoneyThisMonth - moneySpentThisMonth).ToString() + " " + currencyName;
        totalMoneyText.text = "Total money this month : " + totalMoneyThisMonth.ToString() + " " + currencyName;
        timeLeftText.text = "Time left until next income : " + daysLeft + " day(s)";

        updateFinished = true;

        yield return null;
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
}
