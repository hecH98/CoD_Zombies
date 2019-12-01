using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss50 : MonoBehaviour
{
    GameObject monja;
    // Start is called before the first frame update
    void Start()
    {
        print("50%");
        monja = GameObject.Find("ZombieMonja(Clone)");
        monja.GetComponent<zombieMonja1>().agent.speed = 15;
        monja.GetComponent<Renderer>().material = GameObject.Find("tunicaNegra").GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
