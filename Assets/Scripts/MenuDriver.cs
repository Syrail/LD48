using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDriver : MonoBehaviour 
{

    public void GameStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Assets/Scenes/Syrail_Test.unity");
    }

    public void GameQuit()
    {
        Application.Quit();
    }

}
