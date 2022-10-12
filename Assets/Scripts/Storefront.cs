using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Storefront : MonoBehaviour
{
    public int gold;
    private int storeLevel;
    private int warehouseLevel;
    public int firewoodUnit;
    public int furnitureUnit;
    public int jewelryUnit;
    public int grainUnit;
    public int flowersUnit;
    private int expectedTotalSaleFirewood;
    private int expectedTotalSaleFurniture;
    private int expectedTotalSaleJewelry;
    private int expectedTotalSaleGrain;
    private int expectedTotalSaleFlowers;
    private int firewoodSale;
    private int furnitureSale;
    private int jewelrySale;
    private int grainSale;
    private int flowersSale;
    private static int priceLevel1 = 0;
    private static int priceLevel2 = 1000;
    private static int priceLevel3 = 10000;
    private static int upkeepLevel1 = 10;
    private static int upkeepLevel2 = 100;
    private static int upkeepLevel3 = 250;
    private int demandLevel;
    private int storageLevel;
    [SerializeField] private TextMeshProUGUI firewoodPriceText;
    [SerializeField] private TextMeshProUGUI firewoodUnitText;
    [SerializeField] private TextMeshProUGUI firewoodTotalText;
    [SerializeField] private TextMeshProUGUI furniturePriceText;
    [SerializeField] private TextMeshProUGUI furnitureUnitText;
    [SerializeField] private TextMeshProUGUI furnitureTotalText;
    [SerializeField] private TextMeshProUGUI jewelryPriceText;
    [SerializeField] private TextMeshProUGUI jewelryUnitText;
    [SerializeField] private TextMeshProUGUI jewelryTotalText;
    [SerializeField] private TextMeshProUGUI grainPriceText;
    [SerializeField] private TextMeshProUGUI grainUnitText;
    [SerializeField] private TextMeshProUGUI grainTotalText;
    [SerializeField] private TextMeshProUGUI flowersPriceText;
    [SerializeField] private TextMeshProUGUI flowersUnitText;
    [SerializeField] private TextMeshProUGUI flowersTotalText;
    [SerializeField] private TextMeshProUGUI storefrontPriceText;
    [SerializeField] private TextMeshProUGUI storefrontDowngradeProfitText;
    [SerializeField] private TextMeshProUGUI warehousePriceText;
    [SerializeField] private TextMeshProUGUI warehouseDowngradeProfitText;
    [SerializeField] private TextMeshProUGUI storeLevelText;
    [SerializeField] private TextMeshProUGUI warehouseLevelText;
    [SerializeField] private TextMeshProUGUI storeDemandLevelText;
    [SerializeField] private TextMeshProUGUI warehouseStorageText;
    [SerializeField] private TextMeshProUGUI storeUpkeepCostText;
    [SerializeField] private TextMeshProUGUI warehouseUpkeepCostText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private GameObject firewoodObject;
    [SerializeField] private GameObject furnitureObject;
    [SerializeField] private GameObject jewelryObject;
    [SerializeField] private GameObject grainObject;
    [SerializeField] private GameObject flowersObject;
    void Start()
    {
        UpdatePriceText();
        UpdateUnitText();
        ExpectedTotalSales();
        UpdateLevelText();
    }
    public void UpdatePriceText()
    {
        firewoodPriceText.text = firewoodObject.GetComponent<Firewood>().price.ToString();
        furniturePriceText.text = furnitureObject.GetComponent<Furniture>().price.ToString();
        jewelryPriceText.text = jewelryObject.GetComponent<Jewelry>().price.ToString();
        grainPriceText.text = grainObject.GetComponent<Grain>().price.ToString();
        flowersPriceText.text = flowersObject.GetComponent<Flowers>().price.ToString();
    }
    private void UpdateUnitText()
    {
        firewoodUnitText.text = firewoodUnit.ToString();
        furnitureUnitText.text = furnitureUnit.ToString();
        jewelryUnitText.text = jewelryUnit.ToString();
        grainUnitText.text = grainUnit.ToString();
        flowersUnitText.text = flowersUnit.ToString();
    }
    public void ExpectedTotalSales()
    {
        expectedTotalSaleFirewood = firewoodUnit * firewoodObject.GetComponent<Firewood>().price * firewoodSale;
        expectedTotalSaleFurniture = furnitureUnit * furnitureObject.GetComponent<Furniture>().price * furnitureSale;
        expectedTotalSaleJewelry = jewelryUnit * jewelryObject.GetComponent<Jewelry>().price * jewelrySale;
        expectedTotalSaleGrain = grainUnit * grainObject.GetComponent<Grain>().price * grainSale;
        expectedTotalSaleFlowers = flowersUnit * flowersObject.GetComponent<Flowers>().price * flowersSale;
        UpdateTotalText();
    }
    private void UpdateTotalText()
    {
        firewoodTotalText.text = expectedTotalSaleFirewood.ToString();
        furnitureTotalText.text = expectedTotalSaleFurniture.ToString();
        jewelryTotalText.text = expectedTotalSaleJewelry.ToString();
        grainTotalText.text = expectedTotalSaleGrain.ToString();
        flowersTotalText.text = expectedTotalSaleFlowers.ToString();
    }
    public void SellFirewood()
    {
        if(firewoodSale == 1)
        {
            firewoodSale = 0;
        }
        else
        {
            firewoodSale = 1;
        }
        ExpectedTotalSales();
    }
    public void SellFurniture()
    {
        if(furnitureSale == 1)
        {
            furnitureSale = 0;
        }
        else
        {
            furnitureSale = 1;
        }
        ExpectedTotalSales();
    }
    public void SellJewelry()
    {
        if(jewelrySale == 1)
        {
            jewelrySale = 0;
        }
        else
        {
            jewelrySale = 1;
        }
        ExpectedTotalSales();
    }
    public void SellGrain()
    {
        if(grainSale == 1)
        {
            grainSale = 0;
        }
        else
        {
            grainSale = 1;
        }
        ExpectedTotalSales();
    }
    public void SellFlowers()
    {
        if(flowersSale == 1)
        {
            flowersSale = 0;
        }
        else
        {
            flowersSale = 1;
        }
        ExpectedTotalSales();
    }
    private void UpdateLevelText()
    {
        storeLevelText.text = "Store Lv:" + (storeLevel+1);
        warehouseLevelText.text = "Warehouse LV:" + (warehouseLevel+1);
        UpdateDemandAndStorage();
        UpdateUpgradeAndDowngradeCostsAndUpkeep();
    }
    private void UpdateDemandAndStorage()
    {
        demandLevel = storeLevel + 1;
        storeDemandLevelText.text = "Demand: " + demandLevel + "x";
        storageLevel = (warehouseLevel + 1) * 100;
        warehouseStorageText.text = storageLevel.ToString() + "/" + storageLevel;
    }
    private void UpdateUpgradeAndDowngradeCostsAndUpkeep()
    {
        if(storeLevel == 0)
        {
            storeUpkeepCostText.text = "Upkeep Daily Cost:" + upkeepLevel1;
            storefrontPriceText.text = "Cost: " + priceLevel2;
            storefrontDowngradeProfitText.text = "Profit:" + priceLevel1;
        } 
        else if(storeLevel == 1)
        {
            storeUpkeepCostText.text = "Upkeep Daily Cost:" + upkeepLevel2;
            storefrontPriceText.text = "Cost: " + priceLevel3;
            storefrontDowngradeProfitText.text = "Profit:" + priceLevel2;
        }
        else
        {
            storeUpkeepCostText.text = "Upkeep Daily Cost:" + upkeepLevel3;
            storefrontPriceText.text = "Max";
            storefrontDowngradeProfitText.text = "Profit:" + priceLevel3;
        }
        if (warehouseLevel == 0)
        {
            warehouseUpkeepCostText.text = "Upkeep Daily Cost:" + upkeepLevel1;
            warehousePriceText.text = "Cost: " + priceLevel2;
            warehouseDowngradeProfitText.text = "Profit:" + priceLevel1;
        }
        else if (warehouseLevel == 1)
        {
            warehouseUpkeepCostText.text = "Upkeep Daily Cost:" + upkeepLevel2;
            warehousePriceText.text = "Cost: " + priceLevel3;
            warehouseDowngradeProfitText.text = "Profit:" + priceLevel2;
        }
        else
        {
            warehouseUpkeepCostText.text = "Upkeep Daily Cost:" + upkeepLevel3;
            warehousePriceText.text = "Max";
            warehouseDowngradeProfitText.text = "Profit:" + priceLevel3;
        }
    }
}
