using UnityEngine;
using System.Collections;

public class explosionScript : MonoBehaviour
{
    public float increment;
    public float limit;
    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, limit);
    }
	
    // Update is called once per frame
    void Update()
    {
        Vector3 newScale = Vector3.one * increment;
        transform.localScale = transform.localScale + newScale;

    }
}
