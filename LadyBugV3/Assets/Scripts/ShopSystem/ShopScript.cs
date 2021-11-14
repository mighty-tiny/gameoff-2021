using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shop", menuName = "Shop System/Shop")]
public class ShopScript : ScriptableObject
{
    [SerializeField] List<ShopItem> staticShopItems = new List<ShopItem>();
    [HideInInspector] public List<ShopSlot> shopItems = new List<ShopSlot>();
    [SerializeField] InventoryScript inventory;

   public void Init() //replace this with the load function when this is on the same branch as the save load system. Make sure to make the necesary changes (like only adding items with a stock of/above 1)
   {
        foreach (var item in staticShopItems)
        {
            shopItems.Add(new ShopSlot(item, this));
        }
   }

    public void BuyItem(ref ShopSlot item)
    {
        if (item.stock <= 0) return;

        item.DecreaseStock(1);

        if (item.stock <= 1) shopItems.Remove(item);

        inventory.AddItem(item.item.Reward, item.item.Quantity);

        //decrease the players coin value by item.item.Price
    }
}

public class ShopSlot
{
    private ShopScript owner;
    public ShopItem item;
    public int stock;
    public ShopSlot(ShopItem _item, ShopScript _owner)
    {
        item = _item;
        owner = _owner;
        stock = _item.InfiniteStock ? int.MaxValue : item.AvaliableStock;
    }
    public void DecreaseStock(int value)
    {
        stock -= item.InfiniteStock ? 0 : value;
    }
}
