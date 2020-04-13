[System.Serializable]
public class Unit {
	public int id;
	public string name;
	public string spriteName;
	public string type;
	
	// For buildings, mines, warehouses
	public int price;

	// For shops products
	public int sellingPrice;
	
	// For materials, resources
	public int quantity;
}