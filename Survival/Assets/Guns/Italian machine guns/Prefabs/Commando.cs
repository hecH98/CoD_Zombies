using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Commando : MonoBehaviour
{
    // 750 RPM -> 0.08 sec
    public static int damage = 20;
    private bool automatic = true;
    private float rpm = 0.08f;
    public AudioClip sonido;
    public AudioClip recarga;
    private int magSize = 30;
    private bool melee = false;
    public Text buy;
    private int cost = 0;
    public bool isMelee()
    {
        return melee;
    }
    public int getMag()
    {
        return magSize;
    }
    private void Update()
    {
        if (Vector3.Distance(GameObject.Find("Jugador").transform.position, transform.position) < 3)
        {
            print("Press \'F\' to buy");
            buy.text = "Press \'F\' to buy Commando. Cost: " + cost;
            if (Input.GetKeyDown(KeyCode.F) && MainCharacter.puntaje >= cost)
            {
                MainCharacter.setGun(automatic, damage, rpm, sonido, 2, magSize, melee, recarga);
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
