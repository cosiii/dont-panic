using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadInput : MonoBehaviour
{
    TouchScreenKeyboard keyboard;
  string Pseudo;

    public void OpenKeyboard(){
        keyboard = TouchScreenKeyboard.Open ("", TouchScreenKeyboardType.Default);
    }

    /*void Update(){
        if(TouchScreenKeyboard.visible == false && keyboard != null){
            if(keyboard.status == TouchScreenKeyboard.Status.Done){
                Pseudo = keyboard.text;
                txt.text = "hallo" + Pseudo;
                keyboard = null;
            }
        }
    } */
}
