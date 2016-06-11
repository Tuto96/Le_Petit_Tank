using UnityEngine;
using System.Collections;

public class GalaxyGravity : MonoBehaviour
{
    private Rigidbody rb;
    public float gravityMagnitude;
    public GameObject planet;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
	
    // Update is called once per frame
    void Update()
    {
        Vector3 gravityVector = -(transform.position - planet.transform.position).normalized;
        rb.AddForce(gravityVector * gravityMagnitude, ForceMode.Impulse);
    }
}
