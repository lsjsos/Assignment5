using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    Rigidbody rigid;
    float nextMoveX;
    float nextMoveZ;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        nextMoveX = Random.Range(-1f, 1f)/3;
        nextMoveZ = Random.Range(-1f, 1f)/3;
    }

    private void FixedUpdate()
    {
        nextMoveX = Random.Range(-1f, 1f)/3;
        nextMoveZ = Random.Range(-1f, 1f)/3;
    }

    private void Update()
    {
        rigid.transform.Translate(nextMoveX, 0, nextMoveZ);
    }
}
