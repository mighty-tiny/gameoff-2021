using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Rarity", menuName = "Inventory System/Item/Rarity")]
public class Rarity : ScriptableObject
{

    [SerializeField] private Color rarityColor;
    [SerializeField] private string rarityName;

    public Color RarityColor => rarityColor;
    public string RarityName => rarityName;
}
