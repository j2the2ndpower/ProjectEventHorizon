using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpawnItem {
  public GameObject prefab;
  public int weight;
}

[RequireComponent(typeof(BoxCollider))]
public class Spawner : MonoBehaviour {
    public List<SpawnItem> PrefabsToSpawn;
    public Bounds SpawnVelocity;
    public float SpawnFrequency = 5;

    private BoxCollider boxCollider;
    private float SpawnTimer = 0;
    private int currentSpawnIndex = 0;
    private List<int> RandomIndexToSpawn = new List<int>();

    void Start() {
      boxCollider = GetComponent<BoxCollider>();
      RandomIndexToSpawn = MakeRandomListOfItemsToSpawn();
    }

    void Update() {
      if (PrefabsToSpawn.Count == 0) return;
      SpawnTimer += Time.deltaTime;
      
      if (SpawnTimer >= SpawnFrequency) {
        GameObject spawnedObject = Instantiate(GetWeightedRandomItem(), GetRandomPointInBounds(boxCollider.bounds), Random.rotation);
        Rigidbody body = spawnedObject.GetComponent<Rigidbody>();
        Obstacle ob = spawnedObject.GetComponent<Obstacle>();

        Vector3 obstacleVelocity = GetRandomPointInBounds(SpawnVelocity);

        if (ob) {
          ob.spawnVelocity = obstacleVelocity;
        }

        if (body) {
          body.velocity = obstacleVelocity;
        }
        SpawnTimer = 0;
      }
    }

    Vector3 GetRandomPointInBounds(Bounds bounds) {
      return new Vector3(
          Random.Range(bounds.min.x, bounds.max.x),
          Random.Range(bounds.min.y, bounds.max.y),
          Random.Range(bounds.min.z, bounds.max.z)
      );
    }

    GameObject GetWeightedRandomItem () {
      return PrefabsToSpawn[RandomIndexToSpawn[currentSpawnIndex++ % RandomIndexToSpawn.Count]].prefab;
    }

    List<int> MakeRandomListOfItemsToSpawn() {
      List<int> randomIndexes = new List<int>();

      for (int i = 0; i < PrefabsToSpawn.Count; i++) {
        for (int w = 0; w < PrefabsToSpawn[i].weight; w++) {
          randomIndexes.Add(i);
        }
      }

      // Shuffle
      for (int i = 0; i < randomIndexes.Count-1; i++) {
        var r = Random.Range(i, randomIndexes.Count);
        var tmp = randomIndexes[i];
        randomIndexes[i] = randomIndexes[r];
        randomIndexes[r] = tmp;
      }

      return randomIndexes;
    }
}
