using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public Rigidbody player = null;
    private bool isCooldownActive = false;



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy") && !isCooldownActive)
        {
            PlayerManager.Instance.takeDamage();
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        isCooldownActive = true;

        yield return new WaitForSeconds(0.75f);

        isCooldownActive = false;
    }

}
