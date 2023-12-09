using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private bool moveForward = false;
    private bool moveBack = false;
    private float speed = 0f;
    private bool onGrounded = true;

    Rigidbody2D rb2d;

    public WheelJoint2D frontWheel;
    public WheelJoint2D backWheel;
    JointMotor2D motor;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        motor.maxMotorTorque = 10000;
    }

    void FixedUpdate()
    {
        OnGround();
        if(!onGrounded)
        {
            MoveInAir();
        }
        CheckGameOver();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            moveBack = false;
            moveForward = true;
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            moveBack = true;
            moveForward = false;
        }
        else
        {
            moveBack = false;
            moveForward = false;
        }

        if(frontWheel.GetComponent<Collider2D>().IsTouchingLayers() ||
            backWheel.GetComponent<Collider2D>().IsTouchingLayers())
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
                motor.motorSpeed = speed;
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

    void MoveInAir()
    {
        backWheel.useMotor = false;
        frontWheel.useMotor = false;
        if(moveForward)
        {
            if(rb2d.angularVelocity < 200)
            {
                rb2d.AddTorque(10);
            }
        }
        else if (moveBack)
        {
            if (rb2d.angularVelocity > -200)
            {
                rb2d.AddTorque(-10);
            }
        }
    }
   
    void CheckGameOver()
    {
        Vector2 dir = transform.up;
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, dir, 0.7f);
        Debug.DrawRay(transform.position, dir * 0.7f, Color.red);
    }
}
