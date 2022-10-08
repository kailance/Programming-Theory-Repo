using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jewelry : Item
{
    void Start()
    {
        basePrice = 500;
        itemName = "Jewelry";
        Total = 0;
        Unit = 0;
        price = basePrice;
        DisplayItemInfo();
    }
}
