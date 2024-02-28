using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionManager : MonoBehaviour
{
	public static UnitSelectionManager Instance { get; set; }

	public List<GameObject> allUnitsList = new List<GameObject>();
	public List<GameObject> unitsSelected = new List<GameObject>();
	
	public LayerMask clickableLayer;
	public LayerMask groundLayer;

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
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableLayer))
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

			if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
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
		TriggerSelectionIndicator(unit, true);
		EnableUnitMovement(unit, true);
	}

	private void MultiSelect(GameObject unit)
	{
		if (unitsSelected.Contains(unit) == false)
		{
			unitsSelected.Add(unit);
			TriggerSelectionIndicator(unit, true);
			EnableUnitMovement(unit, true);
		}
		else
		{
			TriggerSelectionIndicator(unit, false);
			EnableUnitMovement(unit, false);
			unitsSelected.Remove(unit);
		}
	}
	
	private void DeselectAll()
	{
		foreach (var unit in unitsSelected)
		{
			TriggerSelectionIndicator(unit, false);
			EnableUnitMovement(unit, false);
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
		unit.transform.Find("Indicator").gameObject.SetActive(enable);
	}
}
