using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grain : Item
{
    void Start()
    {
        basePrice = 5;
        itemName = "Grain";
        total = 0;
        unit = 0;
        size = 2;
        springMin = .9f;
        springMax = 1.1f;
        summerMin = .8f;
        summerMax = .9f;
        fallMin = .9f;
        fallMax = 1.1f;
        winterMin = 1.1f;
        winterMax = 1.3f;
        priceModifier = RandomModifier();
        demand = Mathf.RoundToInt(50 * priceModifier);
        price = Mathf.RoundToInt(basePrice * priceModifier);
        DisplayItemInfo();
    }
}
