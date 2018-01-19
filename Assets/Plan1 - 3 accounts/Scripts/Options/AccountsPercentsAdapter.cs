using UnityEngine;
using UnityEngine.UI;

public class AccountsPercentsAdapter : MonoBehaviour {

    public Slider slider1;
    public Slider slider2;
    public Slider slider3;

    // Use this for initialization
    void Start () {
	
	}

    public void SaveAccountsPercents()
    {
        API.Instance.DataWrapper.LocalData.NecesarryAccount.AccountPercent = (int)slider1.value;
        API.Instance.DataWrapper.LocalData.ShoppingsAccount.AccountPercent = (int)slider2.value;
        API.Instance.DataWrapper.LocalData.CirculationAccount.AccountPercent = (int)slider3.value;
        API.Instance.DataWrapper.LocalData.Save();
    }

}
