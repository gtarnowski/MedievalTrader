public class Utils {
	public static bool IsMaterial(string unitType) {
		return unitType == nameof(UnitType.Material);
	}
	
	public static bool IsProduct(string unitType) {
		return unitType == nameof(UnitType.Product);
	}
	
	public static bool IsResource(string unitType) {
		return unitType == nameof(UnitType.Resource);
	}
	
	public static bool IsBuilding(string unitType) {
		return unitType == nameof(UnitType.Building);
	}
	
	public static bool IsMine(string category) {
		return category == nameof(BuildingCategory.Mines);
	}
	
	public static bool IsWarehouse(string category) {
		return category == nameof(BuildingCategory.Warehouses);
	}
}