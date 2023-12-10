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
    public AudioClip reload;
    public int bulletsAmount;
    private bool isReload = false;
    private int count = 0;

    private void Start()
    {
        aus = GetComponent<AudioSource>();
    }

    public void OnFire(InputValue value)
    {
        if (value.isPressed && bulletsAmount > 0 && Time.timeScale > 0 && isReload == false)
        {
            bulletsAmount--;
            anim.SetTrigger("Shoot");
            aus.PlayOneShot(shoot);
            GameObject clone = Instantiate(prefab);
            clone.transform.position = shootPoint.transform.position;
            clone.transform.rotation = shootPoint.transform.rotation;
        }
        else if(value.isPressed && bulletsAmount == 0 && count == 0)
        {
            StartCoroutine("Reload");
        }
    }

    public IEnumerator Reload()
    {
        count++;
        anim.SetBool("Reloading", true);
        aus.PlayOneShot(reload);
        isReload = true;
        yield return new WaitForSeconds(1.5f);
        bulletsAmount += 12;
        anim.SetBool("Reloading", false);
        isReload = false;
        count--;
    }
}
