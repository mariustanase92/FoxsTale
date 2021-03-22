using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScene : MonoBehaviour
{
    [SerializeField] private string _mainMenu;

    public void MainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
