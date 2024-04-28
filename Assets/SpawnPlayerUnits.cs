using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerUnits : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject playerUnitPrefab;
    [SerializeField] PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < playerData.unitNum; i++)
        {
            /*
            GameObject unitObject = Instantiate(playerUnitPrefab);
            unitObject.transform.position = spawnPoints[i].position;
            UnitStats stats = unitObject.GetComponent<UnitStats>();
            stats.maxHealth = playerData.maxHealth[i];
            stats.health = stats.maxHealth;
            stats.maxAP = playerData.maxAP[i];
            stats.currentAP = stats.maxAP;
            stats.carryCapacity = playerData.carryCapacity[i];
            PlayerUnit playerUnit = unitObject.GetComponent<PlayerUnit>();
            playerUnit.unitName = playerData.names[i];
            */
        }
    }
}
