using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public ClientController client;

    public void createRoom()
    {
        //SceneManager.LoadScene("Battle");
        client.createTable();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
