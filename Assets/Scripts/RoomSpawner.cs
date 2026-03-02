using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public Direction openSide;

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    public void Spawn()
    {

        if (spawned == false)
        {
            spawned = true;
            templates.RectifyEnemy.Add(transform.position);
            if (openSide == Direction.Top)
            {
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
            }
            else if (openSide == Direction.Bottom)
            {
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
            }
            else if (openSide == Direction.Right)
            {
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
            }
            else if (openSide == Direction.Left)
            {
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (templates.RectifyEnemy.Contains(other.transform.position))
        {
            return;
        }

        if (!other.CompareTag("SpawnPoint"))
        {
            return;
        }

        RoomSpawner otherSpawner = other.GetComponent<RoomSpawner>();
        if (otherSpawner == null) { 
            return;
        }

        if (spawned && otherSpawner.spawned)
        {
            return;
        }
            

        if (GetInstanceID() < otherSpawner.GetInstanceID())
        {
            spawned = true;
            Destroy(gameObject);
        }
        else if (GetInstanceID() > otherSpawner.GetInstanceID())
        {
            Debug.Log($"NameOther {other.transform.parent.name} - {other.name}");
            Debug.Log($"NameThis {transform.parent.name} - {transform.name}");
            Debug.Log($"PosOther {other.transform.parent.transform.position} - {other.transform.position}");
            Debug.Log($"PosThis {transform.parent.transform.position} - {transform.position}");
            CloseRoom();
        }

    }

    public void CloseRoom()
    {
        float posY = transform.position.y - GetPosYCloseRoom();
        float posX = transform.position.x - GetPosXCloseRoom();
        Vector3 pos = new Vector3(posX, posY, -1);
        GameObject wall = Instantiate(templates.closedRoom, pos, Quaternion.identity);
        wall.transform.localScale = new Vector2(2, 2);
        wall.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public int GetPosYCloseRoom()
    {
        switch (openSide)
        {
            case Direction.Left: return 0;
            case Direction.Right: return 0;
            case Direction.Top: return 5;
            case Direction.Bottom: return -5;
            default: return 0;
        }
    }

    public int GetPosXCloseRoom()
    {
        switch (openSide)
        {
            case Direction.Left: return -5;
            case Direction.Right: return 5;
            case Direction.Top: return 0;
            case Direction.Bottom: return 0;
            default: return 0;
        }
    }
}
