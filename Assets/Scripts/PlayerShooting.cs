using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public GameObject prefab;
    public GameObject shootPoint;
    public Animator anim;
    public AudioSource aus;
    public AudioClip shoot;

    private void Start()
    {
        aus = GetComponent<AudioSource>();
    }

    public void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            anim.SetTrigger("Shoot");
            aus.PlayOneShot(shoot);
            GameObject clone = Instantiate(prefab);
            clone.transform.position = shootPoint.transform.position;
            clone.transform.rotation = shootPoint.transform.rotation;
        }
    }
}
