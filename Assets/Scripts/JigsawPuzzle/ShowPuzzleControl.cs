using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPuzzleControl : MonoBehaviour
{
    [SerializeField] public GameObject unlockPanel;
    [SerializeField] public GameObject puzzle;
    public GameObject copy;
    
    [Header("Buttons")]
    [SerializeField] public Button unlockBaracks;
    [SerializeField] public Button unlockHouse1;
    [SerializeField] public Button unlockHouse2;
    [SerializeField] public Button unlockHouse3;
    
    public static bool isPuzzleActive;

    private void Start()
    {
        unlockBaracks.onClick.AddListener(UnlockingWithPuzzle);
        unlockHouse1.onClick.AddListener(UnlockingWithPuzzle);
        unlockHouse2.onClick.AddListener(UnlockingWithPuzzle);
        unlockHouse3.onClick.AddListener(UnlockingWithPuzzle);
    }

    private void FixedUpdate()
    {
        if (isPuzzleActive)
        {
            unlockPanel.SetActive(false);
        }
        else if (!isPuzzleActive && JigsawGameManager.isPuzzleCompleted)
        {
            unlockPanel.SetActive(true);
            Destroy(copy);
        }
    }

    public void UnlockingWithPuzzle()
    {
        isPuzzleActive = true;
        copy = Instantiate(puzzle);
    }
}
