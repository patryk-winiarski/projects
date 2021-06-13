using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour, ISpawns
{
    public ItemPickup_SO[] itemDefinitions;

    private int whichToSpawn = 0;
    private int totalSpawnWeight = 0;
    private int chosen = 0;

    public GameObject itemSpawned { get; set; }
    public Rigidbody itemRigidbody { get; set; }
    public Renderer itemMaterial { get; set; }
    public ItemPickUp itemType { get; set; }

    void Start()
    {
        foreach (ItemPickup_SO ip in itemDefinitions)
        {
            totalSpawnWeight += ip.spawnChanceWeight;
        }
    }

    public void CreateSpawn()
    {
        foreach (ItemPickup_SO ip in itemDefinitions)
        {
            whichToSpawn += ip.spawnChanceWeight;
            if (whichToSpawn >= chosen)
            {
                itemSpawned = Instantiate(ip.itemSpawnObject, transform.position, Quaternion.identity);

                itemRigidbody = itemSpawned.GetComponent<Rigidbody>();
                itemMaterial = itemSpawned.GetComponent<Renderer>();
                itemMaterial.material = ip.itemMaterial;

                itemType = itemSpawned.GetComponent<ItemPickUp>();
                itemType.itemDefinition = ip;
                break;

            }
        }
    }
}
