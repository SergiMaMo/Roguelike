using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject closedRoom;

    [NonSerialized] public List<Vector2> RectifyEnemy = new List<Vector2>();

    public List<GameObject> rooms = new List<GameObject>();

    public GameObject simpleEnemy;
    public GameObject boss;

    public void Awake()
    {
        RectifyEnemy.Add(new Vector2(0, 0));
    }

    public void Start()
    {
        Invoke("SpawnEnemies", 3f);
    }

    public void SpawnEnemies()
    {
        Instantiate(boss, rooms[rooms.Count - 1].transform.position, Quaternion.identity);
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms.Count - 1 != i)
            {
                Instantiate(simpleEnemy, rooms[i].transform.position, Quaternion.identity);
            }
        }
    }
}
