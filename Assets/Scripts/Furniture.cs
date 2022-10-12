using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : Item
{
    void Start()
    {
        basePrice = 50;
        itemName = "Furniture";
        total = 0;
        unit = 0;
        size = 3;
        springMin = 1.1f;
        springMax = 1.2f;
        summerMin = 1f;
        summerMax = 1.1f;
        fallMin = .9f;
        fallMax = 1.0f;
        winterMin = 1.0f;
        winterMax = 1.1f;
        priceModifier = RandomModifier();
        demand = Mathf.RoundToInt(10 * priceModifier);
        price = Mathf.RoundToInt(basePrice * priceModifier);
        DisplayItemInfo();
    }
}
