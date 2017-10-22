using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*
 * Script to stretch child elements of a GridLayoutGroup to fit to content. It can force child items to be square.
  
 * Usage : 

   1. Attach to any object which already contains a GridLayoutGroup component
   2. Make settings in the "GridLayoutGroup" component
   3. Check if you want to force child components to be square instead of rectangular

   The script will re-arrange components when a RectTransform width and height value bigger than zero is provided(after first frame,usually).
     
*/


public class DynamicGrid : MonoBehaviour
{
    private GridLayoutGroup gridLayoutGroup;
    private int itemsCount;

    private int rows;
    private int columns;

    private float parentRectWidth;
    private float parentRectHeight;

    //It makes items square,in function of lowest of the width and height
    public bool forceSquareItems;

    private bool elementsReady;//used to refresh RectTransform of current object(an empty ones is available at first)

    void Start ()
    {
        elementsReady = false;
    }

    void Update ()
    {
        if (!elementsReady)
        {
            RectTransform parentRect = GetComponent<RectTransform>();

            if ((parentRect.rect.width > 0) && (parentRect.rect.height > 0))
            {
                elementsReady = true;
                parentRectWidth = parentRect.rect.width;
                parentRectHeight = parentRect.rect.height;
                StartCoroutine(PositionElements());
            }
        }
    }

    private IEnumerator PositionElements()
    {
        bool UIError = false;
        gridLayoutGroup = GetComponent<GridLayoutGroup>();

        RectOffset padding = gridLayoutGroup.padding;
        Vector2 spacing = gridLayoutGroup.spacing;
        itemsCount = gameObject.transform.childCount;

        GridLayoutGroup.Constraint constraint = gridLayoutGroup.constraint;

        int constraintCount = gridLayoutGroup.constraintCount;

        if (constraint.ToString() == "Flexible")
        {
            //Constraint is "Flexible". So let's keep the columns count fixed.

            float columnsAproximate = Mathf.Sqrt(itemsCount);
            if (columnsAproximate > 0)
            {
                columns = (((int)columnsAproximate) == columnsAproximate) ? (int)columnsAproximate : ((int)columnsAproximate) + 1;
                rows = (itemsCount % columns == 0) ? itemsCount / columns : (itemsCount / columns) + 1;
            }
            else
            {
                columns = 0;
                rows = 0;

                print("No child items in current object !");
                UIError = true;
            }
        }
        else
        {
            //We have a constraint count on rows/columns. Check which

            string constraintStr = gridLayoutGroup.constraint.ToString();

            if (constraintStr == "FixedRowCount")
            {
                rows = constraintCount;
                columns = (itemsCount % rows == 0) ? itemsCount / rows : (itemsCount / rows) + 1;
            }
            else
            {
                if (constraintStr == "FixedColumnCount")
                {
                    columns = constraintCount;
                    rows = (itemsCount % columns == 0) ? itemsCount / columns : (itemsCount / columns) + 1;
                }
                else
                {
                    print("Unknown constraint error in GridLayoutGroup !");
                    UIError = true;
                }
            }

        }

        if (!UIError)
        {
            float sizeX = 0.0f;
            float sizeY = 0.0f;

            //X-Coordinate stretch in function of padding and spacing
            float allColumnsSize = parentRectWidth - padding.left - padding.right;
            float spacingSizeLine = (columns - 1) * spacing.x;
            allColumnsSize -= spacingSizeLine;
            sizeX = allColumnsSize / columns;

            //Y-Coordinate stretch in function of padding and spacing
            float allRowsSize = parentRectHeight - padding.top - padding.bottom;
            float spacingSizeColumn = (rows - 1) * spacing.y;
            allRowsSize -= spacingSizeColumn;
            sizeY = allRowsSize / rows;

            if (forceSquareItems)
            {
                if (sizeX < sizeY)
                    sizeY = sizeX;
                else
                    sizeX = sizeY;
            }

            gridLayoutGroup.cellSize = new Vector2(sizeX, sizeY);
        }

        yield return null;
    }

}
