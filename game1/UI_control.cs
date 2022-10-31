using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_control : MonoBehaviour
{
    [SerializeField]
    private GameObject game_ui;
    [SerializeField]
    private GameObject menu_ui;

    private bool chk = true;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(chk == true)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                game_ui.SetActive(false);
                menu_ui.SetActive(true);
                GameManagerLogic.game_stop = true;
                chk = false;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                game_ui.SetActive(true);
                menu_ui.SetActive(false);
                GameManagerLogic.game_stop = false;
                chk = true;
            }
            
        }
    }
}
