using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatingManager : MonoBehaviour
{
    public static bool trialStarted = false;
    private bool isInitialized = false;

    [Range(1, 4)]
    [SerializeField] private int noOfPlayers;
    [SerializeField] private GameObject boatPrefab;
    private float spacing = 8f;

    void Update()
    {
        if (trialStarted && !isInitialized)
        {
            InitializeBoats();
            isInitialized = true;
        }
    }

    void InitializeBoats()
    {
        for (int i = 0; i < noOfPlayers; i++)
        {
            Vector3 spawnPosition = new Vector3(spacing * i, 0.5f, 0f);
            GameObject boat = Instantiate(boatPrefab, spawnPosition, Quaternion.Euler(90, 0, 0));

            Camera boatCamera = boat.GetComponentInChildren<Camera>();
            if (boatCamera != null)
            {
                boatCamera.rect = new Rect((float)i / noOfPlayers, 0f, 1f / noOfPlayers, 1f);
            }
        }
    }
}
