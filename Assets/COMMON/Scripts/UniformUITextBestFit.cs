using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic
    ;
/*
    Usage :
    1.  Add Text components inside the array in editor
    2.  Set for each Text component the best-fit option
    3.  You can chech the "acceptMultiLineTexts" if you want to have multi-line texts in your UI controls.
        Otherwise,the font size will be again decreased,to fit all texts in a single line.

     */

public class UniformUITextBestFit : MonoBehaviour {

    public List<Text> textList;
    public bool acceptMultiLineTexts = false;//to decrease font even more,until all texts are single line
    public int computedMinimmumSizeResult = 0;//Just to display the computed size in Editor

    // Use this for initialization
    void Start ()
    {
        ComputeTextsCommonSize();
    }

    public void ComputeTextsCommonSize()
    {
        //Set the maximmum font size to fit. Useful for high resolution devices.
        foreach (Text item in textList)
            item.resizeTextMaxSize = 300;

        StartCoroutine(SetMinimmumCommonSize());
    }

    private IEnumerator SetMinimmumCommonSize()
    {
        //Wait a frame,for the Engine to compute controls size
        yield return null;

        computedMinimmumSizeResult = textList[0].cachedTextGenerator.fontSizeUsedForBestFit;

        foreach (Text item in textList)
        {
            if (item.cachedTextGenerator.fontSizeUsedForBestFit < computedMinimmumSizeResult)
                computedMinimmumSizeResult = item.cachedTextGenerator.fontSizeUsedForBestFit;
        }

        ApplyTextSize(computedMinimmumSizeResult);
        yield return null;//wait a frame

        if (!acceptMultiLineTexts)
        {
            while (ContainsMultilineTexts())
            {
                computedMinimmumSizeResult--;
                ApplyTextSize(computedMinimmumSizeResult);
            }
        }

        yield return null;
    }

    private bool ContainsMultilineTexts()
    {
        Canvas.ForceUpdateCanvases();
        foreach (Text item in textList)
        {
            if (item.cachedTextGenerator.lineCount > 1)
                return true;
        }

        return false;
    }

    private void ApplyTextSize(int size)
    {
        foreach (var item in textList)
            item.resizeTextMaxSize = size;
    }

}
