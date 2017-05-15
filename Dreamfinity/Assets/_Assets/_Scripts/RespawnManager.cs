using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour {

    public int spawnLocation;
    public Vector3[] SpawnPoints;
    Rigidbody rigidBodyRef;
    LucidityControl lucidityControlRef;
    SheepCounterManager sheepCountRef;
    SpawnPoint spawnPointRef;
    public int CurrentHP;
    public int MaxLuc;
    public int CurrentLucidity;
    public int MaxLucidity;
    public int SheepCount;
    public int SpawnLoc;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        rigidBodyRef = GetComponent<Rigidbody>();
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


        lucidityControlRef.limit = MaxLucidity;
        lucidityControlRef.balance = CurrentLucidity;
        sheepCountRef.sheepCount = SheepCount;

    }

    public void SaveSpawn()
    {
        CurrentHP = lucidityControlRef.balance;
        MaxLuc = lucidityControlRef.limit;
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
