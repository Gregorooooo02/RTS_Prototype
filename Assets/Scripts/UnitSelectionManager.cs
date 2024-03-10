using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitSelectionManager : MonoBehaviour
{
	public static UnitSelectionManager Instance { get; set; }

	public List<GameObject> allUnitsList = new List<GameObject>();
	public List<GameObject> unitsSelected = new List<GameObject>();
	
	public LayerMask clickableLayer;
	public LayerMask groundLayer;
	public LayerMask enemyLayer;

	public GameObject groundMarker;
	
	private Camera camera;
	
	private void Awake()
	{
		camera = Camera.main;
		
		if (Instance != null & Instance != this)
		{
			Destroy(this);
		}
		else
		{
			Instance = this;
		}
	}

	private void Start()
	{
		camera = Camera.main;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			
			// If we are hitting clickable object
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableLayer,QueryTriggerInteraction.Ignore))
			{
				if (Input.GetKey(KeyCode.LeftShift))
				{
					MultiSelect(hit.collider.gameObject);
				}
				else
				{
					SelectByClick(hit.collider.gameObject);	
				}
			} 
			else // If we are NOT hitting clickable object (i.e. ground) - deselect all
			{
				if (!Input.GetKey(KeyCode.LeftShift))
				{
					DeselectAll();	
				}
			}
		}

		if (Input.GetMouseButtonDown(1) && unitsSelected.Count > 0)
		{
			RaycastHit hit;
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);


			if(Physics.Raycast(ray, out hit,Mathf.Infinity, enemyLayer))
			{
                groundMarker.SetActive(false);
            }
			else if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
			{
				groundMarker.transform.position = hit.point + new Vector3(0, 0.02f, 0);
				
				groundMarker.SetActive(false);
				groundMarker.SetActive(true);
			}
		}
	}
	
	private void SelectByClick(GameObject unit)
	{
		DeselectAll();
		
		unitsSelected.Add(unit);
		SelectUnit(unit, true);
	}

	private void MultiSelect(GameObject unit)
	{
		if (unitsSelected.Contains(unit) == false)
		{
			unitsSelected.Add(unit);
			SelectUnit(unit, true);
		}
		else
		{
			SelectUnit(unit, false);
			unitsSelected.Remove(unit);
		}
	}

	internal void DragSelect(GameObject unit)
	{
		if (unitsSelected.Contains(unit) == false)
		{
			unitsSelected.Add(unit);
			SelectUnit(unit, true);
		}
	}

	private void SelectUnit(GameObject unit, bool isSelected)
	{
		TriggerSelectionIndicator(unit, isSelected);
		EnableUnitMovement(unit, isSelected);
	}
	
	internal void DeselectAll()
	{
		foreach (var unit in unitsSelected)
		{
			SelectUnit(unit, false);
		}
		
		groundMarker.SetActive(false);
		unitsSelected.Clear();
	}

	private void EnableUnitMovement(GameObject unit, bool enable)
	{
		unit.GetComponent<UnitMovement>().enabled = enable;
	}

	private void TriggerSelectionIndicator(GameObject unit, bool enable)
	{
		//'if' below is for debuging purposes
		if(unit.transform.Find("Indicator") == null) Debug.Log(unit.name);

        unit.transform.Find("Indicator").gameObject.SetActive(enable);
	}
}
