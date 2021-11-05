using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ladybug.ShopItem;

public class Shop : MonoBehaviour
{
    public List<ShopItem> shopItems = new List<ShopItem>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void tryTobuyItem(ShopItem item)
    {
        /* if (item.Price <= player.Coins)
         * {
         *      player.coins -= item.price
         *      BuyItem(item);
         *      //display visual for purchasing and pkay audio
         * }
         * else
         * {
         *      display not enougth coins visual and plau audio;
         * }
         */
    }

    public void BuyItem(ShopItem item)
    {
        
        //give the player the item


    }
}
