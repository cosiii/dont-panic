
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{	
	
public static UIManager Instance;
 public Text TimerText; 
 public bool playing;
 private float Timer;
 private bool addedHoleTile = false;
 [SerializeField] public TextWriter textWriter;
 public Text TextToWrite;

public bool TextIsPlaying;
 public string messageText;


public void Awake(){
	Instance = this;

}

void Start(){
	//messageText.text = "Haaaaalllo";
	//textWriter.AddWriter(textWriterText.GetComponent<Text>(), "haaaallloooooo", 1f);
}


 void Update () {
	if( TextIsPlaying == true){
	textWriter.AddWriter(TextToWrite.GetComponent<Text>(), messageText , 0.1f);
	}

// NORMAL TIMER
 	if(playing == true){
  
	  Timer += Time.deltaTime;
	  int minutes = Mathf.FloorToInt(Timer / 60F);
	  int seconds = Mathf.FloorToInt(Timer % 60F);
	  int milliseconds = Mathf.FloorToInt((Timer * 100F) % 100F);
	  TimerText.text = minutes.ToString ("00") + ":" + seconds.ToString ("00") + ":" + milliseconds.ToString("00");


	  if(minutes > 0 && seconds == 00){
		 if (addedHoleTile == false){
			 //GridManager.Instance.AddHoleTile();
			 addedHoleTile = true;
		 }
	  }

	  if(minutes > 0 && seconds == 59){
		addedHoleTile = false;
	  }
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
	// reload scene

	

}