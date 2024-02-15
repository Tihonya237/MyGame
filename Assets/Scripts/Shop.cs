using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public string objectName, product;
    public int price, max, sum;

    public Text objectPrice, objectLvl, objectSum;
    private int coins, count;

    public int lvl;
    public Image lvl1, lvl2, lvl3;

    void Awake()
    {
        AccessUpdate();
    }

    void AccessUpdate()
    {
        ShopMenu.coins = PlayerPrefs.GetInt("coins");
        ShopMenu.GreenDimond = PlayerPrefs.GetInt("GreenDimond");
        ShopMenu.BlueDimond = PlayerPrefs.GetInt("BlueDimond");
        ShopMenu.HealthPotion = PlayerPrefs.GetInt("HealthPotion");

        lvl = PlayerPrefs.GetInt(objectName + "lvl", lvl);
        price = PlayerPrefs.GetInt(objectName + "price", price);
       
        objectPrice.text = (price).ToString();
        objectLvl.text = objectName + " " + "Lvl: " + lvl.ToString();

        count = PlayerPrefs.GetInt(product);

        if (lvl == 0)
        {
            lvl1.gameObject.SetActive(false);
            lvl2.gameObject.SetActive(false);
            lvl3.gameObject.SetActive(false);
        }
        else if(lvl == 1)
        {
            lvl1.gameObject.SetActive(true);
            lvl2.gameObject.SetActive(false);
            lvl3.gameObject.SetActive(false);
            objectPrice.text = (price).ToString();
        }
        else if(lvl == 2)
        {
            lvl1.gameObject.SetActive(false);
            lvl2.gameObject.SetActive(true);
            lvl3.gameObject.SetActive(false);
            objectPrice.text = (price).ToString();
        }
        else if(lvl == 3)
        {
            lvl1.gameObject.SetActive(false);
            lvl2.gameObject.SetActive(false);
            lvl3.gameObject.SetActive(true);
            price = 0;
            objectPrice.text = "MAX";
        }
    }

    //Покупка вещей
    public void BuyAndUpgrade()
    {
        coins = PlayerPrefs.GetInt("coins");

        if (lvl == 0 || lvl == 1 || lvl == 2)
        {
            if (coins > price)
            {
                count = PlayerPrefs.GetInt(product);
                PlayerPrefs.SetInt(product, count + 25);
                lvl += 1;
                PlayerPrefs.SetInt(objectName + "lvl", lvl);
                PlayerPrefs.SetInt("coins", coins - price);
                
                if (lvl == 1)
                {
                    lvl1.gameObject.SetActive(true);
                    lvl2.gameObject.SetActive(false);
                    lvl3.gameObject.SetActive(false);
                    price += 100;
                    PlayerPrefs.SetInt(product, count + 35);
                }
                else if (lvl == 2)
                {
                    lvl1.gameObject.SetActive(false);
                    lvl2.gameObject.SetActive(true);
                    lvl3.gameObject.SetActive(false);
                    price += 150;
                    PlayerPrefs.SetInt(product, count + 40);
                }

                PlayerPrefs.SetInt(objectName + "price", price);
                AccessUpdate();
            }
        }
    }

    public void Buy()
    {
        coins = PlayerPrefs.GetInt("coins");
        ShopMenu.HealthPotion = PlayerPrefs.GetInt("HealthPotion");

        if(max > ShopMenu.HealthPotion)
        {
            if (coins > price)
            {
                PlayerPrefs.SetInt("coins", coins - price);
                PlayerPrefs.SetInt("HealthPotion", ShopMenu.HealthPotion + 1);
                PlayerPrefs.SetInt(objectName + "price", price); 

                AccessUpdate();
            }
        } 
    }

    //Продажа кристалов
    public void SellGreenDimond()
    {
        coins = PlayerPrefs.GetInt("coins");

        ShopMenu.GreenDimond = PlayerPrefs.GetInt("GreenDimond");

        if (ShopMenu.GreenDimond > 0)
        {
            PlayerPrefs.SetInt("GreenDimond", ShopMenu.GreenDimond - 1);
            PlayerPrefs.SetInt("coins", coins + price);
            AccessUpdate();
        }
    }

    public void SellBlueDimond()
    {
        coins = PlayerPrefs.GetInt("coins");
        ShopMenu.BlueDimond = PlayerPrefs.GetInt("BlueDimond");

        if (ShopMenu.BlueDimond > 0)
        {
            PlayerPrefs.SetInt("BlueDimond", ShopMenu.BlueDimond - 1);
            PlayerPrefs.SetInt("coins", coins + price);
            AccessUpdate();
        }
    }
}
