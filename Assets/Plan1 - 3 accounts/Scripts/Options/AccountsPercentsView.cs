using UnityEngine;
using UnityEngine.UI;

public class AccountsPercentsView : MonoBehaviour {

    //We have 3 sliders,with a sum of values of 100. The values should be between 1 and 98.

    public Slider slider1;
    public Slider slider2;
    public Slider slider3;

    private bool reachedMaximmum1;
    private bool reachedMaximmum2;
    private bool reachedMaximmum3;

    private float oldValue1;
    private float oldValue2;
    private float oldValue3;

    public Text percentValueDescriptor1;
    public Text percentValueDescriptor2;
    public Text percentValueDescriptor3;

    // Use this for initialization
    void Start () {
        reachedMaximmum1 = false;
        reachedMaximmum2 = false;
        reachedMaximmum3 = false;

        oldValue1 = slider1.value;
        oldValue2 = slider2.value;
        oldValue3 = slider3.value;

        Slider1ValueChanged();//If all 3 values are zero(at the beginning),sets the values to 1-98-1

        RecalculateSlidersValues();
    }

    public void RecalculateSlidersValues()
    {
        bool ok1 = TestMinimmumAndMaximmumRange(ref slider1, 1);
        bool ok2 = TestMinimmumAndMaximmumRange(ref slider2, 2);
        bool ok3 = TestMinimmumAndMaximmumRange(ref slider3, 3);

        if (ok1 && ok2 && ok3)
        {
            //Check for changed values

            if (slider1.value != oldValue1)
                Slider1ValueChanged();

            if (slider2.value != oldValue2)
                Slider2ValueChanged();

            if (slider3.value != oldValue3)
                Slider3ValueChanged();
        }
        else
        {
            if (!ok1)
            {
                if (!reachedMaximmum1)
                {
                    slider1.value = 1.0f;
                    oldValue1 = 1.0f;
                }
                else
                {
                    slider1.value = 98.0f;
                    oldValue1 = 98.0f;
                }
            }
            if (!ok2)
            {
                if (!reachedMaximmum2)
                {
                    slider2.value = 1.0f;
                    oldValue2 = 1.0f;
                }
                else
                {
                    slider2.value = 100 - slider1.value - 1;
                    oldValue3 = 100 - slider1.value - 1;
                }
            }
            if (!ok3)
            {
                if (!reachedMaximmum3)
                {
                    slider3.value = 1.0f;
                    oldValue3 = 1.0f;
                }
                else
                {
                    slider3.value = 100 - slider1.value - 1;
                    oldValue3 = 100 - slider1.value - 1;
                }
            }
        }

        UpdateViews();
    }

    private void UpdateViews()
    {
        percentValueDescriptor1.text = slider1.value.ToString() + "% from income";
        percentValueDescriptor2.text = slider2.value.ToString() + "% from income";
        percentValueDescriptor3.text = slider3.value.ToString() + "% from income";
    }

    private bool TestMinimmumAndMaximmumRange(ref Slider s, int index)
    {
        float value = s.value;
        bool ok = (value > 0 && value < 99);

        if(!ok)
        {
            float correctedValue = 0.0f;

            if(value > 98)
            {
                if (index == 2)
                    correctedValue = 100 - slider3.value - 1;
                else
                    if(index == 3)
                        correctedValue = 100 - slider2.value - 1;
            }

            switch(index)
            {
                case 1: { slider1.value = correctedValue; oldValue1 = correctedValue; reachedMaximmum1 = (correctedValue == 0 ? false : true); } break;
                case 2: { slider2.value = correctedValue; oldValue2 = correctedValue; reachedMaximmum2 = (correctedValue == 0 ? false : true); } break;
                case 3: { slider3.value = correctedValue; oldValue3 = correctedValue; reachedMaximmum3 = (correctedValue == 0 ? false : true); } break;
                default: {/* DO NOTHING */ }break;
            }
        }

        return ok;
    }

    private void Slider1ValueChanged()
    {
        //Lets the current value of slider1 unchanged,and changes value of slider3 to 1,and second with the difference

        oldValue1 = slider1.value;

        float otherSlidersTotal = 100 - slider1.value;

        slider2.value = otherSlidersTotal - 1;
        slider3.value = 1;
    }

    private void Slider2ValueChanged()
    {
        //Sets the value of slider2 to maximmum 100 - slider1Value - 1(for 3-rd slider)

        float last2SlidersMaxValue = 100 - slider1.value;

        if (slider2.value >= last2SlidersMaxValue)
            slider2.value = last2SlidersMaxValue - 1;

        oldValue2 = slider2.value;
        slider3.value = last2SlidersMaxValue - slider2.value;
        oldValue3 = slider3.value;
    }

    private void Slider3ValueChanged()
    {
        float last2SlidersMaxValue = 100 - slider1.value;

        if (slider3.value >= last2SlidersMaxValue)
            slider3.value = last2SlidersMaxValue - 1;

        oldValue3 = slider3.value;
        slider2.value = last2SlidersMaxValue - slider3.value;
        oldValue2 = slider2.value;
    }

}
