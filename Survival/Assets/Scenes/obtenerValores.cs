using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class obtenerValores : MonoBehaviour
{
    public Text puntaje, round, kills;
    // Start is called before the first frame update
    void Start()
    {
        puntaje.text = MainCharacter.totalMoney.ToString();
        round.text = MainCharacter.currentRound.ToString();
        kills.text = MainCharacter.totalKills.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
