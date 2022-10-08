using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grain : Item
{
    void Start()
    {
        basePrice = 5;
        itemName = "Grain";
        Total = 0;
        Unit = 0;
        price = basePrice;
        DisplayItemInfo();
    }
}
