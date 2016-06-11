using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour
{

    public void OnPlayButton()
    {
        Application.LoadLevel(1);
    }
    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnInstructions()
    {
        Application.LoadLevel(3);
    }
}
