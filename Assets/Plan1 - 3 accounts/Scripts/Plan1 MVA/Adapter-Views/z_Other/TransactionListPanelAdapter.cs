using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class TransactionListPanelAdapter : MonoBehaviour {

    public GameObject transactionListElementPrefab;
    public GameObject transactionsScrollableGrid;
    public ScrollableGridResizer gridResizerReference;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void RefreshTransactionsPrefabs()
    {
        //Delete all child objects first

        foreach (Transform child in transactionsScrollableGrid.transform)
        {
            Destroy(child.gameObject);
        }

        //Add transactions

        List<Transaction> transactionsList = null;

        //Get the transactions list of that account
        switch (API.Instance.Logic.lastAccountPanelIndex)
        {
            case 0: { transactionsList = API.Instance.DataWrapper.LocalData.NecesarryAccount.Transactions; } break;
            case 1: { transactionsList = API.Instance.DataWrapper.LocalData.ShoppingsAccount.Transactions; } break;
            case 2: { transactionsList = API.Instance.DataWrapper.LocalData.CirculationAccount.Transactions; } break;
            case 3: { transactionsList = API.Instance.DataWrapper.LocalData.IncomeAccount.Transactions; } break;

            default: { /*DO NOTHING*/} break;
        }

        if(null != transactionsList && (transactionsList.Count > 0))
        {
            //Set items count for the scrollable elements container
            gridResizerReference.childItemsCount = transactionsList.Count;
            gridResizerReference.ResizeUsingCurrentInfo();

            foreach (Transaction tr in transactionsList)
            {
                GameObject tmp = Instantiate(transactionListElementPrefab) as GameObject;
                tmp.transform.SetParent(transactionsScrollableGrid.transform);

                tmp.transform.GetChild(0).GetComponent<Text>().text = tr.Description;
                tmp.transform.GetChild(1).GetComponent<Text>().text = GetFormattedStringFor(tr.DateTime);
                tmp.transform.GetChild(2).GetComponent<Text>().text = tr.Amount.ToString() + " " + API.Instance.DataWrapper.LocalData.CurrencyName;
            }
        }

    }

    private string GetFormattedStringFor(DateTime value)
    {
        return value.Day + "/" + value.Month + "/" + value.Year + " " + value.Hour + ":" + value.Minute + ":" + value.Second;
    }

}
