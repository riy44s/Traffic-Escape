using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarWaypoint : MonoBehaviour
{
    [Header("Car AI")]
    public Car car;
    public Waypoints currentWaypoint;
    public Waypoints signWaypoint;

  //  public Button signButton;
    public bool isSignPressed;
    public GameObject signWaypointObject;
    public GameObject currentWaypointObject;

    public GameObject currentArrow;
    public GameObject ChangedArrow;

    public static CarWaypoint instance;

    public int count = 1;
    public TextMeshProUGUI CountText;

    public GameObject signText;

    private void Awake()
    {
        car = GetComponent<Car>();
        instance = this;
    }

    private void Start()
    {
        if (currentWaypoint != null)
        {
            car.LocateDestination(currentWaypoint.GetPosition());
        }
    }

    private void Update()
    {
        if (car.signButton)
        {
            
            if (isSignPressed)
            {
                if (signText != null)
                {
                    signText.SetActive(true);
                    Destroy(signText, 2f);
                }

                ChangeDirection();
            }

            else
            {
               
                if (car.destinationReached)
                {
                    currentWaypoint = currentWaypoint.nextWaypoint;
                    car.LocateDestination(currentWaypoint.GetPosition());
                }

            }
        }
    }

    public void ChangeDirection()
    {
    
        isSignPressed = true;

        if (isSignPressed && count > 0)
        {
            count--;
            CountText.text = count.ToString();
            signWaypointObject.SetActive(true);
            currentWaypointObject.SetActive(false);    

        }

        if (car.destinationReached)
        {
            currentArrow.SetActive(false);
            ChangedArrow.SetActive(true);
            signWaypoint = signWaypoint.nextWaypoint;
            car.LocateDestination(signWaypoint.GetPosition());
            car.signButton.interactable = false;
        }
        
    }
}
