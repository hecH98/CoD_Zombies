using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prueba : MonoBehaviour
{
    public AudioClip sonido;
    private AudioSource player;
    bool canShoot;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<AudioSource>();
        player.clip = sonido;
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot)
        {
            if (Input.GetMouseButton(0))
            {
                StartCoroutine(disparo());
                canShoot = false;
            }
        }
    }

    IEnumerator disparo()
    {
        player.Play();
        yield return new WaitForSeconds(0.075f);
        canShoot = true;
        StopCoroutine(disparo());
    }
}
