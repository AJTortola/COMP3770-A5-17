using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoomBehaviour : MonoBehaviour
{
    public GameObject[] walls;
    public GameObject[] doors;

    public NavMeshSurface surface;

    // Update is called once per frame
    public void UpdateRoom(bool[] status)
    {
        for(int i=0; i<status.Length; i++)
        {
            doors[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
        }
        
    }

    public void BakeNavMesh(){
        NavMeshSurface surface = (NavMeshSurface) gameObject.GetComponent<NavMeshSurface>();
        surface.BuildNavMesh();
    }
}
