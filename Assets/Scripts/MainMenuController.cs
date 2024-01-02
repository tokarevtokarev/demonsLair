using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainScreen, aboutScreen;
    public Button palyButton, backButton;

    // Start is called before the first frame update
    void Start()
    {
        palyButton.Select();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnButtonPlayGame()
    {
        SceneManager.LoadScene("TheLair");
    }

    public void OnButtonAboutGame()
    {
        mainScreen.SetActive(false);
        aboutScreen.SetActive(true);
        backButton.Select();
    }

    public void OnButtonBackGame()
    {
        mainScreen.SetActive(true);
        aboutScreen.SetActive(false);
        palyButton.Select();
    }

    public void OnButtonQuit()
    {
        Application.Quit();
    }
}
