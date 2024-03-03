using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
   [SerializeField] private GameObject mouseIndicator, cellIndicator, gridShader;
   [SerializeField] private InputManager inputManager;
   [SerializeField] private Grid grid;
   [SerializeField] private ObjectDatabase database;
   private int selectedObjectIndex = -1;

   private void Start()
   {
      StopPlacement();
   }

   private void StopPlacement()
   {
      selectedObjectIndex = -1;
      inputManager.setBuildMode(false);
   }
   
   private void PlaceStructure()
   {
      Vector3 mousePosition = inputManager.getSelectedMapPosition();
      Vector3Int gridPosition = grid.WorldToCell(mousePosition);
      GameObject newObject = Instantiate(database.objectsData[selectedObjectIndex].Prefab);
      Vector3 tempVec = grid.CellToWorld(gridPosition);
      tempVec.y -= 2.2f;
      tempVec.x += 0.5f;
      tempVec.z += 0.5f;
      newObject.transform.position = tempVec;
   }
   
   public void StartPlacement(int ID)
   {
      selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
      if (selectedObjectIndex < 0)
      {
         Debug.LogError($"No ID found {ID}");
         return;
      }
      cellIndicator.SetActive(inputManager.getBuildMode());
      gridShader.SetActive(inputManager.getBuildMode());
      PlaceStructure();
      
   }

   private void Update()
   {
      
      Vector3 mousePosition = inputManager.getSelectedMapPosition();
      Vector3Int gridPosition = grid.WorldToCell(mousePosition);
      // mouseIndicator.transform.position = mousePosition;
      cellIndicator.transform.position = grid.CellToWorld(gridPosition);
      inputManager.buildMode();
      cellIndicator.SetActive(inputManager.getBuildMode());
      // mouseIndicator.SetActive(inputManager.getBuildMode());
      gridShader.SetActive(inputManager.getBuildMode());
      if (Input.GetKeyDown("t"))
      {
         if (inputManager.getBuildMode() == true)
         {
            StartPlacement(0);
         }
         
      }
      if (Input.GetKeyDown(KeyCode.Escape))
      {
         StopPlacement();
      }
   }
}
