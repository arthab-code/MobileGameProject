using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameplayManager.Instance.playerScript;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerScript.isPlaying == false)
            return;

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (playerScript.speed <= 0)
                return;

            playerScript.isBouncing = true;

            if (!playerScript.isControlling)
            {
                playerScript.BouncePosition = new Vector3(0, playerScript.BouncePosition.y-10f, 0);
            }
            else
            {
                playerScript.BouncePosition = new Vector3(0, playerScript.BouncePosition.y-2f, 0);
                playerScript.isControlling = false;               
            }
            
            playerScript.speed -= 0.5f;
        } 
    }
}
