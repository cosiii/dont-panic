using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTouch : MonoBehaviour
{
    public static MultipleTouch Instance;
    public GameObject circle;
    public GameObject circle2;
    public List<TouchLocation> touches = new List<TouchLocation>();
    public bool onTablet = false;

    public bool objectOneRecognized = false;
    public bool objectTwoRecognized = false;
    public int howManyTouchesOnBoard = 0;

    public void Awake(){
        Instance = this;
    }
    // Update is called once per frame
    public void Update()
    {

        int i = 0;
        while(i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);
            // später zwei weil sonst kann man ja auch touchen
          
           
              if(t.phase == TouchPhase.Began)
              {
                  Debug.Log("touch began");
              } else if (t.phase == TouchPhase.Ended)
              {
                  Debug.Log(" touch ended" + t.fingerId);
                
              } else if(t.phase == TouchPhase.Moved)
              {
                  Debug.Log("touch is moving");
              }
              Debug.Log(Input.touchCount);

            if (Input.touchCount == 1){
                howManyTouchesOnBoard = 1;
            }
              if( howManyTouchesOnBoard == 1){
                MultipleTouch.Instance.objectOneRecognized = true;
                MultipleTouch.Instance.objectTwoRecognized = false;
                
            } else{
                MultipleTouch.Instance.objectOneRecognized = false;
                MultipleTouch.Instance.objectTwoRecognized = true;
                
            } 
            ++i;
            
        }
    }
}
