using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct WeightedDesiredItem
{
    //Tony changed this
    public ItemClass item;
    public int weight;
}

[CreateAssetMenu(fileName = "NewCustomerArchetype", menuName = "ScriptableObjects/Customer/Archetype")]
public class CustomerArchetypeSO : ScriptableObject
{
    public string archetypeName;
    public List<WeightedDesiredItem> preferredItemPool;
    public List<WeightedDesiredItem> fallbackItemPool;
    public int minutesWillWait;
    public float chanceToSpawn;
}
