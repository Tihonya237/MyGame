using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    public Text countCoins, countGreenDimond, countBlueDimond, countPotion;
    public static int coins, GreenDimond, BlueDimond, HealthPotion;

    void Awake()
    {
        if (PlayerPrefs.HasKey("GreenDimond") == false)
        {
            PlayerPrefs.SetInt("GreenDimond", 0);
        }

        if (PlayerPrefs.HasKey("BlueDimond") == false)
        {
            PlayerPrefs.SetInt("BlueDimond", 0);
        }

        updatecoins();
    }

    void Update()
    {
        countCoins.text = (coins).ToString();
        countGreenDimond.text = (GreenDimond).ToString();
        countBlueDimond.text = (BlueDimond).ToString();
        countPotion.text = (HealthPotion).ToString();
    }

    void updatecoins()
    {
        if (PlayerPrefs.HasKey("coins"))
        {
            coins = PlayerPrefs.GetInt("coins");
        }
        if (PlayerPrefs.HasKey("GreenDimond"))
        {
            GreenDimond = PlayerPrefs.GetInt("GreenDimond");
        }
        if (PlayerPrefs.HasKey("BlueDimond"))
        {
            BlueDimond = PlayerPrefs.GetInt("BlueDimond");
        }

        if(PlayerPrefs.HasKey("HealthPotion"))
        {
            HealthPotion = PlayerPrefs.GetInt("HealthPotion");
        }
    }
}
