using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Show Player's current money.
 */

public class MoneyCounter : MonoBehaviour {

    private Text MoneyText;


    void Awake()
    {
        MoneyText = GetComponent<Text>();
    }

    /*
     * Show Player's current money from GameMaster.
     */
    void Update()
    {
        MoneyText.text = "MONEY: " + GameMaster.Money;
    }
}
