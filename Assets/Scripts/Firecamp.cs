using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//To solve: Interaction and unlocking is working, but with significant delay.

public class Firecamp : MonoBehaviour
{
    [SerializeField] GameObject FireCampMapButton;
    [SerializeField] GameObject Spawnpoint;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject UIManager;

    UIManager UIManagerScript;
    Stats Stats;

    public bool isUnlocked = false;
    bool PlayerTrigger;

    void Start()
    {
        UIManagerScript = UIManager.GetComponent<UIManager>();
        Stats = Player.GetComponent<Stats>();

        //Unlocking firecamp should be loaded from player prefs, turn on before creating a build.
        //isUnlocked = PlayerPrefs.GetInt("isUnlocked") == 1 ? true : false;
    }
    void Update()
    {
        PlayerInteraction();
        //Checks if firecamp is unlocked and controls if player can teleport to it from map menu.
        if (isUnlocked)
        {
            FireCampMapButton.SetActive(true);
        }
        else
        {
            FireCampMapButton.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerTrigger = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerTrigger = false;
    }

    private void PlayerInteraction()
    {
        if (PlayerTrigger)
        {
            if (isUnlocked && Input.GetButtonDown("Interact"))
            {
                Debug.Log("Campfire: Interaction");
            }
            else if (Input.GetButtonDown("Interact"))
            {
                isUnlocked = true;
                Debug.Log("Campfire: " + isUnlocked);
            }
        }
    }
    private void OnApplicationQuit()
    {
        //Saves state of firecamp (unlocked or not).
        PlayerPrefs.SetInt("isUnlocked", isUnlocked ? 1 : 0);
    }
    public void OnClickFirecamp()
    {
        //Teleports player to firecamp, resumes game and refills his stats.
        Debug.Log("Firecamp: Teleport");
        Player.transform.position = Spawnpoint.transform.position;
        Stats.health = Stats.healthMax;
        Stats.stamina = Stats.staminaMax;
        UIManagerScript.Resume();
    }
}
