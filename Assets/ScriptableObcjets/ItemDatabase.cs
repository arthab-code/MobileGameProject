using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "ScriptableObjects/ItemDatabase", order = 1)]
public class ItemDatabase : ScriptableObject
{
    [Header("Obstacle")]
    public GameObject[] Obstacle;

    [Header("Ground")]
    public GameObject[] GroundDown;

    [Header("Game Over Area")]
    public GameObject[] GameoverArea;

    [Header("Enemy")]
    public GameObject[] Enemy;

}
