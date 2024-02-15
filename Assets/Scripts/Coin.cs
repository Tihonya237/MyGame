using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject ChestOpen, ChestClose;
    public static int op, cl;

    int rnd, rnd1, rnd2;
    public int prob = 20;
    public int prob1 = 60;

    public bool isOpen;

    public string id;

    public int open;

    void Start()
    {
       ChestOpen.SetActive(false);

       open = PlayerPrefs.GetInt("chest№" + id, open);

        if (open == 1)
        {
            ChestOpen.SetActive(true);
            ChestClose.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&& !isOpen)
        {
            rnd = Random.Range(1, 10);
            rnd1 = Random.Range(0, 100);
            rnd2 = Random.Range(0, 100);

            Coins();//Рандомное выпадение от 1 до 10 монет

            if (rnd1 <= prob)
            {
                BlueDimond();
            }

            if (rnd2 <= prob1)
            {
                GreenDimond();
            }

           if(!isOpen)
           {
                ChestOpen.SetActive(true);
                ChestClose.SetActive(false);
                open += 1;
                isOpen = true;
                PlayerPrefs.SetInt("chest№" + id, open);                
            }
        }
    }

    void Coins()
    {
        ShopMenu.coins += rnd;
        PlayerPrefs.SetInt("coins", ShopMenu.coins);
        PlayerPrefs.Save();
    }
    

    void GreenDimond()
    {
        ShopMenu.GreenDimond += 1;
        PlayerPrefs.SetInt("GreenDimond", ShopMenu.GreenDimond);
        PlayerPrefs.Save();
    }
    void BlueDimond()
    {
        ShopMenu.BlueDimond += 1;
        PlayerPrefs.SetInt("BlueDimond", ShopMenu.BlueDimond);
        PlayerPrefs.Save();
    }
}