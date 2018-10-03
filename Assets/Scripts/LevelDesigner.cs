using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesigner : MonoBehaviour
{
    public Vector3[] enemyArray;
    public Transform Enemy;
    public Transform Player;
    public Vector3 PlayerPosition;
    private int arrayCount;

    // Use this for initialization
    void Start()
    {
        arrayCount = 0;
        YeniMovement.IsLevelInstantiated = false;
        Instantiate(Player, PlayerPosition, Quaternion.identity);
        InvokeRepeating("createEnemy", 0.25f, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void createEnemy()
    {
        if (arrayCount < enemyArray.Length)
        {
            Instantiate(Enemy, enemyArray[arrayCount], Quaternion.identity);
        }
        else
        {
            CancelInvoke("createEnemy");
            YeniMovement.IsLevelInstantiated = true;
        }
        arrayCount++;

    }
}
