using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, 0, Random.Range(-45, 45));
    }
}
