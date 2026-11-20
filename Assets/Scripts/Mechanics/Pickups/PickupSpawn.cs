using UnityEngine;

public class PickupSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] pickupPrefabs;
    [SerializeField] private Transform[] spawnPoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int randomIndex = Random.Range(0, pickupPrefabs.Length);

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(pickupPrefabs[randomIndex], spawnPoints[i].position, Quaternion.identity);
        }
    }
}
