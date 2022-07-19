using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextWriter : MonoBehaviour
{
private List<TextWriterSingle> textWriterSingleList;


public void Awake(){
	textWriterSingleList = new List <TextWriterSingle>();
}

public void AddWriter(Text uiText, string TextBlockToWrite, float timePerChar, bool invisibleChar){
	Debug.Log("Added writer");
	textWriterSingleList.Add(new TextWriterSingle(uiText,  TextBlockToWrite,  timePerChar, invisibleChar));
}


private void Update(){
	for (int i = 0; i < textWriterSingleList.Count; i++){
		textWriterSingleList[i].Update();
	}
}


public class TextWriterSingle {
    private Text uiText; // evtl später public und dann jeweiliges einfügen
    private string TextBlockToWrite;
    private float timePerChar;
    private float timer;
	private int charIndex;
	private bool invisibleChar;

	
	public TextWriterSingle (Text uiText, string TextBlockToWrite, float timePerChar, bool invisibleChar){
		this.uiText = uiText;
        this.TextBlockToWrite = TextBlockToWrite;
        this.timePerChar = timePerChar;
		this.invisibleChar = invisibleChar;
	}


	public void Update(){

			if(uiText != null){
				timer -= Time.deltaTime;

				while(timer <= 0f){
				// DISPLAY NEXT CHARACTER
				timer +=  timePerChar;
				charIndex++;
				/*if (charIndex <= TextBlockToWrite.Length){
					uiText.text = TextBlockToWrite.Substring(0, charIndex);
				} */
				string text = TextBlockToWrite.Substring(0, charIndex);
				if(invisibleChar){
				// text += "<<color=#00000000>" + TextBlockToWrite.Substring(charIndex) + "</color>>"; // <<#FFFF00>style="H1"</color>>
				}
				
				uiText.text = text;
			}

			if(charIndex >= TextBlockToWrite.Length){
					// ENTIRE STRING DISPLAYED
					uiText = null;
					UIManager.Instance.TextIsPlaying = false;
					charIndex = 0;
					return;
					// funktion eigentlich aufhören
				}
			}
			

		}
	}
}
