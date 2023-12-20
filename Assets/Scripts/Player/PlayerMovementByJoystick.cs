using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementByJoystick : MonoBehaviour
{
    public Joystick joystick = null;
    public float speed = 10.0f;
    public Rigidbody body = null;


    void Update()
    {
        Vector2 inputMovement = joystick.Direction;

        body.velocity = new Vector3(
            inputMovement.x * speed,
            body.velocity.y,
            inputMovement.y * speed);
        if (inputMovement == Vector2.zero)
        {
            return;
        }
        body.rotation = Quaternion.LookRotation(body.velocity);
    }
}
