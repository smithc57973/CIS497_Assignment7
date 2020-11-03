/*
 * Chris Smith
 * Prototype 4
 * Script to control player
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    public float speed;
    private float forwardInput;
    private GameObject focalPoint;
    public bool hasPowerup;
    private float powerupStrength = 15f;
    public GameObject powerupIndicator;
    public SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        focalPoint = GameObject.FindGameObjectWithTag("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");

        //move indicator to ground below player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        //Player loses if they fall off
        if (transform.position.y < -10)
        {
            spawnManager.gameOver = true;
        }
    }

    private void FixedUpdate()
    {
        playerRB.AddForce(focalPoint.transform.forward * speed * forwardInput);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdown());
            powerupIndicator.gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            //Get enemey rigidbody
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
            //set vector3 with direction away from player
            Vector3 away = (collision.gameObject.transform.position - transform.position).normalized;
            //add force away from player
            enemyRB.AddForce(away * powerupStrength, ForceMode.Impulse);
        }
    }

    IEnumerator PowerupCountdown()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }
}
