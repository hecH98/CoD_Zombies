using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieLanzador : MonoBehaviour
{
    public int vida = 100;
    //private Transform posicion;
    public int speed = 10;
    GameObject jugador;
    public GameObject hatchet;
    public NavMeshAgent agent;
    public Transform referencia;
    // Start is called before the first frame update 
    void Start()
    {
        jugador = GameObject.Find("Jugador");
        StartCoroutine(lanzar());
        //posicion = jugador.transform;
    }



    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(jugador.transform.position);
        /*if(transform.position != posicion.position)
        {
            //print("siguiendo al jugador");
            Vector3 pos = Vector3.MoveTowards(transform.position, posicion.position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }*/
        if (vida <= 0)
        {

            MainCharacter.puntaje += 100;
            MainCharacter.totalMoney += 10;
            Spawner.killscurrentRound++;
            MainCharacter.totalKills++;
            print("kills: " + Spawner.killscurrentRound);
            Destroy(gameObject);


        }
    }

    IEnumerator lanzar()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            print("lanzando");
            if(Vector3.Distance(jugador.transform.position, transform.position) > 8)
            {
                Instantiate(hatchet, referencia.position, referencia.transform.rotation);
            }
            
        }
    }
   
}
