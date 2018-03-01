using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ParentPercentOverwrite : MonoBehaviour {

    private float parentRectWidth;
    private float parentRectHeight;
   
    private LayoutElement layoutElementObject;
    public bool isUpdated;

    public float parentWidthPercent = 0;//default(0) means NO change
    public float parentHeightPercent = 0;//default(0) means NO change

    // Use this for initialization
    void Start()
    {
        isUpdated = false;
    }

    public void Update()
    {
        if (!isUpdated)
        {
            UpdateControl();
        }
    }

    private void UpdateControl()
    {
        RectTransform parentRect = gameObject.transform.parent.GetComponent<RectTransform>();

        if (null == parentRect)
            return;

        layoutElementObject = gameObject.transform.GetComponent<LayoutElement>();

        if (null == layoutElementObject)
            return;

        if ((parentRect.rect.width > 0) && (parentRect.rect.height > 0))
        {
            parentRectWidth = parentRect.rect.width;
            parentRectHeight = parentRect.rect.height;

            isUpdated = true;
            StartCoroutine(ProcessControl());
        }
    }

    public void RefreshUpdateControl()
    {
        isUpdated = false;
    }

    private IEnumerator ProcessControl()
    {
        if (null != layoutElementObject && isUpdated)
        {
            if ((parentWidthPercent > 0.0f) && (parentWidthPercent <= 100))
            {
                layoutElementObject.preferredWidth = (parentWidthPercent / 100.0f) * parentRectWidth;
            }

            if ((parentHeightPercent > 0.0f) && (parentHeightPercent <= 100.0f))
            {
                layoutElementObject.preferredHeight = (parentHeightPercent / 100) * parentRectHeight;
            }
        }

        //Notify Children that current size is changed

        ParentPercentOverwrite [] childScripts = gameObject.transform.GetComponentsInChildren<ParentPercentOverwrite>();

        foreach(ParentPercentOverwrite item in childScripts)
        {
            item.isUpdated = false;
        }

        yield return null;
    }
}
