﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombiSpawn : MonoBehaviour
{
    public GameObject[] prefab;
    public float delay;
    public AudioSource ZombiesAreComing;
    private Nullable<int> start = null;
    private Nullable<int> end = 2;
    private int numberOfSpawns;
    private int typeOfSpawns = 1;
    private int currentIndex = 0;
    void Start() {
        numberOfSpawns = Levels.spawns[Counter.currentLevel - 1, 0];
        typeOfSpawns = Levels.spawns[Counter.currentLevel - 1, 1];
        for (int i = 0; i < 5; i++)
        {
            if (start == null && Levels.levelTilemaps[Counter.currentLevel - 1, i] == 1)
                start = i - 2;
            if (start != null && Levels.levelTilemaps[Counter.currentLevel - 1, i] == 0)
            {
                end = i - 3;
                break;
            }
        }
        ZombiesAreComing.PlayDelayed(delay);
        Invoke("Spawn", delay);
    }

    private void Spawn()
    {
        if (currentIndex < numberOfSpawns)
        {
            Instantiate(prefab[UnityEngine.Random.Range(0, typeOfSpawns)], new Vector3(8, (float)Math.Round(UnityEngine.Random.Range((float)start, (float)end)), 0), Quaternion.identity);
            currentIndex++;
            Invoke("Spawn", UnityEngine.Random.Range(5f, 15f));
        }
        if(currentIndex > 30)
        {
            Invoke("SpawnWave", 10f);
        }
    }

    void SpawnWave()
    {
        for(int i = currentIndex; i < numberOfSpawns; i++)
        {
            Instantiate(prefab[UnityEngine.Random.Range(0, typeOfSpawns)], new Vector3(UnityEngine.Random.Range(8, 10), (float)Math.Round(UnityEngine.Random.Range((float)start, (float)end)), 0), Quaternion.identity);
            currentIndex++;
        }
    }
}
