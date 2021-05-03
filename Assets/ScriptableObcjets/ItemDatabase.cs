using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "ScriptableObjects/ItemDatabase", order = 1)]
public class ItemDatabase : ScriptableObject
{
    [Header("Obstacle")]
    public GameObject[] Obstacle;

    [Header("Terrain")]
    public GameObject[] Terrain;

    [Header("Enemy")]
    public GameObject[] Enemy;

}
