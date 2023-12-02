using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float speed = 0;
    private bool onGrounded = true;



    Rigidbody2D rb2d;

    private bool moveForward = false;
    private bool moveBack = false;
    public WheelJoint2D backWheel;
    public WheelJoint2D frontWheel;
    JointMotor2D motor;
    // Start is called before the first frame update
    void Start()
    {
        motor.maxMotorTorque = 10000;
        rb2d.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        OnGround();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveBack = false;
            moveForward = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow))

        {
            moveBack = true;
            moveForward = false;
        }
        else
        {
            moveBack = false;
            moveForward = false;
        }

        if (frontWheel.GetComponent<Collider2D>().IsTouchingLayers() ||
           backWheel.GetComponent<Collider2D>.IsTouchingLayers())
        {
            onGrounded = true;
        }
        else
        {
            onGrounded = false;
        }

    }

    private void OnGround()
    {
        if (moveForward)
        {
            if (frontWheel.attachedRigidbody.angularVelocity > -2000)
            {
                speed += 40;
                motoe.motorSpeed = speed;
            }
            frontWheel.motor = motor;
            backWheel.motor = motor;
            frontWheel.useMotor = true;
            backWheel.useMotor = true;
        }
        else if (moveBack)
        {
            if (frontWheel.attachedRigidbody.angularVelocity < 2000)
            {
                speed -= 40;
                motor.motorSpeed = speed;
            }
            frontWheel.motor = motor;
            backWheel.motor = motor;
            frontWheel.useMotor = true;
            backWheel.useMotor = true;
        }
        else
        {
            speed = -frontWheel.attachedRigidbody.angularVelocity;
            frontWheel.useMotor = false;
            backWheel.useMotor = false;
        }
    }

}
