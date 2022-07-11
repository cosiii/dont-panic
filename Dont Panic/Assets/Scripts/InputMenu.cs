using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputMenu : MonoBehaviour
{
     public Touch t;

     public bool three, six;

     public GameObject sectionOne, sectionTwo;
    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
 public void Update(){
    UpdateToken();

    if(six == true){
        PlayGame();
    }
    }

	public void UpdateToken () {
        int i = 0;
        while(i < Input.touchCount){
            t = Input.GetTouch(i);

            if(i == 0){
            }
            
            if(i == 1){
            }

            if(i == 2){ 
                three = true;
            } 

            if(i == 5){ 
                six = true;
            } 
            ++i;
	}
}
}
