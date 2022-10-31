using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int Score = 0;

    private Text show;
    private void Start()
    {
        show = GetComponent<Text>();
    }
    private void Update()
    {
        show.text = "Score: " + Score;
    }

}
