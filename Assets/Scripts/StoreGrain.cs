using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreGrain : Storefront
{
    public List<int> expirationDate { get; private set; }
    public override void UpdateUnitText()
    {
        unitText.text = unit.ToString();
        summaryUnitText.text = (goldObject.GetComponent<Gold>().DemandCheck(itemsObject.GetComponent<Grain>().demand, unit) * sale).ToString();
    }
    public override void Sell()
    {
        IntSwitch();
        expectedTotalSale = goldObject.GetComponent<Gold>().DemandCheck(itemsObject.GetComponent<Grain>().demand, unit) * itemsObject.GetComponent<Grain>().price * sale;
        UpdateTotalText();
        UpdateUnitText();
    }
    protected override void UpdatePriceText()
    {
        priceText.text = itemsObject.GetComponent<Grain>().price.ToString();
    }
    public void RemoveUnitsFromExpirationDate()
    {
        for (int i = goldObject.GetComponent<Gold>().DemandCheck(itemsObject.GetComponent<Grain>().demand, unit) * sale; i > 0;)
        {
            if (i > expirationDate[1])
            {
                i -= expirationDate[1];
                expirationDate.RemoveRange(1, 2);
            }
            else
            {
                expirationDate[1] -= i;
                i = 0;
            }
        }
    }
    private void DecreaseDateOfExpiration()
    {
        int p = expirationDate.Count;
        for (int i = 1; i < p;)
        {
            expirationDate[i + 1] -= 1;
            if (expirationDate[i + 1] == 0)
            {
                unit -= expirationDate[i];
                expirationDate.RemoveRange(i, i + 1);
                p -= 2;
            }
            else
            {
                i += 2;
            }
        }
    }
    public void AddExpirationDate()
    {
        if (itemsObject.GetComponent<Grain>().unit != 0)
        {
            expirationDate.Add(itemsObject.GetComponent<Grain>().unit);
            expirationDate.Add(40);
        }
    }
    public override void DailyUpdate()
    {
        DecreaseDateOfExpiration();
        UpdatePriceText();
        UpdateUnitText();
        UpdateTotalText();
    }
    private void Awake()
    {
        unit = DataManagment.Instance.grainUnit;
        expirationDate = new List<int>();
        expirationDate = DataManagment.Instance.grainExperationDates;
        if (expirationDate.Count == 0)
        {
            expirationDate.Add(0);
        }
    }
}
