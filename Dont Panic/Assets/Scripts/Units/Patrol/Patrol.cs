using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : BasePatrol
{
  public static Patrol Instance;
 
    public float speed;
    private float waitTime;
    public float startWaitTime;

    public bool collided = false;

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
      Tile.Instance.ThrowPlayer(1);
      if(Player1.Instance.deciding){
      Tile.Instance.ShowWalkableTiles(Player1.Instance);
      }
    } 
    if (col.gameObject.name == "playertwo(Clone)"){
      Tile.Instance.ThrowPlayer(2);
      if(Player2.Instance.deciding){
      Tile.Instance.ShowWalkableTiles(Player2.Instance);
      }
    }
     
  }

    void Update(){
      // transform.position is recent position
      transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
      if(Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f){
        if(waitTime <= 0){
          // ist der spot der auf der gleichen Achse liegt
          randomSpot = Random.Range(0, moveSpots.Length);
          waitTime = startWaitTime;
        } else {
          waitTime -= Time.deltaTime;
        }
      }

      

     /* if (multipleTouch.onTablet == true){
        speed = 0;
      } else if (multipleTouch.onTablet == false){
        speed = 1;   // am besten das was am anfang wäre
      } */

    }
}
