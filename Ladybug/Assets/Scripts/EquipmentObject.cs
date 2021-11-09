using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Item/Eqiupment")]
public class EquipmentObject : ItemObject
{

    public float atkBonus;
    public float defBonus;

    public void Awake()
    {
        type = ItemType.Equipment;
    }
}
