using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject target;
    public float dist = 10.0f;
    public float height = 0.0f;
    public float smoothRotate = 5.0f;
    public float mouseSensitivity = 400f; //마우스감도

    private Transform tr;
    private float MouseY;
    private float MouseX;

    public float CameraSpeed = 10.0f;

    private void Start()
    {
        tr = GetComponent<Transform>();
    }
    void Update()
    {
        Rotate();
    }

    private void LateUpdate()
    {
        float curYAngle = Mathf.LerpAngle(tr.eulerAngles.y, target.transform.eulerAngles.y, smoothRotate * Time.deltaTime);
        Quaternion rot = Quaternion.Euler(0, curYAngle, 0);
    }

    private void Rotate()
    {


    }
}
