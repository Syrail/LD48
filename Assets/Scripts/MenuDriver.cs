using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDriver : MonoBehaviour 
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GameStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Primary");
    }

    public void GameQuit()
    {
        Application.Quit();
    }

}
