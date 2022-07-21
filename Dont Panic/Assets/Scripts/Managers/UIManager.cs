
using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{	
	
public static UIManager Instance;
 public Text TimerText; 
 public bool playing;
 private float Timer;
 [SerializeField] public TextWriter textWriter;
 public Text TextToWrite, BeginnerText1, BeginnerText2, EndText;
 public GameObject pauseMenu, HelpMenu, HelpFeatures, HelpFeaturesRotate, HelpItems, HelpItemsRotate, HelpGuard, HelpGuardRotate, BeginnerTextModal;

 public List <GameObject> HelpFeaturesRotateThemself = new List<GameObject>();
 public List <GameObject> HelpItemsRotateThemself = new List<GameObject>();
 public List <GameObject> HelpGuardRotateHimself = new List<GameObject>();
 public Button PauseButton;
 public bool pauseMenuOpen;

 public Sprite pause, starting;

public bool TextIsPlaying;
 public string messageText;



public void Awake(){
	Instance = this;
	if( TextIsPlaying == true){
	messageText = BeginnerText1.text;
	textWriter.AddWriter(BeginnerText1.GetComponent<Text>(), messageText , 0.07f, true);
	textWriter.AddWriter(BeginnerText2.GetComponent<Text>(), messageText , 0.07f, true);
	}

}

void Start(){
}
public void SpeedSoundUp(string sound)
{
		Sound s = Array.Find(AudioManager.Instance.sounds, item => item.name == sound);
}

 void Update () {
	

// NORMAL TIMER
 	if(playing == true){
  
	  Timer += Time.deltaTime;
	  int minutes = Mathf.FloorToInt(Timer / 60F);
	  int seconds = Mathf.FloorToInt(Timer % 60F);
	  int milliseconds = Mathf.FloorToInt((Timer * 100F) % 100F);
	  TimerText.text = minutes.ToString ("00") + ":" + seconds.ToString ("00");
	}

  }

  public void OneBack(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
		AudioManager.Instance.Play("click");
        Debug.Log("oneback");
    }

  public void TwoBack(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
		AudioManager.Instance.Play("click");
		Time.timeScale = 1.0f;
        Debug.Log("twoback");
    }

	public void CloseHelpMenu(){
		HelpMenu.SetActive(false);
		AudioManager.Instance.Play("click");
    }	
	public void CloseFeatures(){
		HelpFeatures.SetActive(false);
		AudioManager.Instance.Play("click");
    }

	public void OpenFeatures(){
		HelpFeatures.SetActive(true);
		AudioManager.Instance.Play("click");
    }

	public void OpenHelpMenu(){
		HelpMenu.SetActive(true);
		AudioManager.Instance.Play("click");
    }

	public void CloseBeginnerText(){
	BeginnerTextModal.SetActive(false);
	AudioManager.Instance.Play("click");
	TextIsPlaying = false;
	}
	

	public void RotateFeatures(){
		foreach (GameObject x in HelpFeaturesRotateThemself){
			x.transform.Rotate(0, 0, 180);
		}

		HelpFeaturesRotate.transform.Rotate(0,0,180);
		AudioManager.Instance.Play("click");

    }

	public void CloseItems(){
		HelpItems.SetActive(false);
		AudioManager.Instance.Play("click");
    }

	public void OpenItems(){
		HelpItems.SetActive(true);
		AudioManager.Instance.Play("click");
    }
	public void RotateItems(){
		foreach (GameObject x in HelpItemsRotateThemself){
			x.transform.Rotate(0, 0, 180);
		}
		HelpItemsRotate.transform.Rotate(0,0,180);
		AudioManager.Instance.Play("click");
    }

	public void CloseGuard(){
		HelpGuard.SetActive(false);
		AudioManager.Instance.Play("click");
    }

	public void OpenGuard(){
		HelpGuard.SetActive(true);
		AudioManager.Instance.Play("click");
    }

	public void RotateGuard(){
		foreach (GameObject x in HelpGuardRotateHimself){
			x.transform.Rotate(0, 0, 180);
		}
		HelpGuardRotate.transform.Rotate(0,0,180);
		AudioManager.Instance.Play("click");
    }

	public void PauseMenu(){
		if(pauseMenuOpen == true){
		pauseMenu.SetActive(false);
        Time.timeScale = 1;
		PauseButton.image.sprite = pause;
		}
		
		if(pauseMenuOpen == false){
		pauseMenu.SetActive(true);
		Time.timeScale = 0;
		PauseButton.image.sprite = starting;
		}

		pauseMenuOpen = !pauseMenuOpen;
		AudioManager.Instance.Play("click");
    }	

}