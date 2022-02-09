using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTouch : MonoBehaviour
{
    public GameObject circle;
    public GameObject circle2;
    public List<TouchLocation> touches = new List<TouchLocation>();
    public bool onTablet = false;

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        while(i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);
            // player 1
            if(t.fingerId == 1)
            {
              if(t.phase == TouchPhase.Began)
              {
                  Debug.Log("touch began");
                  touches.Add(new TouchLocation(t.fingerId, createCircle(t)));
              } else if (t.phase == TouchPhase.Ended)
              {
                  Debug.Log(" touch ended" + t.fingerId);
                  TouchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchId == t.fingerId);
                  Destroy(thisTouch.circle);
                  touches.RemoveAt(touches.IndexOf(thisTouch));

                  // figure stands on tablet?
                  if( onTablet == true)
                  {
                    Debug.Log("on tablet");
                    onTablet = false;
                  } else if ( onTablet == false)
                  {
                    Debug.Log("off tablet");
                    onTablet = true;
                  }


              } else if(t.phase == TouchPhase.Moved)
              {
                  Debug.Log("touch is moving");
                  TouchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchId == t.fingerId);
                  thisTouch.circle.transform.position = getTouchPosition(t.position);
              }
            }

            else if(t.fingerId == 2){
              if(t.phase == TouchPhase.Began)
              {
                  Debug.Log("touch3 began");
                  touches.Add(new TouchLocation(t.fingerId, createCircle2(t)));
              } else if (t.phase == TouchPhase.Ended)
              {
                  Debug.Log(" touch3 ended" + t.fingerId);
                  TouchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchId == t.fingerId);
                  Destroy(thisTouch.circle);
                  touches.RemoveAt(touches.IndexOf(thisTouch));
              } else if(t.phase == TouchPhase.Moved)
              {
                  Debug.Log("touch3 is moving");
                  TouchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchId == t.fingerId);
                  thisTouch.circle.transform.position = getTouchPosition(t.position);
              }
            }

            ++i;

        }
    }

    Vector2 getTouchPosition(Vector2 touchPosition) // convert to worldspace
    {
        return GetComponent<Camera>().ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z));
    }


    GameObject createCircle(Touch t)
    {
        GameObject c = Instantiate(circle) as GameObject;
        c.name = "Touch" + t.fingerId;
        c.transform.position = getTouchPosition(t.position);
        return c;
    }

    GameObject createCircle2(Touch t)
    {
        GameObject c = Instantiate(circle2) as GameObject;
        c.name = "Touch" + t.fingerId;
        c.transform.position = getTouchPosition(t.position);
        return c;
    }
}
