using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreFlowers : Storefront
{
    public override void UpdateUnitText()
    {
        unitText.text = unit.ToString();
        summaryUnitText.text = (goldObject.GetComponent<Gold>().DemandCheck(itemsObject.GetComponent<Flowers>().demand, unit) * sale).ToString();
    }
    public override void Sell()
    {
        IntSwitch();
        expectedTotalSale = goldObject.GetComponent<Gold>().DemandCheck(itemsObject.GetComponent<Flowers>().demand, unit) * itemsObject.GetComponent<Flowers>().price * sale;
        UpdateTotalText();
        UpdateUnitText();
    }
    protected override void UpdatePriceText()
    {
        priceText.text = itemsObject.GetComponent<Flowers>().price.ToString();
    }
    private void Awake()
    {
        unit = DataManagment.Instance.flowersUnit;
    }
}
