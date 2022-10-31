using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enermy_spawn : MonoBehaviour
{
    public GameObject enermy;
    public GameObject nt_enermy;
    public GameManagerLogic gameManagerLogic;
    private BoxCollider area;
    public int count = 20;
    public int nt_enermy_count = 10;
    public int max_count = 30;
    private void Start()
    {
        area = GetComponent<BoxCollider>();

        for(int i = 0; i< count; i++)
        {
            Spawn();
            max_count--;
        }
        for (int i = 0; i < nt_enermy_count; i++)
        {
            Spawn_1();
        }
    }
    private void Update()
    {
        if (GameManagerLogic.spawn_control && max_count > 0)
        {
            Spawn();
            max_count--;
            if(max_count % 2 == 0)
            {
                Spawn_1();
            }
            GameManagerLogic.spawn_control = false;
        }
    }
    private void Spawn()
    {
        Vector3 spawnPos = GetRandomPosition();

        GameObject instance = Instantiate(enermy, spawnPos, Quaternion.identity);
        
    }
    private void Spawn_1()
    {
        Vector3 spawnPos = GetRandomPosition();

        GameObject instance = Instantiate(nt_enermy, spawnPos, Quaternion.identity);
    }
    private Vector3 GetRandomPosition()
    {
        Vector3 basePosition = transform.position;
        Vector3 size = area.size;

        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = basePosition.y + Random.Range(-size.y / 2f, size.y / 2f);
        float posZ = basePosition.z + Random.Range(-size.z / 2f, size.z / 2f);

        Vector3 spawnPos = new Vector3(posX, posY, posZ);

        return spawnPos;
    }
}
