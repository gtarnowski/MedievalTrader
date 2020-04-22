using UnityEngine;

public class GameData : MonoBehaviour {
  private static GameData instance;
  public TextAsset materialsJsonFile;
  public TextAsset resourcesJsonFile;
  public TextAsset buildingsJsonFile;
  public TextAsset productsJsonFile;
  public TextAsset processesJsonFile;

  private Units materialsJson;
  private Units resourcesJson;
  private Units buildingsJson;
  private Units productsJson;
  private Units processesJson;

  void Awake() {
    instance = this;
    ReadMaterials();
    ReadBuildings();
    ReadResources();
    ReadProducts();
    ReadProcesses();
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

  private void ReadProcesses() {
    processesJson = JsonUtility.FromJson<Units>(processesJsonFile.text);
  }

  public static Units GetUnitByType(ModelType type) {
    switch (type) {
      case ModelType.Buildings: return instance.buildingsJson;
      case ModelType.Materials: return instance.materialsJson;
      case ModelType.Products: return instance.productsJson;
      case ModelType.Resources: return instance.resourcesJson;
      case ModelType.Processes: return instance.processesJson;
      default: return null;
    }
  }
}
