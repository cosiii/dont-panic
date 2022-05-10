using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class multipleTouch : MonoBehaviour {
    public GameObject circle;
    
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
        UnitManager.Instance.UpdatePlayerTwo();
        int i = 0;
         while(i < Input.touchCount){
            Touch t = Input.GetTouch(i);
            if(i == 0){
                t1 = t;
            }
            
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

              Debug.Log("c: " + c + "farestT: " + farestT.position.x + " / " + farestT.position.y);

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
            }
            }

            if (i == 3){
                Debug.Log("4 touches");
            }

            ++i;
        }

      
	}

    // GET TOUCH POSITION IN WORLD SPACE
    Vector2 getTouchPosition(Vector2 touchPosition){
        return GetComponent<Camera>().ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z));
    }

    public void effect(){
        Instantiate(circle, new Vector3(2,3,1), Quaternion.identity );
        Debug.Log("hheeeee");
    }
}