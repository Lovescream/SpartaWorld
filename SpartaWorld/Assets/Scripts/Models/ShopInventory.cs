using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : Inventory {

    public ShopInventory() {
        foreach (ItemData data in Main.Data.Items.Values) {
            Add(new Item(data));
        }
    }

}