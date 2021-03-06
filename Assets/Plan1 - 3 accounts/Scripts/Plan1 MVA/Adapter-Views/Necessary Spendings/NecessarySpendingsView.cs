﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class NecessarySpendingsView : MonoBehaviour {

    public Text currentMoneyText;
    public Text totalMoneyText;
    public Text timeLeftText;

    private bool updateFinished;

	// Use this for initialization
	void Start () {
        updateFinished = false;
        StartCoroutine(UpdateNecessarySpendingsView());
    }
	
	// Update is called once per frame
	void Update () {
        if (updateFinished)
        {
            updateFinished = false;
            StartCoroutine(UpdateNecessarySpendingsView());
        }
    }

    IEnumerator UpdateNecessarySpendingsView()
    {
        float moneySpentThisMonth = NecessarySpendingsCashThisMonth();
        float totalMoneyThisMonth = API.Instance.DataWrapper.LocalData.NecesarryAccount.AccountMoney;
        string currencyName = API.Instance.DataWrapper.LocalData.CurrencyName;

        int daysLeft = 0;

        currentMoneyText.text = "Money left this month : " + (totalMoneyThisMonth - moneySpentThisMonth).ToString() + " " + currencyName;
        totalMoneyText.text = "Total money this month : " + totalMoneyThisMonth.ToString() + " " + currencyName;
        timeLeftText.text = "Time left until next income : " + daysLeft + " day(s)";

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

}
