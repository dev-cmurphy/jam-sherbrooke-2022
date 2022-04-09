using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player = GetComponent<PlayerMovement>();

        if (player)
        {
            player.SlowDown();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = GetComponent<PlayerMovement>();

        if (player)
        {
            player.RemoveSlow();
        }
    }
}
