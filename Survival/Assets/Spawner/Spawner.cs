using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemigos;
    private Coroutine thread;
    private Coroutine threadTimerWait;
    private IEnumerator spawnerStart;
    public static int maxEnemigos;
    public static int contadorEnemigos;

    public  AudioClip roundStart, roundEnd, monjita;
    private  AudioSource player;

    public GameObject monja;
    public static int killscurrentRound;

    public Text time;

    private bool ameno;

    public GameObject [] enemySpawnerpositions;

    // Start is called before the first frame update
    void Start()
    {
        ameno = false;
        player = GetComponent<AudioSource>();
        player.clip = roundStart;
        player.Play();
        maxEnemigos = 1;
        killscurrentRound=0;
        contadorEnemigos=0;
        spawnerStart = timer();
        time.text = "";
        thread = StartCoroutine(spawnerStart);
    }

    // Update is called once per frame
    void Update()
    {
        if(killscurrentRound==maxEnemigos){
            killscurrentRound=0;
            maxEnemigos+=0;
            contadorEnemigos=0;
            print("round terminated!!");
            endRound();
            threadTimerWait = StartCoroutine(preparationForNextRound());

        }
        
    }
    public void startRound()
    {
        player.clip = roundStart;
        player.Play();
    }

    public void endRound()
    {
        player.clip = roundEnd;
        player.Play();
    }

    public void Monja()
    {
        player.clip = monjita;
        player.Play();
    }

    IEnumerator timer()
    {
        if ((MainCharacter.currentRound % 2) != 0 )
        {
            while (contadorEnemigos < maxEnemigos)
            {
                yield return new WaitForSeconds(1);
                int position = (int)Random.Range(0, enemySpawnerpositions.Length);
                print(position + " pos   " + contadorEnemigos + "enemies");
                Instantiate(enemigos[(int)Random.Range(0, enemigos.Length)], enemySpawnerpositions[position].transform.position, enemySpawnerpositions[position].transform.rotation);
                contadorEnemigos++;




            }
            yield break;
        }
        else
        {
            ameno = true;
            int position = (int)Random.Range(0, enemySpawnerpositions.Length);
            Instantiate(monja, enemySpawnerpositions[position].transform.position, enemySpawnerpositions[position].transform.rotation);
            contadorEnemigos++;
        }
    }


    IEnumerator preparationForNextRound(){
        int seconds=10;
        while(seconds>0){
            yield return new WaitForSeconds(1f);
            time.text="Next round in... "+seconds;
            seconds--;
        }
        time.text="";
        
        thread = StartCoroutine(timer());
        MainCharacter.currentRound++;
        
        if (!ameno)
        {
            startRound();
        }
        else
        {
            Monja();
            ameno = false;
        }
        yield break;
    }

    
}
