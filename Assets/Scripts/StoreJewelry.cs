using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreJewelry : Storefront
{
    public override void UpdateUnitText()
    {
        unitText.text = unit.ToString();
        summaryUnitText.text = (goldObject.GetComponent<Gold>().DemandCheck(itemsObject.GetComponent<Jewelry>().demand, unit) * sale).ToString();
    }
    public override void Sell()
    {
        IntSwitch();
        expectedTotalSale = goldObject.GetComponent<Gold>().DemandCheck(itemsObject.GetComponent<Jewelry>().demand, unit) * itemsObject.GetComponent<Jewelry>().price * sale;
        UpdateTotalText();
        UpdateUnitText();
    }
    protected override void UpdatePriceText()
    {
        priceText.text = itemsObject.GetComponent<Jewelry>().price.ToString();
    }
    private void Awake()
    {
        unit = DataManagment.Instance.jewelryUnit;
    }
}
