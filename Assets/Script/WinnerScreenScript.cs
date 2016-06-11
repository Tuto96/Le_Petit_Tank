using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinnerScreenScript : MonoBehaviour
{

    // Use this for initialization
    public GameObject body1, body2;
    private GameObject[] obj;
    private int duration; 
    private float timer;
    public float rot, offset;
    public Text winnerText;
    void Start()
    {
        timer = 0;
        duration = 5;
        if (GameManager.winner == 0)// player 1 wins
        {
            obj = new GameObject[1];
            obj [0] = (GameObject)Instantiate(body1);
            winnerText.text = "Player 1 wins";
        } else if (GameManager.winner == 1)// player 2 wins
        {
            obj = new GameObject[1];
            obj [0] = (GameObject)Instantiate(body2);
            winnerText.text = "Player 2 wins";
        } else// tie
        {
            obj = new GameObject[2];
            obj [0] = (GameObject)Instantiate(body1);
            obj [0].transform.localPosition = new Vector3(-offset, 0f, 0f);
            obj [1] = (GameObject)Instantiate(body2);
            obj [1].transform.localPosition = new Vector3(offset, 0f, 0f);
            winnerText.text = "It's a tie!";
        }
    }
	
    // Update is called once per frame
    void Update()
    {
        if (timer < duration)
        {        
            for (int i = 0; i< obj.Length; i++)
            {
                obj [i].transform.Rotate(rot, rot, rot);
            }
        } else
        {
            Application.LoadLevel(0);
        }
        timer += Time.deltaTime;
    }
}
