using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO Refactor
public class TextUpdater : MonoBehaviour
{
    [SerializeField] Text[] text;

    PlayerController player;
    CollectableCollector collector;

    int playerHealth;
    int coinCount;
    int keyCount;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        collector = player.gameObject.GetComponent<CollectableCollector>();

        text = FindObjectsOfType<Text>();
    }

    void Update()
    {
        playerHealth = player.GetPlayerHealth();
        coinCount = collector.GetCoinCount();
        keyCount = collector.GetKeyCount();

        text[1].color = Color.black;
        text[1].text = "Health: " + playerHealth;

        text[0].color = Color.black;
        text[0].text = "Coins: " + coinCount + "\r\n" + "Keys: " + keyCount;
    }
}
