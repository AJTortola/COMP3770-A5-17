using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DungeonGenerator : MonoBehaviour
{

    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }


    public Vector2 size;
    public int startPos = 0;

    public GameObject endPointRoom;
    //public EndPoint room;
    //List of prefabs
    public GameObject[] rooms;
    public Vector2 offset;
    public int roomCount = 0;
    public int numOfRooms = 0;

    List<Cell> board;

    public NavMeshSurface[] navMeshSurfaces;

    // Start is called before the first frame update
    void Start()
    {
        
        rooms = Resources.LoadAll<GameObject>("Rooms");

        MazeGenerator();
        //UPDATE NAVMESH
        NavMeshBaker();
       


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NavMeshBaker(){
        NavMeshSurface[] navMeshSurfaces = (NavMeshSurface[]) GameObject.FindObjectsOfType (typeof(NavMeshSurface));
        for(int i = 0; i<navMeshSurfaces.Length; i++){
            navMeshSurfaces[i].BuildNavMesh();
        }
    }
    int chooseRandomPrefab()
    {
        int roomChoice = Random.Range(0,3);        //only 3 rooms to choose from minus the endpoint room
        return roomChoice;
    }
    void GenerateDungeon()
    {

        

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                
                Cell currentCell = board[Mathf.FloorToInt(i + j * size.x)];
                if (currentCell.visited)
                {
                    
                    if (roomCount != numOfRooms){
                        var newRoom = Instantiate(rooms[chooseRandomPrefab()] as GameObject, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                        newRoom.UpdateRoom(currentCell.status);
                        newRoom.name += " " + i + "-" + j;
                        roomCount++;
                    }
                    else{
                        var newRoom = Instantiate(endPointRoom, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                        newRoom.UpdateRoom(currentCell.status);
                        newRoom.name += " " + i + "-" + j;
                    }

                    
                }

            

            }
        }
    }

    void MazeGenerator()
    {
        board = new List<Cell>();

        for(int i =0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = startPos;

        Stack<int> path = new Stack<int>();

        int k = 0;

        while (k < 1000)
        {
            k++;

            board[currentCell].visited = true;

            if(currentCell == board.Count - 1)
            {
                break;
            }

            //check the cell's neighbours 
            List<int> neighbours = CheckNeighbours(currentCell);

            if(neighbours.Count == 0)
            {
                if(path.Count == 0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop();
                }
            }
            else
            {
                path.Push(currentCell);

                int newCell = neighbours[Random.Range(0, neighbours.Count)];
                numOfRooms++;
                
                if (newCell > currentCell)
                {
                    //down or right
                    if (newCell - 1 == currentCell)
                    {
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        board[currentCell].status[3] = true;
                    }
                    else
                    {
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        board[currentCell].status[0] = true;
                    }
                }
                else
                {
                    //up or left
                    if (newCell + 1 == currentCell)
                    {
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[currentCell].status[2] = true;
                    }
                    else
                    {
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        board[currentCell].status[1] = true;
                    }
                }
            }
        }
        GenerateDungeon();
    }

    List<int> CheckNeighbours(int cell)
    {
        List<int> neighbours = new List<int>();

        //check up neighbour
        if (cell - size.x >= 0 && !board[Mathf.FloorToInt(cell-size.x)].visited)
        {
            neighbours.Add(Mathf.FloorToInt(cell - size.x));
        }

        //check down neighbour
        if (cell + size.x < board.Count && !board[Mathf.FloorToInt(cell + size.x)].visited)
        {
            neighbours.Add(Mathf.FloorToInt(cell + size.x));
        }

        //check right neighbour
        if ((cell+1) % size.x != 0 && !board[Mathf.FloorToInt(cell +1)].visited)
        {
            neighbours.Add(Mathf.FloorToInt(cell + 1));
        }

        //check left neighbour
        if (cell % size.x != 0 && !board[Mathf.FloorToInt(cell - 1)].visited)
        {
            neighbours.Add(Mathf.FloorToInt(cell -1));
        }


        return neighbours; 
    }
}
