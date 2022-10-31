using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DPI : MonoBehaviour
{
    public Slider silder_horz;
    public Slider silder_vert;
    public void SliderValue()
    {
        GameManagerLogic.DPI = silder_horz.value;
    }
    public void SliderValue_up_down()
    {
        GameManagerLogic.up_down_DPI = silder_vert.value;
    }
}
