using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{
    public NavMeshSurface[] navMeshSurfaces;

    void Start()
    {
        
        NavMeshSurface[] navMeshSurfaces = (NavMeshSurface[]) GameObject.FindObjectsOfType (typeof(NavMeshSurface));
        for(int i = 0; i<navMeshSurfaces.Length; i++){
            navMeshSurfaces[i].BuildNavMesh();
        }
        //GameObject Player = GameObject.Find("Capsule");
        //Player.AddComponent<NavMeshAgent>();
        
    }

}
