using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShoppingListItem : MonoBehaviour {
    public void DestroyCurrentItem()
    {
        Transform priceItem = gameObject.transform.GetChild(1).GetChild(0);
        InputField iField = priceItem.GetComponent<InputField>();
        iField.text = "";

        NotifyPropertyChanged();

        Destroy(gameObject);
    }

    public void NotifyPropertyChanged()
    {
        ShoppingListView view = gameObject.transform.parent.transform.parent.GetComponent<ShoppingListView>();

        if(null != view)
        {
            view.CalculateTotal();
        }

    }

}
