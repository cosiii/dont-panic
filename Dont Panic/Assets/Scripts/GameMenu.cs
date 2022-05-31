using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
  public void OneBack(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Debug.Log("oneback");
    }

  public void TwoBack(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        Debug.Log("twoback");
    }
}
