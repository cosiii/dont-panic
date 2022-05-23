using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class Buttons : MonoBehaviour
{

  public static Buttons Instance;

   public TextMeshProUGUI nameText1, nameText2;
   public TextMeshProUGUI nameDisplay1, nameDisplay2;
   
     void Awake(){
      Instance = this;
     }

     public void Update(){
         nameDisplay1.text = nameText1.text;
         nameDisplay2.text = nameText2.text;
     }

     public void ButtonClick(){
         Debug.Log("hello");
     }
}
