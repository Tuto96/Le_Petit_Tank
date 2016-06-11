using UnityEngine;
using System.Collections;

public class PillScript : MonoBehaviour
{
    // Use this for initialization
    public GameObject explosion;

    void OnCollisionEnter(Collision coll)
    {
        if (coll != null)
        {
            if (coll.gameObject.tag == "planet")
            {
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
