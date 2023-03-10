using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu2 : MonoBehaviour
{
    private string LostLevel;

    void Start()
    {
        LostLevel = PlayerPrefs.GetString("LostLevel");
    }
    public void LoadLostLevel()
    {
        SceneManager.LoadScene(3);
    }
    public void OnPlayHandler()
    {
        SceneManager.LoadScene(1);
    }
    public void OnExitHandler()
    {
        Application.Quit();
    }
}