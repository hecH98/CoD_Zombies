using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MP5 : MonoBehaviour
{
    // 800 RPM -> 0.075 sec
    private int damage = 13;
    private float rpm = 0.075f;
    private bool automatic = true;
    public AudioClip sonido;
    public AudioClip recarga;
    private bool melee = false;
    public Text buy;
    private int cost = 0;

    public bool isMelee()
    {
        return melee;
    }

    private int magSize = 30;

    public int getMag()
    {
        return magSize;
    }

    private void Update()
    {
        if(Vector3.Distance(GameObject.Find("Jugador").transform.position, transform.position) < 3)
        {
            print("Press \'F\' to buy");
            buy.text = "Press \'F\' to buy MP5. Cost: "+cost;
            if (Input.GetKeyDown(KeyCode.F) && MainCharacter.puntaje >= cost)
            {
                MainCharacter.setGun(automatic, damage, rpm, sonido, 1, magSize, melee, recarga);
                MainCharacter.puntaje -= cost;
            }
        }
        else
        {
            buy.text = "";
        }

    }
    public int getDamage()
    {
        return damage;
    }

    public bool isAutomatic()
    {
        return automatic;
    }

    public float getRpm()
    {
        return rpm;
    }
    public AudioClip getAudio()
    {
        return sonido;
    }

    public AudioClip getReload()
    {
        return recarga;
    }
}
