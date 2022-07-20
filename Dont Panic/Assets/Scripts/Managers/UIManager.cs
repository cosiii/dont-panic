
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


 void Update () {

// NORMAL TIMER
 	if(playing == true){
  
	  Timer += Time.deltaTime;
	  int minutes = Mathf.FloorToInt(Timer / 60F);
	  int seconds = Mathf.FloorToInt(Timer % 60F);
	  int milliseconds = Mathf.FloorToInt((Timer * 100F) % 100F);
	  TimerText.text = minutes.ToString ("00") + ":" + seconds.ToString ("00") + ":" + milliseconds.ToString("00");
	}

  }

  public void OneBack(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Debug.Log("oneback");
    }

  public void TwoBack(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        Debug.Log("twoback");
    }

	public void CloseHelpMenu(){
		HelpMenu.SetActive(false);
    }	
	public void CloseFeatures(){
		HelpFeatures.SetActive(false);
    }

	public void OpenFeatures(){
		HelpFeatures.SetActive(true);
    }

	public void OpenHelpMenu(){
		HelpMenu.SetActive(true);
    }

	public void CloseBeginnerText(){
	BeginnerTextModal.SetActive(false);
	}
	

	public void RotateFeatures(){
		foreach (GameObject x in HelpFeaturesRotateThemself){
			x.transform.Rotate(0, 0, 180);
		}

		HelpFeaturesRotate.transform.Rotate(0,0,180);

    }

	public void CloseItems(){
		HelpItems.SetActive(false);
    }

	public void OpenItems(){
		HelpItems.SetActive(true);
    }
	public void RotateItems(){
		foreach (GameObject x in HelpItemsRotateThemself){
			x.transform.Rotate(0, 0, 180);
		}
		HelpItemsRotate.transform.Rotate(0,0,180);
    }

	public void CloseGuard(){
		HelpGuard.SetActive(false);
    }

	public void OpenGuard(){
		HelpGuard.SetActive(true);
    }

	public void RotateGuard(){
		foreach (GameObject x in HelpGuardRotateHimself){
			x.transform.Rotate(0, 0, 180);
		}
		HelpGuardRotate.transform.Rotate(0,0,180);
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
    }	

}