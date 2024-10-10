using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public int health;
    [SerializeField] Transform minionSpawnPoint;
    [SerializeField] float spawnCooldown;
    [SerializeField] List<GameObject> minions;
    float timer;
    public int currentMinion;



    private void Start()
    {
        timer = spawnCooldown;
    }
    // Update is called once per frame
    void Update()
    {
        if(timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Instantiate(minions[currentMinion], minionSpawnPoint.position, Quaternion.identity);
            timer = spawnCooldown;
        }
    }
}
