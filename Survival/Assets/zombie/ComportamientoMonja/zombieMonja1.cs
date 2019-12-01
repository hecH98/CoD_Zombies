using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombieMonja1 : MonoBehaviour
{
    public float vida = 1000;
    //private Transform posicion;
    public int speed = 10;
    public static GameObject jugador;
    public NavMeshAgent agent;
    public float vidaMedia, vidaCuarto;
    

    private ANodo actual;
    private MonoBehaviour comportamiento;
    private Simbolo bossInicial, boss50, boss25;
    public static Transform referencia;
    public static GameObject hatchet;
    // Start is called before the first frame update 
    void Start()
    {
        hatchet = GameObject.Find("Axe_Small_scaled");
        referencia = gameObject.transform.GetChild(1);
        jugador = GameObject.Find("Jugador");
        actual = new ANodo("creando", typeof(Enemigo));
        ANodo inicial = new ANodo("inicial", typeof(BossInicial));
        ANodo segundaFace = new ANodo("enojado", typeof(Boss50));
        ANodo terceraFace = new ANodo("emputada", typeof(Boss25));


        bossInicial = new Simbolo("iniciando");
        boss50 = new Simbolo("enojarse");
        boss25 = new Simbolo("emputarse");

        actual.AddTransicion(bossInicial, inicial);
        inicial.AddTransicion(boss50, segundaFace);
        segundaFace.AddTransicion(boss25, terceraFace);
        CambiarEstado(bossInicial);
        print("la vida de la monja es: " + vida);
    }



    // Update is called once per frame
    void Update()
    {

        agent.SetDestination(jugador.transform.position);
        if (vida <= vidaMedia && vida > vidaCuarto)
        {
            CambiarEstado(boss50);
        }
        else if (vida <= vidaCuarto)
        {
            CambiarEstado(boss25);
        }
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

    void CambiarEstado(Simbolo simbolo)
    {
        ANodo temp = actual.AplicarTransicion(simbolo);


        if (actual != temp)
        {

            actual = temp;
            // deshacernos del behaviour actual
            Destroy(comportamiento);

            // reasignar referencia
            comportamiento = gameObject.AddComponent(actual.Comportamiento) as MonoBehaviour;
        }
    }
}
