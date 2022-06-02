
using UnityEngine;
using UnityEngine.UI;

public class InputField : MonoBehaviour
{

public InputField input;
private TouchScreenKeyboard keyboard;

 void Start () {
        if (input)
        {
            TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, true);
        }
    }
}