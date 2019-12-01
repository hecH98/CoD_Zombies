using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float sensH = 1.5f, sensV = 1.5f;
    float yaw = 0.00572957795130823f;
    float pitch = 0.00572957795130823f;
    public GameObject cuerpo;

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        yaw += sensH * Input.GetAxis("Mouse X");
        pitch -= sensV * Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, -90.0f, 50.0f);
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        cuerpo.transform.eulerAngles = new Vector3(0.0f,yaw,0.0f);

    }
}
