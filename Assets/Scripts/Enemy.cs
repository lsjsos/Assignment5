using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.AddEnemy(this);
    }

    private void OnDestroy()
    {
        GameManager.instance.RemoveEnemy(this);
    }
}
