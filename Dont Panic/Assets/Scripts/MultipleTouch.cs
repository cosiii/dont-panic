using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class multipleTouch : MonoBehaviour {
    public GameObject circle, circle2;

    public bool obejectOneIsMoving, objectTwoIsMoving;

    public Touch t1, t2;

    public GameObject touch3Object;

    public List<touchLocation> touches = new List<touchLocation>();

    public Vector2 c;

	// Update is called once per frame
	public void Update () {
    GameObject createCircle(Touch t){
        GameObject c = Instantiate(circle) as GameObject;
        c.name = "Touch" + t.fingerId;
        c.transform.position = getTouchPosition(t.position);
        return c;
    }

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
               t2 = t;
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
                // second touch position
                // Debug.Log(t.position);
                // first touch position
                // Debug.Log(t1.position);
            }else if(t.phase == TouchPhase.Moved){
                Debug.Log("touch2 is moving");
            }
            }


        // three touch points
        if(i == 2){
            if(t.phase == TouchPhase.Began){
                Debug.Log("touch2 began");
                objectTwoIsMoving = true;
                obejectOneIsMoving = false;
                Player2.Instance.highlight.SetActive(true);
                Player1.Instance.highlight.SetActive(false);

                //touches.Add(new touchLocation(t.fingerId, createCircle(t)));
                //Instantiate(touch3Object, touch3Object.transform.position, Quaternion.identity).transform.Rotate(5, 5, 90, Space.Self);

            }else if(t.phase == TouchPhase.Ended){
                Debug.Log("touch2 ended");
                objectTwoIsMoving = false; 
                Player2.Instance.highlight.SetActive(false);
                
              

            }else if(t.phase == TouchPhase.Moved){

                // second touch position
                // Debug.Log(t.position + " " + t1.position + " " + t2.position);
                
                float dist = Vector3.Distance(t.position, t1.position);
                
                float dist2 = Vector3.Distance(t1.position, t2.position);
                
                float dist3 = Vector3.Distance(t2.position, t.position);

                

                // Debug.Log(dist + " " + dist2 + " " + dist3);

                // shortest dist
                float shortestDist = Mathf.Min(Mathf.Min(dist, dist2), dist3);
                Debug.Log(shortestDist);
                float xAngle = 6;
                float yAngle = 2;
                if(dist == shortestDist){
                  Debug.Log("dist is shortest");
                  c= t1.position + (t.position - t1.position) / 2;
                   xAngle = 4;
                   yAngle = 2;
              } else if(dist2 == shortestDist){
                  Debug.Log("dist2 is shortest");
                  c= t2.position + (t1.position - t2.position) / 2;
                   xAngle = 7;
                   yAngle = 1;
              } else if(dist3 == shortestDist){
                  Debug.Log("dist3 is shortest");
                  c= t.position + (t2.position - t.position) / 2;
                   xAngle = 5;
                   yAngle = 8;
              }

              

                touch3Object.transform.position = getTouchPosition(c);
                Instantiate(touch3Object, touch3Object.transform.position, Quaternion.identity).transform.Rotate(xAngle, yAngle, yAngle, Space.World);
                //touch3Object.transform.Rotate(5, 5, 90, Space.Self);
                // Debug.Log(xAngle/yAngle);
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