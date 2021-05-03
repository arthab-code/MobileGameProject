using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsManager : Singleton<GameSettingsManager>
{
    public GameObject gameoverArea;
    public GameObject ground;

    public Vector3 gameoverAreaPosition;
    public Vector3 groundPosition;
    // Start is called before the first frame update
    void Start()
    {
        gameoverAreaPosition = gameoverArea.transform.position;
        groundPosition = ground.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
