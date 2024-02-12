using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementByJoystick : MonoBehaviour
{
    public Joystick joystick = null;
    public Rigidbody body = null;
    
    void Update()
    {
        Vector2 inputMovement = joystick.Direction;

        body.velocity = new Vector3(
            inputMovement.x * PlayerManager.Instance.speed,
            body.velocity.y,
            inputMovement.y * PlayerManager.Instance.speed);
        if (inputMovement == Vector2.zero)
        {
            PlayerManager.Instance.animator.SetBool("IsWalking", false);
            return;
        }
        PlayerManager.Instance.animator.SetBool("IsWalking", true);
        body.rotation = Quaternion.LookRotation(body.velocity);
    }
}
