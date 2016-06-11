using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player1, player2, spawn1, spawn2;
    public AudioSource epicSoundtrack;
    public int scoreLimit;

    public static int[] playerscore = new int[2];
    public static int winner = -1;

    private GalaxyMacmanController player1controller, player2controller;
    private GameObject obj1, obj2;
    private Text score1, score2;   
    // Use this for initialization
    void Start()
    {
        // Create player one
        epicSoundtrack.PlayOneShot(epicSoundtrack.clip);
        obj1 = (GameObject)Instantiate(player1, spawn1.transform.position, spawn1.transform.rotation);

        // Create player two
        obj2 = (GameObject)Instantiate(player2, spawn2.transform.position, spawn2.transform.rotation);

        // Create player controllers
        player1controller = obj1.GetComponent<GalaxyMacmanController>();
        player2controller = obj2.GetComponent<GalaxyMacmanController>();

        // Get the text area of each player canvas renderer
        score1 = obj1.GetComponentInChildren<Text>();
        score2 = obj2.GetComponentInChildren<Text>();
    }
	
    // Update is called once per frame
    void Update()
    {
        if (epicSoundtrack.isPlaying && playerscore [0] < scoreLimit && playerscore [1] < scoreLimit)
        {
            // If the player one died
            if (player1controller.isDead)
            {
                obj1.transform.position = spawn1.transform.position;
                obj1.transform.rotation = spawn1.transform.rotation;
                player1controller.isDead = false;
            }

            // If the player two died
            if (player2controller.isDead)
            {
                obj2.transform.position = spawn2.transform.position;
                obj2.transform.rotation = spawn2.transform.rotation;
                player2controller.isDead = false;
            }
            score1.text = "Score: " + playerscore [1]; // Scores are set by the opposite player       
            score2.text = "Score: " + playerscore [0]; // Scores are set by the opposite player


        } else
        {
            player1controller.isDead = true;
            player2controller.isDead = true;
            if (playerscore [1] > playerscore [0])//player1 wins
            {
                winner = 0;
            } else if (playerscore [0] > playerscore [1])//player 2 wins
            {
                winner = 1;
            } else//tie
            {
                winner = -1;
            }
            Application.LoadLevel(2);
        }
    }
}
