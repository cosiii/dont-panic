using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class multipleTouch : MonoBehaviour {
    public GameObject circle, circle2;
    
    public static multipleTouch Instance;

    public bool obejectOneIsMoving, objectTwoIsMoving;

    public bool touch3ObjectRight, touch3ObjectLeft, touch3ObjectUp, touch3ObjectDown;

    public Touch t1, t2, farestT;

    public GameObject touch3Object;

    public Vector2 c, c2;

    public void Awake(){
        Instance = this;
    }
	// Update is called once per frame
	public void Update () {
        UnitManager.Instance.UpdatePlayerOne();
        int i = 0;
         while(i < Input.touchCount){
            Touch t = Input.GetTouch(i);
            // PlAYER 1
            if(i == 0){
                t1 = t;
            }
            
            //Player 2
            if(i == 1){
               t2 = t;
            }


        // three touch points
        if(i == 2){

            float dist = Vector3.Distance(t.position, t1.position);
                
            float dist2 = Vector3.Distance(t1.position, t2.position);
                
            float dist3 = Vector3.Distance(t2.position, t.position);

            if(t.phase == TouchPhase.Began){

            }else if(t.phase == TouchPhase.Ended){

            }else if(t.phase == TouchPhase.Moved){
                // second touch position
                // Debug.Log(t.position + " " + t1.position + " " + t2.position);
                
                // shortest dist
                float shortestDist = Mathf.Min(Mathf.Min(dist, dist2), dist3);
                // Debug.Log(shortestDist);
                float xAngle = 6;
                if(dist == shortestDist){
                  //Debug.Log("dist is shortest");
                  c= t1.position + (t.position - t1.position) / 2;
                   xAngle = (t1.position.x- t.position.x/t1.position.y- t.position.y) ;
                   farestT = t2;
              } else if(dist2 == shortestDist){
                  //Debug.Log("dist2 is shortest");
                  c= t2.position + (t1.position - t2.position) / 2;
                   xAngle = (t2.position.x- t1.position.x/t2.position.y- t1.position.y) ;
                  farestT = t;
              } else if(dist3 == shortestDist){
                  //Debug.Log("dist3 is shortest");
                  c= t.position + (t2.position - t.position) / 2;
                   xAngle = (t.position.x- t2.position.x/t.position.y- t2.position.y) ;
                   farestT = t1;
              }

                if(c.x >= farestT.position.x){
                    touch3ObjectRight = true;
                    touch3ObjectLeft = false;
                    //oldImage.sprite = Item2;
                } 
                if(c.x <= farestT.position.x){
                    touch3ObjectLeft = true;
                    touch3ObjectRight = false;
                    
                } 
                
                if(c.y <= farestT.position.y){
                    touch3ObjectUp = false;
                    touch3ObjectDown = true;
                } 

                if(c.y >= farestT.position.y){
                    touch3ObjectUp = true;
                    touch3ObjectDown = false;
                } 
            }
            }

            ++i;
        }

      
	}

    // GET TOUCH POSITION IN WORLD SPACE
    Vector2 getTouchPosition(Vector2 touchPosition){
        return GetComponent<Camera>().ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z));
    }
}