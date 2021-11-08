using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class menuBut : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void CreditScene()
    {
        SceneManager.LoadScene("Credits");
    }*/

    public void MainMenu()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void ControlsMenu()
    {
        SceneManager.LoadScene("HelpMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
