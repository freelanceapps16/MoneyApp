using UnityEngine;
using UnityEngine.UI;

public class DeveloperApplication : MonoBehaviour
{
    public Button btnResetInternalData;
    public Text welcomeTextAvatarName;

    public bool z_EnableDeveloperMode;


	// Use this for initialization
	void Start () {
        if(z_EnableDeveloperMode)
        {
            btnResetInternalData.gameObject.SetActive(true);
            welcomeTextAvatarName.gameObject.SetActive(false);
        }
        else
        {
            btnResetInternalData.gameObject.SetActive(false);
            welcomeTextAvatarName.gameObject.SetActive(true);
        }

	}
	
    public void ResetInternalData()
    {
        API.Instance.DataWrapper.LocalData.NecesarryAccount.AccountMoney = 0;
        API.Instance.DataWrapper.LocalData.NecesarryAccount.AccountPercent = 0;
        API.Instance.DataWrapper.LocalData.NecesarryAccount.Transactions.Clear();

        API.Instance.DataWrapper.LocalData.ShoppingsAccount.AccountMoney = 0;
        API.Instance.DataWrapper.LocalData.ShoppingsAccount.AccountPercent = 0;
        API.Instance.DataWrapper.LocalData.ShoppingsAccount.Transactions.Clear();

        API.Instance.DataWrapper.LocalData.CirculationAccount.AccountMoney = 0;
        API.Instance.DataWrapper.LocalData.CirculationAccount.AccountPercent = 0;
        API.Instance.DataWrapper.LocalData.CirculationAccount.Transactions.Clear();

        API.Instance.DataWrapper.LocalData.IncomeAccount.AccountMoney = 0;
        API.Instance.DataWrapper.LocalData.IncomeAccount.AccountPercent = 0;
        API.Instance.DataWrapper.LocalData.IncomeAccount.Transactions.Clear();

        API.Instance.DataWrapper.LocalData.Save();
    }
	
}
