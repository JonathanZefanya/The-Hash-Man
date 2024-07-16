using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    public PowerUp thisLoot;
    public ItemPickup itemLoot;
    public int lootChance;
}

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loots;

    public PowerUp specialLoot ()
    {
        int cumulativeProbability = 0;
        int currentProbability = Random.Range(0, 100);
        for(int i = 0; i < loots.Length; i++)
        {
            cumulativeProbability += loots[i].lootChance;
            if(currentProbability <= cumulativeProbability)
            {
                return loots[i].thisLoot;
            }
        }
        return null;
    }

    public ItemPickup ItemLoot()
    {
        int cumulativeProbability = 0;
        int currentProbability = Random.Range(0, 100);
        for (int i = 0; i < loots.Length; i++)
        {
            cumulativeProbability += loots[i].lootChance;
            if (currentProbability <= cumulativeProbability)
            {
                return loots[i].itemLoot;
            }
        }
        return null;
    }
}
