using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDriver : MonoBehaviour 
{

    public void GameStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Primary");
    }

    public void GameQuit()
    {
        Application.Quit();
    }

}
