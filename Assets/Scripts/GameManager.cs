using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    [SerializeField] GameObject Player;
    void Start()
    {
        if (manager == null)
        {
            manager = this;
        }
    }
}
