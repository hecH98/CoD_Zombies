using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss25 : MonoBehaviour
{
    GameObject monja;
    // Start is called before the first frame update
    void Start()
    {
        print("25%");
        StartCoroutine(lanzar());
        monja = GameObject.Find("ZombieMonja(Clone)");
        monja.GetComponent<zombieMonja1>().agent.speed = 15;
        monja.GetComponent<Renderer>().material = GameObject.Find("tunicaRoja").GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator lanzar()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            print("lanzando");
            if (Vector3.Distance(zombieMonja1.jugador.transform.position, transform.position) > 8)
            {
                Instantiate(zombieMonja1.hatchet, zombieMonja1.referencia.position, zombieMonja1.referencia.transform.rotation);
            }

        }
    }

}
