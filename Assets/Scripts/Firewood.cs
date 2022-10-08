using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firewood : Item
{
    void Start()
    {
        basePrice = 5;
        itemName = "Firewood";
        Total = 0;
        Unit = 0;
        price = basePrice;
        DisplayItemInfo();
    }
}
