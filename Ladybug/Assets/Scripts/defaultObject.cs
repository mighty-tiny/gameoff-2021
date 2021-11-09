using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Item/Default")]
public class defaultObject : ItemObject 
{
    public void Awake()
    {
        type = ItemType.Default;
    }
}

