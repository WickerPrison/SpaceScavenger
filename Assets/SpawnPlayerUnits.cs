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
        for(int i = 0; i < playerData.squad.Count; i++)
        {
            GameObject unitObject = Instantiate(playerUnitPrefab);
            unitObject.transform.position = spawnPoints[i].position;
            UnitStats stats = unitObject.GetComponent<UnitStats>();
            stats.maxHealth = playerData.squad[i].maxHealth;
            stats.health = stats.maxHealth;
            stats.maxAP = playerData.squad[i].maxAP;
            stats.currentAP = stats.maxAP;
            stats.carryCapacity = playerData.squad[i].carryCapacity;
            PlayerUnit playerUnit = unitObject.GetComponent<PlayerUnit>();
            playerUnit.unitName = playerData.squad[i].unitName;
        }
    }
}
