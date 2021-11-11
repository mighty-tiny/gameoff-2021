using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop Item")]
public class ShopItem : ScriptableObject
{
    
    [SerializeField] private string itemName = "name";
    [SerializeField] private int price = 100;
    [SerializeField] private int quantity = 1;
    [SerializeField] private Sprite itemShopSprite; 
    [SerializeField] private ItemObject reward;
    [SerializeField] private Rarity itemRarity; //this should go on the reward class, but i dont know how we handle items in the inventory yet
    [SerializeField] private int buyAmount;
    [SerializeField] private bool infiniteStock;
    [SerializeField] private int avaliableStock; //only applies if infiniteStock is false
    [SerializeField] GameObject shopPrefab;

    public string ItemName => itemName;

    public int Price => price;

    public Sprite ItemShopSprite => itemShopSprite;

    public int Quantity => quantity;
    public ItemObject Reward => reward;

    public Rarity ItemRarity => itemRarity;

    public int BuyAmount => buyAmount;
    public bool InfiniteStock => infiniteStock;

    public int AvaliableStock => avaliableStock;

    public GameObject ShopPrefab => shopPrefab;
}

