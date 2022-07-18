using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 5.0f;
    private float powerOfStrenght = 15.0f;
    private GameObject focalPoint;
    public bool hasPowerUp = false;
    public GameObject poweUpIndicator;
   
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        poweUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f,0);
        //poweUpIndicator.transform.Rotate(0, 50.0f, 0, Space.Self);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            hasPowerUp = true;
            poweUpIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerUpCountdownRoutine());
        }
    }

    IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        poweUpIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy")&& hasPowerUp)
        {
            Rigidbody enemeRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemeRigidBody.AddForce(awayFromPlayer * powerOfStrenght, ForceMode.Impulse);
            Debug.Log("Collided with: " + collision.gameObject.name + "with power up set to " + hasPowerUp);
        }
    }
}
