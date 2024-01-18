using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScript : MonoBehaviour
{
    // destroy object 1 seconds after creation
    void Start()
    {
        Destroy(gameObject, 1);
    }
}
