using UnityEngine;
using System.Collections;

public class instructionsScript : MonoBehaviour
{
    private float timer = 0;
    // Use this for initialization
    void Start()
    {
	    
    }
	
    // Update is called once per frame
    void Update()
    {
        if (timer > 10)
        {
            Application.LoadLevel(0);
        }
        timer += Time.deltaTime;
    }
}
