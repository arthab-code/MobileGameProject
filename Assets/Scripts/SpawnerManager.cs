using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : Singleton<SpawnerManager>
{
    public float durationEnemyRespawn = 0.5f;
    public float durationObstacleRespawn = 4f;

    public List<GameObject> terrains;
    public List<Enemy> enemies;
    public List<GameObject> obstacles;

    public ItemDatabase itemDatabase;

    public GameObject leftSpawn;
    public GameObject rightSpawn;

    private SpriteRenderer groundRenderer;

    private Player playerScript;
    private Enemy enemyScript;
    // Start is called before the first frame update
    void Start()
    {
        terrains = new List<GameObject>();
        enemies = new List<Enemy>();
        obstacles = new List<GameObject>();
        playerScript = GameplayManager.Instance.playerScript;

        SpriteRenderer[] spriteRenderers  = itemDatabase.Terrain[0].GetComponentsInChildren<SpriteRenderer>();
        groundRenderer = spriteRenderers[0];

        SetSpawnPoints();
        ReloadEnemyScript(0);
        StartRespawn(0);
        StartCoroutine(EnemyRespawner());
        StartCoroutine(ObstacleRespawner());
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.isPlaying == true)
        {
            if (GreaterPlayerPosition())
                RespawnBounds(0);
            if (FirstTerrainOffCamera())
                RemoveFirstTerrain();
        }

    }

    private void StartRespawn(int itemDatabaseIndex)
    {
        SceneBoundsRespawn(itemDatabase.Terrain[itemDatabaseIndex]);
    }

    private void RespawnBounds(int itemDatabaseIndex)
    {
        InstantiateBounds(itemDatabase.Terrain[itemDatabaseIndex]);
    }

    private void SceneBoundsRespawn(GameObject prefab)
    {
        if (terrains.Count == 0)
        {
            Transform[] children;
            GameObject go = Instantiate(prefab);
            go.transform.position = new Vector3(go.transform.position.x - (groundRenderer.bounds.size.x - 0.1f), go.transform.position.y, 0);
            children = go.GetComponentsInChildren<Transform>();
            terrains.Add(go);

            go = Instantiate(prefab);
            children = go.GetComponentsInChildren<Transform>();
            terrains.Add(go);

            go = Instantiate(prefab);
            go.transform.position = new Vector3(go.transform.position.x + (groundRenderer.bounds.size.x - 0.1f), go.transform.position.y, 0);
            children = go.GetComponentsInChildren<Transform>();
            terrains.Add(go);

            for (int i=0; i<terrains.Count; i++)
            {
                children = terrains[i].GetComponentsInChildren<Transform>();
                children[1].position = new Vector3(children[1].position.x, children[1].position.y-0.4f, 0);
                children[2].position = new Vector3(children[2].position.x, children[2].position.y, 0);
            }

            return;

        }
    }

    private bool GreaterPlayerPosition()
    {
        return playerScript.transform.position.x > terrains[terrains.Count - 2].transform.position.x ? true : false;
    }

    private bool FirstTerrainOffCamera()
    {
        return terrains[0].transform.position.x < playerScript.transform.position.x - groundRenderer.bounds.size.x  ? true : false;
    }

    private void InstantiateBounds(GameObject prefab)
    {
        GameObject go = Instantiate(prefab);
        go.transform.position = new Vector3(terrains[terrains.Count - 1].transform.position.x + groundRenderer.bounds.size.x, terrains[terrains.Count - 1].transform.position.y, 0);
        terrains.Add(go);

        go = Instantiate(prefab);
        go.transform.position = new Vector3(terrains[terrains.Count - 1].transform.position.x + groundRenderer.bounds.size.x, terrains[terrains.Count - 1].transform.position.y, 0);
        terrains.Add(go);
    }

    private void RemoveFirstTerrain()
    {
        Destroy(terrains[0].gameObject);

        terrains.RemoveAt(0);
    }

    /* ENEMY RESPAWN */

    private IEnumerator EnemyRespawner()
    {
        while (true)
        {
            InstantiateEnemy(0);
            yield return new WaitForSeconds(durationEnemyRespawn);
        }
    }

    private void InstantiateEnemy(int index)
    {
        int random = Random.Range(0, 2);

        if (random == 0)
        {
            /* RIGHT SPAWN */
            GameObject go = Instantiate(itemDatabase.Enemy[index]);
            Enemy enemyScript = go.GetComponent<Enemy>();
            enemyScript.transform.position = rightSpawn.transform.position;
            enemyScript.speed = playerScript.MaxSpeed * (-1);
            enemies.Add(enemyScript);
        }
        else if (random == 1)
        {
            /* LEFT SPAWN */
            GameObject go = Instantiate(itemDatabase.Enemy[index]);
            Enemy enemyScript = go.GetComponent<Enemy>();
            enemyScript.transform.position = leftSpawn.transform.position;
            enemyScript.speed = playerScript.MaxSpeed + (playerScript.MaxSpeed / 2);
            enemies.Add(enemyScript);
        }
    }

    private void ReloadEnemyScript(int index)
    {
        enemyScript = itemDatabase.Enemy[index].GetComponent<Enemy>();
    }

    private void SetSpawnPoints()
    {
        rightSpawn.transform.position = new Vector3(Camera.main.transform.position.x + (groundRenderer.bounds.size.x / 2) + 3f, rightSpawn.transform.position.y, 0);
        leftSpawn.transform.position = new Vector3(Camera.main.transform.position.x - (groundRenderer.bounds.size.x / 2) - 7f, leftSpawn.transform.position.y, 0);

    }

    private IEnumerator ObstacleRespawner()
    {
        while(true)
        {
           yield return new WaitForSeconds(durationObstacleRespawn);
            InstantiateObstacle(0);
        }
    }

    private void InstantiateObstacle(int index)
    {
        GameObject go = Instantiate(itemDatabase.Obstacle[index]);
        go.transform.position = terrains[terrains.Count - 1].transform.position;
        obstacles.Add(go);

    }
}
