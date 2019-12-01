using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieCorredor : MonoBehaviour
{
    public int vida = 100;
    //private Transform posicion;
    GameObject jugador;
    public NavMeshAgent agent;
    // Start is called before the first frame update 
    void Start()
    {
        jugador = GameObject.Find("Jugador");
        //posicion = jugador.transform;
        agent.speed = 1;
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
}
