using UnityEngine;
using System.Collections;

public class GalaxyMacmanController : MonoBehaviour
{
#region Private Variables
    private Rigidbody rigidBody;
    private AudioSource death;
    private float shootTimer, damageTimer,boostTimer, shootStrength, boost;
    private bool canShoot, canTakeDamage, canBoost;
    private Vector3 lastPosition;
#endregion
#region Public Variables
    /// <summary>
    /// The player number for the controller input.
    /// </summary>
    public int playerNumber;
    /// <summary>
    /// The movement speed of the tank.
    /// </summary>
    public float speed;
    /// <summary>
    /// The gravity force applied by the planet.
    /// </summary>
    public float gravityMagnitude;
    /// <summary>
    /// The rotation smoothness for the upwards rotation setting.
    /// </summary>
    public float rotationSmoothness;
    /// <summary>
    /// The rotation speed of the tank.
    /// </summary>
    public float rotationSpeed;
    /// <summary>
    /// The force applied to the new projectiles.
    /// </summary>
    public float shootForce;
    /// <summary>
    /// The cannon cooldown.
    /// </summary>
    public float cannonCooldown;
    /// <summary>
    /// The invulnerability cooldown.
    /// </summary>
    public float damageCooldown;
    public float boostCooldown;
    public float maxShootingForce, minimumShootingForce, boostForceMultiplier;
    public GameObject planet,
        cannon,
        bullet;
    public bool isDead;
    public ParticleSystem noozleParticle,boostParticle;

#endregion

    // Use this for initialization
    void Start()
    {
        isDead = false;
        rigidBody = GetComponent<Rigidbody>();
        death = GetComponent<AudioSource>();
        shootTimer = 0;
        shootStrength = minimumShootingForce;
        canShoot = false;
        canBoost = false;
        GameManager.playerscore [playerNumber - 1] = 0; // This is inverse, each player sets the other player score

        boostParticle.emissionRate = 0;
    }
	
    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            move();
            if (Input.GetButton("Fire" + playerNumber))
            {
                shootStrength += Time.deltaTime;
                if(shootStrength>=maxShootingForce){
                    shoot();
                }

            }
            if (Input.GetButtonUp("Fire" + playerNumber))
            {
                shoot();
            }
            refreshCooldown();
        }
    }
    void refreshCooldown()
    {
        // Cannon cooldown
        if (shootTimer < cannonCooldown)
        {
            shootTimer += Time.deltaTime;
        } else
        {
            canShoot = true;
        }

        // Invulnerability cooldown
        if (damageTimer < damageCooldown)
        {
            damageTimer += Time.deltaTime;
        } else
        {
            canTakeDamage = true;
        }


        if (boostTimer < boostCooldown)
        {
            boostTimer += Time.deltaTime;
        } else
        {
            canBoost = true;
        }
    }
    void shoot()
    {
        // Can the tank shoot?
        if (canShoot)
        {      
            // play the particle system
            noozleParticle.Play();

            // Create a new projectile
            GameObject currentBullet = (GameObject)Instantiate(bullet, cannon.transform.position, cannon.transform.rotation);

            // Add an impulse to the projetile
            currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.up * shootForce * shootStrength, ForceMode.Impulse);
            Debug.Log(shootForce*shootStrength);
            // Reset cannon cooldown
            canShoot = false;
            shootTimer = 0;
            shootStrength = minimumShootingForce;
        }
    }

    void move()
    {
        // Get each player axis
        float hor = Input.GetAxis("Horizontal" + playerNumber);
        float ver = Input.GetAxis("Vertical" + playerNumber) * speed;
        boost = Mathf.Lerp(boost, 1f, Time.deltaTime);
        if (canBoost && Input.GetButton("Boost" + playerNumber))
        {
            boost = boostForceMultiplier;
            boostTimer = 0;
            canBoost = false;
        }

            
        boostParticle.emissionRate = Mathf.RoundToInt( Vector3.Distance(transform.position,lastPosition) * 100);
        

        // Create a vector from the tank to the center of the planet, this is the gravity vector
        Vector3 gravityVector = -(transform.position - planet.transform.position).normalized;

        // Smoothly rotate the transform so the up vector is oposiste of the gravity vector
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, -gravityVector) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothness * Time.deltaTime);
  

        // Rotate the tank according to the player input
        transform.Rotate(transform.up, hor * rotationSpeed, Space.World);
        // Add the gravity force
        rigidBody.AddForce(gravityVector * gravityMagnitude, ForceMode.Impulse);

        // Add forward impulse from player input
        rigidBody.AddForce(transform.forward * ver * boost, ForceMode.Impulse);
        lastPosition = transform.position;
    }
    void OnTriggerEnter(Collider coll)
    {
        // Is invulnerable?
        if (canTakeDamage)
        {                    
            if (coll != null)
            {
                // is a explosion? (what else could it be?)
                if (coll.tag == "explosion")
                {                
                    //Debug.Log("BOOOOOM biatch " + playerNumber);
                    // This is important for the game manager
                    isDead = true;

                    //Deathrattle
                    death.PlayOneShot(death.clip);

                    // Static variable!!!!!!!!!!!!!!!!!
                    GameManager.playerscore [playerNumber - 1]++;

                    // reset invulnerability cooldown
                    canTakeDamage = false;
                    damageTimer = 0;
                }
            }
        }
    }
}
