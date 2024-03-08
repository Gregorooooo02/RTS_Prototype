using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
   [SerializeField] private GameObject mouseIndicator, cellIndicator, gridShader, coursorIndicator;
   
   [SerializeField] private InputManager inputManager;
   
   [SerializeField] private Grid grid;
   
   [SerializeField] private ObjectDatabase database;
   
   private GridData floorData, buildingData;
   
   private int selectedObjectIndex = -1;
   
   private Renderer previewRenderer;

   private List<GameObject> placedGameObjects = new();
   
   private Vector3 greenPreviewScale = new Vector3(1.0f, 1.0f, 1.0f);
   
   private Vector3 redPreviewScale = new Vector3(1.3f, 1.3f, 1.3f);
   
   
   

   private void Start()
   {
      StopPlacement();
      floorData = new();
      buildingData = new();
      previewRenderer = cellIndicator.GetComponentInChildren<Renderer>();
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

      bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
      if (placementValidity == false)
      {
         Debug.LogError("Place occupied");
         return;
      }

      GameObject newObject = Instantiate(database.objectsData[selectedObjectIndex].Prefab);
      Vector3 tempVec = grid.CellToWorld(gridPosition);
      tempVec.y -= 2.2f;
      tempVec.x += 0.5f;
      tempVec.z += 0.5f;
      newObject.transform.position = tempVec;
      placedGameObjects.Add(newObject);
      GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ? floorData : buildingData;
      selectedData.AddObjectAt(gridPosition, database.objectsData[selectedObjectIndex].Size, database.objectsData[selectedObjectIndex].ID,
         placedGameObjects.Count - 1);
   }

   private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
   {
      GridData selectedData = database.objectsData[this.selectedObjectIndex].ID == 0 ? floorData : buildingData;
      return selectedData.CanPlaceObjectAt(gridPosition, database.objectsData[this.selectedObjectIndex].Size); 
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
      if (inputManager.getBuildMode() && selectedObjectIndex != -1)
      {
         bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
         if (placementValidity)
         {
            previewRenderer.material.color = Color.green;
            coursorIndicator.transform.localScale = greenPreviewScale;
         }
         else
         {
            //change scale of the object
            coursorIndicator.transform.localScale = redPreviewScale;
            
            previewRenderer.material.color = Color.red;
         }
      }


      if (Input.GetKeyDown("t"))
         {
            if (inputManager.getBuildMode() == true)
            {
               StartPlacement(0);
            }
         }

         if (Input.GetKeyDown("y"))
         {
            if (inputManager.getBuildMode() == true)
            {
               StartPlacement(1);
            }
         }

         if (Input.GetKeyDown(KeyCode.Escape))
         {
            StopPlacement();
         }
      }
   
}
