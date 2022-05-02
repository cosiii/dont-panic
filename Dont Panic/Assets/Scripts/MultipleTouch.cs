using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class multipleTouch : MonoBehaviour {
    public GameObject circle, circle2;

    public bool obejectOneIsMoving, objectTwoIsMoving;

    public Touch t1, t2;

	// Update is called once per frame
	public void Update () {

        int i = 0;
         while(i < Input.touchCount){
            Touch t = Input.GetTouch(i);
            // PlAYER 1
            if(i == 0){
                t1 = t;
            if(t.phase == TouchPhase.Began){
                Debug.Log("touch began");
                obejectOneIsMoving = true;
                objectTwoIsMoving = false;
                Player1.Instance.highlight.SetActive(true);
                Player2.Instance.highlight.SetActive(false);

            } else if(t.phase == TouchPhase.Ended){
                Debug.Log("touch1 ended");
                obejectOneIsMoving = false;
                Player1.Instance.highlight.SetActive(false);
            }else if(t.phase == TouchPhase.Moved){
                Debug.Log("touch1 is moving");
            }
            }
            
            //Player 2
            if(i == 1){
                // Debug.Log("TOUCHES " + touches);
                // Debug.Log("TOUCH COUNT " + Input.touchCount);
            if(t.phase == TouchPhase.Began){
                Debug.Log("touch2 began");
                objectTwoIsMoving = true;
                obejectOneIsMoving = false;
                Player2.Instance.highlight.SetActive(true);
                Player1.Instance.highlight.SetActive(false);
            }else if(t.phase == TouchPhase.Ended){
                Debug.Log("touch2 ended");
                objectTwoIsMoving = false; 
                Player2.Instance.highlight.SetActive(false);
            }else if(t.phase == TouchPhase.Moved){
                Debug.Log("touch2 is moving");
                // second touch position
                Debug.Log(t.position);
                // first touch position
                Debug.Log(t1.position);
            }
            }

            if(i > 1){
                Debug.Log("more than 2 touches recognized");
            }
            ++i;
        }
	}
}