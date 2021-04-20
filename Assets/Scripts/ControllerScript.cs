using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    private Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerScript.isPlaying)
            return;

        if (playerScript.isControlling)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerScript.isControlling = true;
            playerScript.isBouncing = false;
        }
    }
}
