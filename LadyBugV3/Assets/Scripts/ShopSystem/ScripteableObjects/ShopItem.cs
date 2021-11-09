using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop Item")]
public class ShopItem : ScriptableObject
{
    [SerializeField] private string itemName = "name";
    [SerializeField] private int price = 100;
    [SerializeField] private Sprite itemShopSprite; 
    [SerializeField] private ItemObject reward;
    [SerializeField] private Rarity itemRarity; //this should go on the reward class, but i dont know how we handle items in the inventory yet

    public string ItemName => itemName;
    public Sprite ItemShopSprite => itemShopSprite;

    public int Price => price;
    public ItemObject Reward => reward;

    public Rarity ItemRarity => itemRarity;



}

