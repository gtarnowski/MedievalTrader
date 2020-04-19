using UnityEngine;

public class GameData : MonoBehaviour {
  private static GameData instance;
  public TextAsset materialsJsonFile;
  public TextAsset resourcesJsonFile;
  public TextAsset buildingsJsonFile;
  public TextAsset productsJsonFile;

  private Units materialsJson;
  private Units resourcesJson;
  private Units buildingsJson;
  private Units productsJson;

  void Awake() {
    instance = this;
    ReadMaterials();
    ReadBuildings();
    ReadResources();
    ReadProducts();
  }
  
  private void ReadMaterials() {
    materialsJson = JsonUtility.FromJson<Units>(materialsJsonFile.text);
  }

  private void ReadBuildings() {
    buildingsJson = JsonUtility.FromJson<Units>(buildingsJsonFile.text);
  }

  private void ReadResources() {
    resourcesJson = JsonUtility.FromJson<Units>(resourcesJsonFile.text);
  }

  private void ReadProducts() {
    productsJson = JsonUtility.FromJson<Units>(productsJsonFile.text);
  }

  public static Units GetUnitByType(ModelType type) {
    switch (type) {
      case ModelType.Buildings: return instance.buildingsJson;
      case ModelType.Materials: return instance.materialsJson;
      case ModelType.Products: return instance.productsJson;
      case ModelType.Resources: return instance.resourcesJson;
      default: return null;
    }
  }
}
