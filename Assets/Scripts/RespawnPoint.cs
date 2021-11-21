using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public Transform Player;
    public Transform Respawn;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.transform.position = Respawn.transform.position;
            Physics.SyncTransforms();
        }
    }
}
