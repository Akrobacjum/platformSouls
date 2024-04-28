using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//To solve: Interaction and unlocking is working, but with significant delay.

public class Firecamp : MonoBehaviour
{
    [SerializeField] GameObject FireCampMapButton;
    public bool isUnlocked = false;

    void Start()
    {
        //Unlocking firecamp should be loaded from player prefs, turn on before creating a build.
        //isUnlocked = PlayerPrefs.GetInt("isUnlocked") == 1 ? true : false;
    }
    void Update()
    {
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
        //Checks if player already unlocked firecamp and is interacting with it, or unlocking it for the first time.
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
    private void OnApplicationQuit()
    {
        //Saves state of firecamp (unlocked or not).
        PlayerPrefs.SetInt("isUnlocked", isUnlocked ? 1 : 0);
    }
}
