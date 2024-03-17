using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCollector : MonoBehaviour
{
    private static PuzzleCollector instance;
    public static int puzzleCount;
    
    private void Start()
    {
        puzzleCount = 9;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    
    public static void AddPuzzle()
    {
        puzzleCount++;
    }

    public static void ResetPuzzle()
    {
        puzzleCount = 0;
    }
}
