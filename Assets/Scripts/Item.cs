using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{
    protected int basePrice;
    // ENCAPSULATION
    public int price { get; protected set; }
    public int unit { get; protected set; }
    public int total { get; protected set; }
    public float eventPriceMod;
    public float eventDemandMod;
    protected float priceModifier;
    protected float springMin;
    protected float springMax;
    protected float summerMin;
    protected float summerMax;
    protected float fallMin;
    protected float fallMax;
    protected float winterMin;
    protected float winterMax;
    public int size { get; protected set; }
    public int demand { get; protected set; }
    protected string itemName;
    [SerializeField] private TextMeshProUGUI baseString;
    [SerializeField] private TextMeshProUGUI nameString;
    [SerializeField] private TextMeshProUGUI priceString;
    [SerializeField] private TextMeshProUGUI unitString;
    [SerializeField] private TextMeshProUGUI totalString;
    [SerializeField] private GameObject calendarObject;
    protected void DisplayItemInfo()
    {
        baseString.text = basePrice.ToString();
        nameString.text = itemName;
        priceString.text = price.ToString();
        totalString.text = total.ToString();
    }
    protected float RandomModifier()
    {
        if (calendarObject.GetComponent<Calendar>().season == 0)
        {
            float i =
            Random.Range(springMin, springMax);
            return i;
        }
        else if (calendarObject.GetComponent<Calendar>().season == 1)
        {
            float i =
            Random.Range(summerMin, summerMax);
            return i;
        }
        else if (calendarObject.GetComponent<Calendar>().season == 2)
        {
            float i =
            Random.Range(fallMin, fallMax);
            return i;
        }
        else
        {
            float i =
            Random.Range(winterMin, winterMax);
            return i;
        }
    }
    public void UnitChangeUp()
    {
        unit += 1;
        CalculateTotal();
        UnitAndTotalUpdate();
    }
    public void UnitChangeDown()
    {
        if(unit > 0)
        {
            unit -= 1;
        }
        CalculateTotal();
        UnitAndTotalUpdate();
    }
    private void UnitAndTotalUpdate()
    {
        unitString.text = unit.ToString();
        totalString.text = total.ToString();
    }
    protected void CalculateTotal()
    {
        total = price * unit;
    }
    public virtual void NextDayPrices()
    {
        priceModifier = RandomModifier();
        price = Mathf.RoundToInt(basePrice * priceModifier * eventPriceMod);
        demand = Mathf.RoundToInt(50 * priceModifier * eventDemandMod);
        CalculateTotal();
        DisplayItemInfo();
    }
}