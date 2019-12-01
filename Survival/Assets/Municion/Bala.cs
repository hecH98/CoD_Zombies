using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bala : MonoBehaviour
{
    public int speed = 100;
    private Rigidbody rb;
    public Text HUBPuntaje;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * speed, ForceMode.Impulse);
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.transform.tag == "Enemigo")
        //{
        //    MainCharacter.puntaje += 10;
        //    print(MainCharacter.puntaje);
        //    
        //}
        Destroy(gameObject);
    }
}
