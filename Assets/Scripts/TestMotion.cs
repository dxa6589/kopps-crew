using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestMotion : MonoBehaviour
{
    public float accelerationRate, deccelerationRate, maxSpeed;
    public bool kbMotion, msMotion;
    public Vector3 acceleration, velocity, position, mousePos;
    public Rigidbody RB;
    // Start is called before the first frame update
    void Start()
    {
        accelerationRate = 0.1f;
        deccelerationRate = 0.1f;
        maxSpeed = 1f;

        acceleration = Vector3.zero;
        velocity = Vector3.zero;
        position = transform.position;

        //RB = this.GetComponent<Rigidbody>();
        //mousePos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        mousePos = Mouse.current.position.ReadValue();
    }

    // Update is called once per frame
    void Update()
    {
        if (kbMotion)
        {
            //CalculatePosition();
            // move the vehicle to it's new position
            //transform.position = position;
            transform.Translate(position*maxSpeed*Time.deltaTime);
        }
        if (msMotion)
        {
            FollowMouse();
        }
    }
    void FixedUpdate()
    {
        if (msMotion) RB.MovePosition(position);
    }

    /*
    void CalculatePosition()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            acceleration = transform.forward * accelerationRate;
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
            velocity += acceleration;

            Debug.Log("up arrow is down");
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            acceleration = -transform.forward * accelerationRate;
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
            velocity += acceleration;

            Debug.Log("down arrow is down");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            acceleration = -transform.right * accelerationRate;
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
            velocity += acceleration;

            Debug.Log("left arrow is down");
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            acceleration = transform.right * accelerationRate;
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
            velocity += acceleration;

            Debug.Log("right arrow is down");
        }

        if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            // exponentially reduce the velocity 
            velocity *= deccelerationRate;
        }

        // add velocity to the vehicle
        position += velocity;
    }
    */

    void OnMove(InputValue value)
    { 
        Vector2 val = value.Get<Vector2>();
        position.x = -val.x;
        position.z = -val.y;
        position.y = 0;
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    acceleration = transform.forward * accelerationRate;
        //    velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        //    velocity += acceleration;

        //    Debug.Log("up arrow is down");
        //}
        //else if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    acceleration = -transform.forward * accelerationRate;
        //    velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        //    velocity += acceleration;

        //    Debug.Log("down arrow is down");
        //}
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    acceleration = -transform.right * accelerationRate;
        //    velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        //    velocity += acceleration;

        //    Debug.Log("left arrow is down");
        //}
        //else if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    acceleration = transform.right * accelerationRate;
        //    velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        //    velocity += acceleration;

        //    Debug.Log("right arrow is down");
        //}

        //if (!Input.GetKey(KeyCode.LeftArrow)&&!Input.GetKey(KeyCode.RightArrow)&&!Input.GetKey(KeyCode.UpArrow)&&!Input.GetKey(KeyCode.DownArrow))
        //{
        //    // exponentially reduce the velocity 
        //    velocity *= deccelerationRate;
        //}

        //// add velocity to the vehicle
        //position += velocity;
    }

    void FollowMouse()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position = Vector2.Lerp(transform.position, mousePos, maxSpeed);
        position.z = transform.position.z;
    }
}
