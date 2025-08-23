using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioClip bomb;
    public GameObject ai;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.AddEnemy(this);
    }

    private void OnDestroy()
    {
        GameManager.instance.RemoveEnemy(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PT"))
        {
            ai.GetComponent<EnemyFSM>().isPhoton = true;
            Destroy(other.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ai.GetComponent<EnemyFSM>().isBomb = true;
            collision.gameObject.GetComponent<AudioSource>().PlayOneShot(bomb);
        }
    }
}
