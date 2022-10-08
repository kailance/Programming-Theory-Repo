using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{
    protected int basePrice { get; set; }
    public int price;
    protected int Unit;
    protected int Total;
    protected string itemName;
    public TextMeshProUGUI baseString;
    public TextMeshProUGUI nameString;
    public TextMeshProUGUI priceString;
    protected void DisplayItemInfo()
    {
        baseString.text = basePrice.ToString();
        nameString.text = itemName;
        priceString.text = price.ToString();
    }
}