using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LifeRecover : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager.Instance.recoverLife(20);
            Destroy(gameObject);        
        }
    }

    private void Update()
    {
        // make the object rotate on itself
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
    }


}
