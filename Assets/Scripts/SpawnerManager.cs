using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : Singleton<SpawnerManager>
{
    public List<GameObject> grounds;
    public List<GameObject> gameoverAreas;
    public List<GameObject> enemies;
    public List<GameObject> obstacles;

    public ItemDatabase itemDatabase;

    // Start is called before the first frame update
    void Start()
    {
        grounds = new List<GameObject>();
        gameoverAreas = new List<GameObject>();
        enemies = new List<GameObject>();
        obstacles = new List<GameObject>();

        RespawnGround();
        RespawnGameOverArea();
        RespawnEnemy();
        RespawnObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RespawnGround()
    {
        GameObject go = Instantiate(itemDatabase.GroundDown[0]);
        grounds.Add(go);
    }
    private void RespawnObstacle()
    {
        GameObject go = Instantiate(itemDatabase.Obstacle[0]);
        obstacles.Add(go);
    }
    private void RespawnGameOverArea()
    {
        GameObject go = Instantiate(itemDatabase.GameoverArea[0]);
        gameoverAreas.Add(go);
    }
    private void RespawnEnemy()
    {
        GameObject go = Instantiate(itemDatabase.Enemy[0]);
        enemies.Add(go);
    }
}
