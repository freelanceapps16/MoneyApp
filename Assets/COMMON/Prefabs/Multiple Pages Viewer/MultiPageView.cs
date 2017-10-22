using UnityEngine;
using UnityEngine.UI;

public class MultiPageView : MonoBehaviour {

    public GameObject[] panels;
    public GameObject circularItemPrefab;
    public GameObject bottomCircularItemsPanel;
    public GameObject contentPanel;
    public Sprite selectedStateSprite;
    public Sprite normalStateSprite;


    private int selectedindex;

    void Start ()
    {
        selectedindex = 0;

        //Instantiate point objects
        for (int i = 0; i < panels.Length; ++i)
        {
            GameObject obj = Instantiate(circularItemPrefab) as GameObject;
            obj.transform.SetParent(bottomCircularItemsPanel.transform);
        }

        //Scale is BROKEN after instantiation,so re-set it to normal scale
        for(int i=0; i < bottomCircularItemsPanel.transform.childCount; ++i)
        {
            bottomCircularItemsPanel.transform.GetChild(i).localScale = Vector3.one;
        }

        //Set "normal" sprites
        for(int i=0; i < bottomCircularItemsPanel.transform.childCount; ++i)
        {
            bottomCircularItemsPanel.transform.GetChild(i).GetComponent<Image>().sprite = normalStateSprite;
        }

        //Set "highlight" sprite
        bottomCircularItemsPanel.transform.GetChild(0).GetComponent<Image>().sprite = selectedStateSprite;

        DisplayPanel(selectedindex);

    }
    
    public void GoNextPage()
    {
        if(selectedindex < panels.Length - 1)
        {//NOT the last page

            int nextIndex = selectedindex + 1;

            DisplayPanel(nextIndex);

            {//Solve "points"
                bottomCircularItemsPanel.transform.GetChild(selectedindex).GetComponent<Image>().sprite = normalStateSprite;
                bottomCircularItemsPanel.transform.GetChild(nextIndex).GetComponent<Image>().sprite = selectedStateSprite;
            }

            selectedindex = nextIndex;
        }
        else
        {
            print("Last Page Already !");
        }

    }

    public void GoPreviousPage()
    {
        if(selectedindex > 0)
        {//NOT the first page

            int nextIndex = selectedindex - 1;

            DisplayPanel(nextIndex);

            {//Solve "points"
                bottomCircularItemsPanel.transform.GetChild(selectedindex).GetComponent<Image>().sprite = normalStateSprite;
                bottomCircularItemsPanel.transform.GetChild(nextIndex).GetComponent<Image>().sprite = selectedStateSprite;
            }

            selectedindex = nextIndex;
        }
        else
        {
            print("First Page Already !");
        }
    }

    private void DisplayPanel(int index)
    {
        //If we have childrean anymore,remove them

        if(contentPanel.transform.childCount > 0)
        {
            for(int i = 0; i < contentPanel.transform.childCount; ++i)
            {
                Destroy(contentPanel.transform.GetChild(i).gameObject);
            }
            
        }

        //Set the panel as an instantiated copy

        GameObject tmp = Instantiate(panels[index]) as GameObject;
        tmp.transform.SetParent(contentPanel.transform);
        tmp.transform.localScale = Vector3.one;

        RectTransform tf = tmp.GetComponent<RectTransform>();

        //Set to stretch the container
        tf.anchorMin = Vector2.zero;
        tf.anchorMax = Vector2.one;

        tf.localPosition = Vector3.zero;

        //Set object as big as parent
        tf.sizeDelta = Vector2.zero;

       

    }

}
