using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnerMove : MonoBehaviour
{

    private Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameplayManager.Instance.playerScript;
        transform.position = new Vector3(playerScript.transform.position.x, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerScript.transform.position.x, 0, 0);
    }
}
