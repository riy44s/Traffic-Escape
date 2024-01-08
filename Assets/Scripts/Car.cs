using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    public static Car car;

    Rigidbody rb;

    Vector3 startingPosition;
    Quaternion startingRotation;

    [Header("Car Info")]
    public float movingSpeed;
    public float turningSpeed = 300f;
    public float stopSpeed = 1f;

    [Header("destination Var")]
    public Vector3 destination;
    public bool destinationReached;

    public bool isDriving = false;

    public Button signButton;

    public AudioClip clip;

    private void Awake()
    {
        car = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingPosition = transform.position;
        startingRotation = transform.rotation;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                AudioManager.Instance.sfxSource.PlayOneShot(clip);
                isDriving = true;
            }

        }
        if (isDriving == true)
        {
            HandleCarMoving();
        }

    }

    public void HandleCarMoving()
    {
        if (transform.position != destination)
        {
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;
            float destinationDistance = destinationDirection.magnitude;

            if (destinationDistance >= stopSpeed)
            {
                destinationReached = false;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turningSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * movingSpeed * Time.deltaTime);
            }
            else
            {
                destinationReached = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            ResetCarPosition();
            Health.instance.TakeDamage(10);
            AudioManager.Instance.PlaySFX("Accident");
        }
        else if (collision.gameObject.CompareTag("AI"))
        {
            Health.instance.TakeDamage(20);
            ResetCarPosition();
            AudioManager.Instance.PlaySFX("Accident");
        }
    }

    public void ResetCarPosition()
    {
        transform.position = startingPosition;
        transform.rotation = startingRotation;
        rb.velocity = Vector3.zero;
        isDriving = false;
        LocateDestination(CarWaypoint.instance.currentWaypoint.GetPosition());
    }

    public void LocateDestination(Vector3 destination)
    {
        this.destination = destination;
        destinationReached = false;
    }

}
