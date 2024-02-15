using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private int health, maxHealth, damage, speed, Shootdamage, Armorlvl, Swordlvl;

    public Text countHealth, countSpeed, countDamage, countShootDamage, countArmorlvl, countSwordlvl;

    private void Awake()
    {
        UpgradeStats();
    }

    public void UpgradeStats()
    {
        health = PlayerPrefs.GetInt("Player Health");
        maxHealth = PlayerPrefs.GetInt("Player MaxHealth");
        damage = PlayerPrefs.GetInt("Player damage");
        speed = PlayerPrefs.GetInt("Player Speed");
        Shootdamage = PlayerPrefs.GetInt("Player ShootDamage");
        Armorlvl = PlayerPrefs.GetInt("Armorlvl");
        Swordlvl = PlayerPrefs.GetInt("Swordlvl");

        countHealth.text = "HP: " + health.ToString() + "/" + maxHealth.ToString();
        countSpeed.text = "Speed: " + speed.ToString();
        countDamage.text = "Damage: " + damage.ToString();
        countShootDamage.text = "ShootDamage: " + Shootdamage.ToString();
        countArmorlvl.text = "ArmorLvl: " + Armorlvl.ToString();
        countSwordlvl.text = "SwordLvl: " + Swordlvl.ToString();
    }
}
