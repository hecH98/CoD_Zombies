using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInicial : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject axe;
    GameObject monja;
    void Start()
    {
        
        StartCoroutine(lanzar());
        monja = GameObject.Find("ZombieMonja(Clone)");
        monja.GetComponent<zombieMonja1>().agent.speed = 1;
        monja.GetComponent<zombieMonja1>().vida = 1000;
        monja.GetComponent<zombieMonja1>().vidaMedia = monja.GetComponent<zombieMonja1>().vida * 0.5f;
        monja.GetComponent<zombieMonja1>().vidaCuarto = monja.GetComponent<zombieMonja1>().vida * 0.25f;
        axe = GameObject.Find("Axe_Small_scaled");
        axe.AddComponent<Hatchet>();
        
    }

    // Update is called once per frame
    void Update()
    {
        print("inciando el boss");

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
