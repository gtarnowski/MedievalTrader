using System;
using UnityEngine;

[System.Serializable]
public class Unit {
	public int id;
	public string name;
	public string spriteName;
	public string type;
	
	// For buildings, mines, warehouses
	public int price;
	public int size;
	public string category;

	// For shops products
	public int sellingPrice;
	
	// For materials, resources
	public int count;
	
	// For manufacturing
	public int productId;
	public string productName;
	public int buildingId;
	public RequiredMaterials[] requiredMaterials;
	
	// For mines
	public int[] resources;
	public int materialId;

	public int range;
}

[System.Serializable]
public class RequiredMaterials {
	public int materialId;
	public int count;
}