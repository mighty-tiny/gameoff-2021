using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryScript : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();
    public ItemObject hand;

    public void AddItem(ItemObject _item, int _amount)
    {
        bool hasItem = false;
        for (int i = 0; i < Container.Count; i++)
        {

            if (Container[i].item == _item)
            {
                Container[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            Container.Add(new InventorySlot(_item, _amount));
        }
    }

    public void RemoveItem(ItemObject _item, int _amount)
    {
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == _item)
            {
                Container[i].AddAmount(-_amount);
                if (Container[i].amount <= 0)
                {
                    for (int j = 0; j < Container.Count; j++)
                    {
                        if (Container[j].item == _item)
                        {
                            Container.Remove(Container[j]);
                            break;
                        }
                    }
                }
                break;
            }
        }
    }
}

[System.Serializable]
public struct InventorySlot
{
    public ItemObject item;
    public int amount;
    public InventorySlot(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
}