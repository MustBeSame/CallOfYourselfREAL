using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public ClientController client;

    public void createRoom()
    {
        //SceneManager.LoadScene("Battle");
        client.InicializaConexao();
    }

    public void joinRoom()
    {
        //SceneManager.LoadScene("Battle");
        client.JoinGame();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
