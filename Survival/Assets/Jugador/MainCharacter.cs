using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainCharacter : MonoBehaviour
{
    private int speed = 5;
    public static int currentRound = 1, totalMoney;
    public static int totalKills;
    public GameObject bala;
    public Transform salidaBala;
    private int vida = 100;
    public Text vidaText;
    public Text puntos;
    public Text round;
    public Text balas;
    public static int puntaje;
    private Ray disparo;
    private RaycastHit hit;
    Rigidbody rb;
    bool isGrounded;
    private bool canReload;
    private bool canShoot = true;
    //public static GameObject arma;

    private static AudioSource player;

    private Coroutine healing;
    public RawImage vidaUI;
    public RawImage vida100, vida90, vida80, vida70, vida60, vida50, vida40, vida30, vida20, vida10, vida0;

    public GameObject[] armas;
    static int posArma = 0;

    private static bool gunAutomatic;
    private static int gunDamage;
    private static float gunRpm;
    private static AudioClip sonido;
    private static AudioClip recargar;
    private static int magSize;
    private static int magazine;
    private static bool melee;

    private static bool readyForChange;
    private static int lastWeapon;

    public static void setGun(bool Automatic, int Damage, float Rpm, AudioClip sound, int posArmas, int mag, bool mel, AudioClip recarga)
    {
        gunAutomatic = Automatic;
        gunDamage = Damage;
        gunRpm = Rpm;
        player.clip = sound;
        lastWeapon = posArma;
        posArma = posArmas;
        magSize = mag;
        magazine = magSize;
        melee = mel;
        recargar = recarga;
        sonido = sound;
        readyForChange = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        totalKills = 0;
        totalMoney = 0;
        lastWeapon = 0;
        readyForChange = false;
        armas[posArma].SetActive(true);
        armas[1].SetActive(false);
        armas[2].SetActive(false);
        armas[3].SetActive(false);
        armas[4].SetActive(false);
        vidaText.text = "Vida: " + vida;
        canReload = false;
        healing = StartCoroutine(heal());
        round.text = currentRound.ToString();
        puntaje = 0;
        Screen.lockCursor = true;
        //arma = GameObject.Find("Gun");
        rb = GetComponent<Rigidbody>();
        isGrounded = true;
        vidaUI.texture = vida100.texture;
        player = GetComponent<AudioSource>();
        //print(arma.transform.GetChild(0).name);
        print(GameObject.Find("M1911").GetComponent<M1911>().name);
        print(GameObject.Find("M1911").GetComponent<M1911>());
        //print(GameObject.Find("M1911").GetComponent<M1911>().GetComponent<Renderer>().material);

        
        gunDamage = GameObject.Find("M1911").GetComponent<M1911>().getDamage();
        gunRpm = GameObject.Find("M1911").GetComponent<M1911>().getRpm();
        sonido = GameObject.Find("M1911").GetComponent<M1911>().getAudio();
        recargar = GameObject.Find("M1911").GetComponent<M1911>().getReload();
        gunAutomatic = GameObject.Find("M1911").GetComponent<M1911>().isAutomatic();
        //arma.GetComponent<Renderer>().material = GameObject.Find("M1911").GetComponent<M1911>().GetComponent<Renderer>().material;
        magSize = GameObject.Find("M1911").GetComponent<M1911>().getMag();
        melee = GameObject.Find("M1911").GetComponent<M1911>().isMelee();
        magazine = magSize;
        /*gunDamage = GameObject.Find("MP5").GetComponent<MP5>().getDamage();
        gunRpm = GameObject.Find("MP5").GetComponent<MP5>().getRpm();
        sonido = GameObject.Find("MP5").GetComponent<MP5>().getAudio();
        recargar = GameObject.Find("MP5").GetComponent<MP5>().getReload();
        gunAutomatic = GameObject.Find("MP5").GetComponent<MP5>().isAutomatic();
        magSize = GameObject.Find("MP5").GetComponent<MP5>().getMag();
        melee = GameObject.Find("MP5").GetComponent<MP5>().isMelee();*/
        magazine = magSize;
        player.clip = sonido;
        balas.text = magazine + "/" + magSize;
    }



    // Update is called once per frame
    void Update()
    {
        if (readyForChange)
        {
            armas[posArma].SetActive(true);
            armas[lastWeapon].SetActive(false);
            readyForChange = false;
        }
        round.text = "Round: " + currentRound.ToString();
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (melee)
        {
            balas.text = "melee";
        }
        else
        {
            balas.text = magazine + "/" + magSize;
        }

        transform.Translate(h * speed * Time.deltaTime, 0, v * speed * Time.deltaTime);

        Debug.DrawRay(salidaBala.position, salidaBala.forward, Color.green);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
                isGrounded = false;
                //print("jump");
            }

        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 7;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5;
        }

        if (gunAutomatic)
        {
            if (canShoot)
            {
                if (Input.GetMouseButton(0))
                {
                    canReload = true;
                    StartCoroutine(fire());
                    canShoot = false;
                }
            }
        }
        else
        {
            if (canShoot)
            {
                if (melee)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        StartCoroutine(golpe());
                        canShoot = false;

                    }
                }
                else {
                    if (Input.GetMouseButtonDown(0))
                    {
                        canReload = true;
                        StartCoroutine(fire());
                        canShoot = false;
                    }
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.R) && !melee && canReload)
        {
            canShoot = false;
            canReload = false;
            StartCoroutine(reload());
        }

        if (vida <= 0)
        {
            //print("Moriste");
            SceneManager.LoadScene(2);
            Screen.lockCursor = false;
        }

        puntos.text = "puntos " + puntaje;


    }

    public void healthbar()
    {
        if (vida == 100)
        {
            vidaUI.texture = vida100.texture;
        }
        else if (vida == 90)
        {
            vidaUI.texture = vida90.texture;
        }
        else if (vida == 80)
        {
            vidaUI.texture = vida80.texture;
        }
        else if (vida == 70)
        {
            vidaUI.texture = vida70.texture;
        }
        else if (vida == 60)
        {
            vidaUI.texture = vida60.texture;
        }
        else if (vida == 50)
        {
            vidaUI.texture = vida50.texture;
        }
        else if (vida == 40)
        {
            vidaUI.texture = vida40.texture;
        }
        else if (vida == 30)
        {
            vidaUI.texture = vida30.texture;
        }
        else if (vida == 20)
        {
            vidaUI.texture = vida20.texture;
        }
        else if (vida == 10)
        {
            vidaUI.texture = vida10.texture;
        }
        else if (vida == 0)
        {
            vidaUI.texture = vida0.texture;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemigo" || collision.transform.tag == "Sprinter" || collision.transform.tag == "Lanzador" || collision.transform.tag == "Hatchet" || collision.transform.tag == "Monja")
        {
            vida -= 20;
            vidaText.text = "Vida: " + vida;
            healthbar();
            StopCoroutine(healing);
            healing = StartCoroutine(heal());
        }
        if(collision.transform.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    IEnumerator fire()
    {
        if(magazine > 0)
        {
            player.Play();
            disparo = new Ray(salidaBala.position, salidaBala.forward);
            Debug.DrawRay(disparo.origin, disparo.direction * 100, Color.yellow, 10);
            magazine--;
            if (Physics.Raycast(disparo, out hit))
            {
                //print(hit.transform.name);
                if (hit.transform.tag == "Enemigo")
                {
                    puntaje += 10;
                    totalMoney += 10;
                    hit.collider.GetComponent<Enemigo>().vida = hit.collider.GetComponent<Enemigo>().vida - gunDamage;

                }
                else if (hit.transform.tag == "Sprinter")
                {
                    puntaje += 10;
                    totalMoney += 10;
                    hit.collider.GetComponent<ZombieCorredor>().agent.speed = 10;
                    hit.collider.GetComponent<ZombieCorredor>().vida = hit.collider.GetComponent<ZombieCorredor>().vida - gunDamage;

                }
                else if (hit.transform.tag == "Lanzador")
                {
                    puntaje += 10;
                    totalMoney += 10;
                    hit.collider.GetComponent<ZombieLanzador>().vida = hit.collider.GetComponent<ZombieLanzador>().vida - gunDamage;

                }
                else if (hit.transform.tag == "Monja")
                {
                    puntaje += 10;
                    totalMoney += 10;
                    print(hit.collider.GetComponent<zombieMonja1>().vida);
                    hit.collider.GetComponent<zombieMonja1>().vida = hit.collider.GetComponent<zombieMonja1>().vida - gunDamage;
                }
            }
            yield return new WaitForSeconds(gunRpm);
            canShoot = true;
            if(magazine == 0)
            {
                canShoot = false;
                canReload = false;
                StartCoroutine(waitForReload());
            }
            StopCoroutine(fire());
        }
    }

    IEnumerator golpe()
    {
        player.Play();
        disparo = new Ray(salidaBala.position, salidaBala.forward);
        Debug.DrawRay(disparo.origin, disparo.direction * 4, Color.yellow, 10);
        if (Physics.Raycast(disparo, out hit))
        {
            //print(hit.transform.name);
            if (hit.transform.tag == "Enemigo")
            {
                if(hit.distance < 4)
                {
                    puntaje += 10;
                    totalMoney += 10;
                    hit.collider.GetComponent<Enemigo>().vida = hit.collider.GetComponent<Enemigo>().vida - gunDamage;
                }
            }
            else if(hit.transform.tag == "Sprinter")
            {
                if (hit.distance < 4)
                {
                    puntaje += 10;
                    totalMoney += 10;
                    hit.collider.GetComponent<ZombieCorredor>().agent.speed = 10;
                    hit.collider.GetComponent<ZombieCorredor>().vida = hit.collider.GetComponent<ZombieCorredor>().vida - gunDamage;
                }
            }
            else if (hit.transform.tag == "Lanzador")
            {
                if (hit.distance < 4)
                {
                    puntaje += 10;
                    totalMoney += 10;
                    hit.collider.GetComponent<ZombieLanzador>().vida = hit.collider.GetComponent<ZombieLanzador>().vida - gunDamage;
                }

            }
            else if (hit.transform.tag == "Monja")
            {
                if (hit.distance < 4)
                {
                    puntaje += 10;
                    totalMoney += 10;
                    hit.collider.GetComponent<zombieMonja1>().vida = hit.collider.GetComponent<zombieMonja1>().vida - gunDamage;
                }
            }

        }
        
        yield return new WaitForSeconds(gunRpm);
        canShoot = true;
        StopCoroutine(golpe());
    }

    IEnumerator reload()
    {
        player.clip = recargar;
        player.Play();
        print("recargando...");
        yield return new WaitForSeconds(1.8f);
        magazine = magSize;
        canShoot = true;
        print("Termiando!");
        player.clip = sonido;
        StopCoroutine(reload());
    }

    IEnumerator heal()
    {
        yield return new WaitForSeconds(5);
        while (vida < 100)
        {
            yield return new WaitForSeconds(1);
            vida += 10;
            vidaText.text = "Vida: " + vida;
            print("healing");
            healthbar();
        }
        StopCoroutine(heal());
    }

    IEnumerator waitForReload()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(reload());
        StopCoroutine(waitForReload());
    }
}
