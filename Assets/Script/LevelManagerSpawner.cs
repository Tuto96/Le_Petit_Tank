using UnityEngine;
using System.Collections;

public class LevelManagerSpawner : MonoBehaviour
{


    public int numberOfObstacles;
    public GameObject planet, obstaclePrefab;

    private float minimumObstacleSize = 2f;
    private float maximumObstacleSize = 20f;
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i<numberOfObstacles; i++)
        {
            Vector3 direction = Random.onUnitSphere;
            float distance = planet.transform.localScale.x / 2;
            GameObject obj = (GameObject)Instantiate(obstaclePrefab);            
            GameObject obj1 = (GameObject)Instantiate(obstaclePrefab);

            Vector3 scale = new Vector3(Random.Range(minimumObstacleSize, maximumObstacleSize), Random.Range(minimumObstacleSize, maximumObstacleSize), Random.Range(minimumObstacleSize, maximumObstacleSize));
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
