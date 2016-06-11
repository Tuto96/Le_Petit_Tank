using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{


    public int numberOfObstacles;
    public GameObject planet, obstaclePrefab;
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i<numberOfObstacles; i++)
        {
            Vector3 direction = Random.onUnitSphere;
            float distance = planet.transform.localScale.x / 2;
            GameObject obj = (GameObject)Instantiate(obstaclePrefab);            
            GameObject obj1 = (GameObject)Instantiate(obstaclePrefab);

            Vector3 scale = new Vector3(Random.Range(2f, 5f), Random.Range(2f, 5f), Random.Range(2f, 5f));
            distance += scale.y / 2;

            obj.transform.position = direction * distance;
            obj.transform.up = direction;
            obj.transform.localScale = scale;
            obj1.transform.position = -direction * distance;
            obj1.transform.up = direction;
            obj1.transform.localScale = scale;
        }
    }
	
    // Update is called once per frame
    void Update()
    {
	
    }
}
