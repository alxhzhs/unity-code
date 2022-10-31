using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class title : MonoBehaviour
{
    public string SceneToLoad;
    [SerializeField]
    private GameObject game_ui;
    [SerializeField]
    private GameObject menu_ui;
    public void LoadGame()
    {
        ScoreManager.Score = 0;
        SceneManager.LoadScene(SceneToLoad);
        GameManagerLogic.game_stop = false;
    }
    public void ReturnGame()
    {
        Cursor.visible = false;                 //Ä¿¼­ ¼û±â±â
        Cursor.lockState = CursorLockMode.Locked;
        GameManagerLogic.game_stop = false;
        game_ui.SetActive(true);
        menu_ui.SetActive(false);
    }
    public void gameout()
    {
        Application.Quit();
    }
}
