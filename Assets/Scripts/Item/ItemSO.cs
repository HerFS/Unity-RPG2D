using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;

    public AttributeToChange attributesToChange = new AttributeToChange();
    public int amountToChangeAttribute;

    public bool UseItem()
    {
        if (statToChange == StatToChange.health)
        {
            PlayerHealth playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
            if (playerHealth.health == playerHealth.maxHealth)
            {
                return false;
            } else
            {
                playerHealth.health += amountToChangeStat;
                return true;
            }
        }

        return false;
    }

    public enum StatToChange
    {
        none,
        health,
        xp
    };

    public enum AttributeToChange
    {
        none,
        strength,
        defense
    };
}
