using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputMenu : MonoBehaviour
{
     public Touch t1, t2, t3, t4, t5, t6;

     public bool three, six, firstUp, firstDown, secondUp, secondDown;
    [SerializeField] public GameObject  inputFeedbackDown, inputFeedbackUp; 

    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
 public void Update(){
    UpdateTokens();

    ShowInputMenuFeedback();

    if((firstDown && secondUp) || (firstUp && secondDown)){
        PlayGame();
    }
    
    firstDown = false;
    firstUp = false;
    secondDown = false;
    secondUp = false;
    }

	public void UpdateTokens () {
        int i = 0;
        while(i < Input.touchCount){
            t1 = Input.GetTouch(i);

            if(i == 0){
            }
            
            if(i == 1){
                t2 = t1;
            }

            if(i == 2){ 
                t3 = t1;
                // PLAYER TWO | UP
                if( t1.position.y >= Screen.height/2 &&
                    t2.position.y >= Screen.height/2 && 
                    t3.position.y >= Screen.height/2 ){
                    firstDown = false;
                    firstUp = true;
                }

                // PLAYER ONE | DOWN
                else if( t1.position.y < Screen.height/2 &&
                    t2.position.y < Screen.height/2 && 
                    t3.position.y < Screen.height/2 ){
                    firstUp = false;
                    firstDown = true;
                }

            } 

            if(i == 3){
                t4 = t1;
            }

            if(i == 4){
                t5 = t1;
            }

            if(i == 5){ 
                t6 = t1;

                // PLAYER TWO | UP
                if( t4.position.y >= Screen.height/2 &&
                   t5.position.y >= Screen.height/2 && 
                   t6.position.y >= Screen.height/2 ){
                   secondDown = false;
                   secondUp = true;
                }

                // PLAYER ONE | DOWN
                else if( t4.position.y < Screen.height/2 &&
                    t5.position.y < Screen.height/2 && 
                    t6.position.y < Screen.height/2 ){
                    secondUp = false;
                    secondDown = true;
                }
            } 
            ++i;
	}
}

public void ShowInputMenuFeedback(){

    if(firstDown || secondDown) {
        inputFeedbackDown.SetActive(true);
    } else if(firstDown == false && secondDown == false){
        inputFeedbackDown.SetActive(false);
    }
    if(firstUp || secondUp) {
        inputFeedbackUp.SetActive(true);
    } else if(firstUp == false && secondUp == false){
        inputFeedbackUp.SetActive(false);
    }
}
}
