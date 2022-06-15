using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class multipleTouch : MonoBehaviour {
    public GameObject circle;
    
    public static multipleTouch Instance;

    public bool obejectOneIsMoving, objectTwoIsMoving;

     bool touch3ObjectRight, touch3ObjectLeft, touch3ObjectUp, touch3ObjectDown;

     public bool touch3ObjectRightOne, touch3ObjectLeftOne, touch3ObjectUpOne, touch3ObjectDownOne;
    
     public bool touch3ObjectRightTwo, touch3ObjectLeftTwo, touch3ObjectUpTwo, touch3ObjectDownTwo;


    public Touch t, t1, t2, farestT;

    public Vector2 c, c2;

    public void Awake(){
        Instance = this;
    }

    public void Update(){
        UpdateToken();
    }
	// Update is called once per frame
	public void UpdateToken () {
        UnitManager.Instance.UpdatePlayerOne();
        UnitManager.Instance.UpdatePlayerTwo();


        // wenn ich drufklick macht er das und wenn nicht, dann blinken
        int i = 0;
         while(i < Input.touchCount){
            t = Input.GetTouch(i);
            if(i == 0){
                t1 = t;
            }
            
            if(i == 1){
               t2 = t;
            }

        float dist = Vector3.Distance(t.position, t1.position);
                
        float dist2 = Vector3.Distance(t1.position, t2.position);
                
        float dist3 = Vector3.Distance(t2.position, t.position);

    if (i == 1){
                //Debug.Log("2 touches");
            }
        
        // three touch points
        if(i == 2){
            if(t.phase == TouchPhase.Began){
                    UpdatePosition(dist, dist2, dist3);
                

            }else if(t.phase == TouchPhase.Ended){
                

            }else if(t.phase == TouchPhase.Moved){
            }

            }

            if (i == 3){
              //  Debug.Log("4 touches");
            }

            ++i;
        }
        
       
      
	}

    // GET TOUCH POSITION IN WORLD SPACE
    Vector2 getTouchPosition(Vector2 touchPosition){
        return GetComponent<Camera>().ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z));
    }

    public void UpdatePosition(float D1, float D2, float D3){
                // second touch position
                // Debug.Log(t.position + " " + t1.position + " " + t2.position);
                
                // shortest dist
                float shortestDist = Mathf.Min(Mathf.Min(D1, D2), D3);
                // Debug.Log(shortestDist);
                float xAngle = 6;
                if(D1 == shortestDist){
                  //Debug.Log("dist is shortest");
                  c= t1.position + (t.position - t1.position) / 2;
                   xAngle = (t1.position.x- t.position.x/t1.position.y- t.position.y) ;
                   farestT = t2;
              } else if(D2 == shortestDist){
                  //Debug.Log("dist2 is shortest");
                  c= t2.position + (t1.position - t2.position) / 2;
                   xAngle = (t2.position.x- t1.position.x/t2.position.y- t1.position.y) ;
                  farestT = t;
              } else if(D3 == shortestDist){
                  //Debug.Log("dist3 is shortest");
                  c= t.position + (t2.position - t.position) / 2;
                   xAngle = (t.position.x- t2.position.x/t.position.y- t2.position.y) ;
                   farestT = t1;
              }

              //Debug.Log("c: " + c + "farestT: " + farestT.position.x + " / " + farestT.position.y);

              // ROTATE ALL TO X INSTEAD OD +

                if(c.x >= farestT.position.x && c.y <= farestT.position.y ){
                    touch3ObjectRight = true;
                    touch3ObjectLeft = false;
                    touch3ObjectUp = false;
                    touch3ObjectDown = false;
                } 
                if(c.x <= farestT.position.x && c.y >= farestT.position.y ){
                    touch3ObjectLeft = true;
                    touch3ObjectRight = false;
                    touch3ObjectUp = false;
                    touch3ObjectDown = false;
                } 
                
                if(c.x <= farestT.position.x && c.y <= farestT.position.y ){
                    touch3ObjectUp = false;
                    touch3ObjectDown = true;
                    touch3ObjectLeft = false;
                    touch3ObjectRight = false;
                } 

                if(c.x >= farestT.position.x && c.y >= farestT.position.y ){
                    touch3ObjectUp = true;
                    touch3ObjectDown = false;
                    touch3ObjectLeft = false;
                    touch3ObjectRight = false;
                } 

                 if (GameManager.Instance.GameState == GameState.Player1Turn ){ // && Player1.Instance.deciding == true
            touch3ObjectRightOne = touch3ObjectRight;
            touch3ObjectLeftOne = touch3ObjectLeft; 
            touch3ObjectUpOne = touch3ObjectUp;
            touch3ObjectDownOne = touch3ObjectDown;
            
    Tile.Instance.ShowWalkableTiles(Player1.Instance);
        } else if (GameManager.Instance.GameState == GameState.Player2Turn ){ // && Player2.Instance.deciding == true
            
            touch3ObjectRightTwo = touch3ObjectRight;
            touch3ObjectLeftTwo = touch3ObjectLeft; 
            touch3ObjectUpTwo = touch3ObjectUp;
            touch3ObjectDownTwo = touch3ObjectDown;

            
    Tile.Instance.ShowWalkableTiles(Player2.Instance);
        }  
    }
}