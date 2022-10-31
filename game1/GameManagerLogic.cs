using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerLogic : MonoBehaviour
{
    public int TotalCount = 0;
    public static bool spawn_control = false;
    public static bool game_stop = false;
    public static float DPI = 2.0f;
    public static float up_down_DPI = 2.0f;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void FirstLoad()

    {

        if (SceneManager.GetActiveScene().name.CompareTo("mainloby") != 0)

        {

            SceneManager.LoadScene("mainloby");

        }

    }
}
