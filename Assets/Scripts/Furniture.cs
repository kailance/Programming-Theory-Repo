using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : Item
{
    void Start()
    {
        basePrice = 50;
        itemName = "Furniture";
        Total = 0;
        Unit = 0;
        price = basePrice;
        DisplayItemInfo();
    }
}
