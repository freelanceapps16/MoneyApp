using UnityEngine;
using System.Collections;

public class NewTransactionPanelAdapter : MonoBehaviour
{
    public NewTransactionPanelView viewReference;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddTransaction()
    {
        Transaction tr = new Transaction("none",0.0f);

        switch (API.Instance.Logic.lastAccountPanelIndex)
        {
            case 0: { tr.TransactionType = "spending"; } break;
            case 1: { tr.TransactionType = "spending"; } break;
            case 2: { tr.TransactionType = "spending"; } break;
            case 3: { tr.TransactionType = "income"; } break;

            default: { /*DO NOTHING*/} break;
        }

        //Transaction type is set from parametrized constructor
        //DateTime is set from constructor
        tr.ID = API.Instance.Logic.NextTransactionID();

        //Amount and description left.
        //We take them from the controls of the view

        tr.Description = viewReference.transactionDescriptionText.text;
        tr.Amount = (float) System.Convert.ToDouble(viewReference.transactionAmountText.text);

        switch(API.Instance.Logic.lastAccountPanelIndex)
        {
            case 0: { API.Instance.DataWrapper.LocalData.NecesarryAccount.Transactions.Add(tr); } break;
            case 1: { API.Instance.DataWrapper.LocalData.ShoppingsAccount.Transactions.Add(tr); } break;
            case 2: { API.Instance.DataWrapper.LocalData.CirculationAccount.Transactions.Add(tr); } break;
            case 3: { API.Instance.DataWrapper.LocalData.IncomeAccount.Transactions.Add(tr); } break;

            default: { /*DO NOTHING*/} break;
        }

        if(API.Instance.Logic.lastAccountPanelIndex == 3)
        {//income
            AddIncomeAmount(tr.Amount);
        }

        API.Instance.DataWrapper.LocalData.Save();
    }

    private void AddIncomeAmount(float amount)
    {
        API.Instance.DataWrapper.LocalData.IncomeAccount.AccountMoney += amount;

        float totalfirst2 = 0;
        float accountAmount = 0;

        accountAmount = (amount / 100) * API.Instance.DataWrapper.LocalData.NecesarryAccount.AccountPercent;
        API.Instance.DataWrapper.LocalData.NecesarryAccount.AccountMoney += accountAmount;
        totalfirst2 += accountAmount;

        accountAmount = (amount / 100) * API.Instance.DataWrapper.LocalData.ShoppingsAccount.AccountPercent;
        API.Instance.DataWrapper.LocalData.ShoppingsAccount.AccountMoney += accountAmount;
        totalfirst2 += accountAmount;

        API.Instance.DataWrapper.LocalData.CirculationAccount.AccountMoney += (amount - totalfirst2);//to avoid float-errors
    }

}
