using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour {

    public int spawnLocation;
    public Vector3[] SpawnPoints;
    Rigidbody rigidBodyRef;
    PlayerHP playerHPRef;
    LucidityControl lucidityControlRef;
    SheepCounterManager sheepCountRef;
    SpawnPoint spawnPointRef;
    public int CurrentHP;
    public int MaxHP;
    public int CurrentLucidity;
    public int MaxLucidity;
    public int SheepCount;
    public int SpawnLoc;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        rigidBodyRef = GetComponent<Rigidbody>();
        playerHPRef = GetComponent<PlayerHP>();
        lucidityControlRef = GetComponent<LucidityControl>();
        sheepCountRef = GameObject.FindWithTag("SheepCounterText").GetComponent<SheepCounterManager>();
        spawnPointRef = GameObject.FindWithTag("SpawnPoint").GetComponent<SpawnPoint>();

    }
    // Use this for initialization
    void Start ()
    {
        SaveSpawn();
        SpawnPlayer();

    }

    public void SpawnPlayer()
    {
        rigidBodyRef.position = SpawnPoints[SpawnLoc];
        Debug.Log(SpawnPoints[SpawnLoc]);
        playerHPRef.maxHealth = MaxHP;
        playerHPRef.currentHealth = CurrentHP;
        lucidityControlRef.limit = MaxLucidity;
        lucidityControlRef.balance = CurrentLucidity;
        sheepCountRef.sheepCount = SheepCount;

    }

    public void SaveSpawn()
    {
        CurrentHP = playerHPRef.currentHealth;
        MaxHP = playerHPRef.maxHealth;
        CurrentLucidity = lucidityControlRef.balance;
        MaxLucidity = lucidityControlRef.limit;
        SheepCount = sheepCountRef.sheepCount;
        SpawnLoc = spawnPointRef.spawnPoint;
        Debug.Log("Help Me");

    }

    /* 
     private void SpawnPlayer(Vector3 newPos)
   {
       rigidBodyRef.position = newPos;
   }
   */

}
