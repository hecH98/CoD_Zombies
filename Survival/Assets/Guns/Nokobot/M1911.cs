using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M1911 : MonoBehaviour
{
    // RPM 635 -> 0.096 sec
    private int damage = 10;
    private float rpm = 0.096f;
    private bool automatic = false;
    public AudioClip sonido;
    public AudioClip recarga;
    private bool melee = false;
    public Text buy;
    private int cost = 00;
    public bool isMelee()
    {
        return melee;
    }

    private int magSize = 8;

    public int getMag()
    {
        return magSize;
    }
    private void Update()
    {
        if (Vector3.Distance(GameObject.Find("Jugador").transform.position, transform.position) < 3)
        {
            print("Press \'F\' to buy");
            buy.text = "Press \'F\' to buy M1911. Cost: " + cost;
            if (Input.GetKeyDown(KeyCode.F) && MainCharacter.puntaje >= cost)
            {
                MainCharacter.setGun(automatic, damage, rpm, sonido, 0, magSize, melee, recarga);
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
