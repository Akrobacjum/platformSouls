using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    [SerializeField] GameObject Player;
    void Start()
    {
        //Does nothing at this point.
        if (manager == null)
        {
            manager = this;
        }
    }
}
