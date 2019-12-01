using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lightSaber : MonoBehaviour
{
    // 800 RPM -> 0.075 sec
    private int damage = 100;
    private float rpm = 1.5f;
    private bool automatic = false;
    public AudioClip sonido;
    private int cost = 0;
    public Text buy;

    private bool melee = true;

    public bool isMelee()
    {
        return melee;
    }

    private int magSize = 0;

    public int getMag()
    {
        return magSize;
    }

    private void Update()
    {
        if (Vector3.Distance(GameObject.Find("Jugador").transform.position, transform.position) < 3)
        {
            print("Press \'F\' to buy");
            buy.text = "Press \'F\' to buy Lightsaber. Cost: " + cost;
            if (Input.GetKeyDown(KeyCode.F) && MainCharacter.puntaje >= cost)
            {
                MainCharacter.setGun(automatic, damage, rpm, sonido, 3, magSize, melee, sonido);
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
}
