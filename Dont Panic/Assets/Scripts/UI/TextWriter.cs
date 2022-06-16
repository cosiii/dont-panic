using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
    private Text uiText; // evtl später public und dann jeweiliges einfügen
    private string TextBlockToWrite;
    private float timePerChar;
    private float timer;
	private int charIndex;

public void AddWriter(Text uiText, string TextBlockToWrite, float timePerChar){
		this.uiText = uiText;
        this.TextBlockToWrite = TextBlockToWrite;
        this.timePerChar = timePerChar;
		 
		 //charIndex = 0;
	}


public void Update(){
         if(uiText != null){
			timer -= Time.deltaTime;
		 }
		 while(timer <= 0f){
			// DISPLAY NEXT CHARACTER
			timer +=  timePerChar;
			charIndex++;
			if (charIndex <= TextBlockToWrite.Length){
				uiText.text = TextBlockToWrite.Substring(0, charIndex);
			}
		 }

		 if( charIndex >= TextBlockToWrite.Length){
				// ENTIRE STRING DISPLAYED
				Debug.Log("out of range");
				uiText = null;
				return;
			}

    }
}
