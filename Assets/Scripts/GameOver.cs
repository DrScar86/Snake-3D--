using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadLevel : MonoBehaviour
{ // время до загрузки уровня 
    public float delay = 3; // имя загружаемого уровня 
    public string levelName;
    public IEnumerator Start()
    { // задержка на заданное число секунд 
        yield return new WaitForSeconds(delay); // загрузка уровня с указанным именем 
        SceneManager.LoadScene("Menu4");
    }
}