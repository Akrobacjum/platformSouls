using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Stats : MonoBehaviour
{
    public float health;
    public float healthMax;

    public float stamina;
    public float staminaMax;
    public int staminaRegenPerSec;
    public int staminaRunPerSec;

    public bool staminaRegen;
    public bool staminaRun;
    void Start()
    {
        //Sets player statistics to maximum at scene's starts.
        health = healthMax;
        stamina = staminaMax;

        Debug.Log("Entity Health" + health);
    }
    void Update()
    {
        StatsCheck();
    }
    void StatsCheck()
    {
        //Checks if statistics are mathematically correct and restrained every frame. Also checks if entity should be slain.
        health = Mathf.Clamp(health, 0, healthMax);
        stamina = Mathf.Clamp(stamina, 0, staminaMax);
        if (health <= 0)
        {
            EntitySlay();
        }
    }
    public IEnumerator StaminaRegen()
    {
        //Regenerates stamina, checks if stamina number is correct and waits for one second before allowing next stamina regen coroutine. Turns off stamina run coroutine.
        staminaRegen = true;
        if (staminaRun == true)
        {
            staminaRun = false;
        }
        stamina = stamina + staminaRegenPerSec * Time.deltaTime;
        StatsCheck();
        //Debug.Log("Entity Stamina: " + stamina);
        yield return null;
        staminaRegen = false;
    }
    public IEnumerator StaminaRun()
    {
        //Decreases stamina, checks if stamina number is correct and waits for one second before allowing next stamina run coroutine. Turns off stamina regen coroutine.
        StopCoroutine(StaminaRegen());
        staminaRun = true;
        stamina = stamina - staminaRunPerSec * Time.deltaTime;
        StatsCheck();
        //Debug.Log("Entity Stamina: " + stamina);
        yield return null; ;
        staminaRun = false;
    }
    void EntitySlay()
    {
        //Currently displays log. Made to kill entity in the future.
        Debug.Log("Entity Slain");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Checks tag of trigger's object.
        string coliderTag = collision.gameObject.tag;

        switch (coliderTag)
        {
            case "Entity":
                //Deals damage to player.
                health--;

                Debug.Log("Entity Health: " + health);
                break;
        }
    }
}
