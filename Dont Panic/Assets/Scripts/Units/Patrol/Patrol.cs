using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : BasePatrol
{
  public static Patrol Instance;
 
    public float speed;
    private float waitTime;
    public float startWaitTime;

    public BoxCollider patrolBoxCollider;
    //public MultipleTouch multipleTouch;

    public Transform[] moveSpots;
    private int randomSpot;
 void Awake(){
       Instance = this;
   }
    void Start(){
      waitTime = startWaitTime;
      randomSpot = Random.Range(0, moveSpots.Length);
    }

  public void OnMouseDown(){
    Debug.Log("clicked on patrol");
  }

   public void OnCollisionEnter(Collision col){
    if (col.gameObject.name == "playerone(Clone)"){
      Tile.Instance.ThrowPlayer1ByPatrol();
                
    } 
    if (col.gameObject.name == "playertwo(Clone)"){
      Tile.Instance.ThrowPlayer2ByPatrol();
    }
  }

    void Update(){
      // transform.position is recent position
      transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
      if(Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f){
        if(waitTime <= 0){
          randomSpot = Random.Range(0, moveSpots.Length);
          waitTime = startWaitTime;
        } else {
          waitTime -= Time.deltaTime;
        }
      }


      //Debug.Log(Input.mousePosition);

     /* if (multipleTouch.onTablet == true){
        speed = 0;
      } else if (multipleTouch.onTablet == false){
        speed = 1;   // am besten das was am anfang wäre
      } */

    }
}
