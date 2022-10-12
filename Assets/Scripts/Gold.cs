using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gold : MonoBehaviour
{
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
    [SerializeField] private GameObject insufficientFundsObject;
    [SerializeField] private GameObject itemsObject;
    [SerializeField] private GameObject storefrontObject;
    private static int priceLevel1 = 0;
    private static int priceLevel2 = 1000;
    private static int priceLevel3 = 10000;
    private static int upkeepLevel1 = 10;
    private static int upkeepLevel2 = 100;
    private static int upkeepLevel3 = 250;
    public int demandLevel { get; private set; }
    public int storageLevel { get; private set; }
    private int storeLevel;
    private int warehouseLevel;
    public int gold;
    IEnumerator RemoveInsufficientFundsText()
    {
        yield return new WaitForSeconds(3);
        insufficientFundsObject.SetActive(false);
    }
    public void UpdateLevelText()
    {
        storeLevelText.text = "Store Lv:" + (storeLevel + 1);
        warehouseLevelText.text = "Warehouse LV:" + (warehouseLevel + 1);
        UpdateDemandAndStorage();
        UpdateUpgradeAndDowngradeCostsAndUpkeep();
    }
    private void UpdateDemandAndStorage()
    {
        demandLevel = storeLevel + 1;
        storeDemandLevelText.text = "Demand: " + demandLevel + "x";
        warehouseStorageText.text = storageLevel.ToString() + "/" + ((warehouseLevel + 1) * 100);
    }
    private void UpdateUpgradeAndDowngradeCostsAndUpkeep()
    {
        if (storeLevel == 0)
        {
            storeUpkeepCostText.text = "Upkeep Daily Cost:" + upkeepLevel1;
            storefrontPriceText.text = "Cost: " + priceLevel2;
            storefrontDowngradeProfitText.text = "Profit:" + priceLevel1;
        }
        else if (storeLevel == 1)
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
    public void UpgradeStoreLevel()
    {
        if (storeLevel == 0 && gold >= priceLevel2)
        {
            storeLevel += 1;
            gold -= priceLevel2;
            UpdateLevelText();
        }
        else if (storeLevel == 1 && gold >= priceLevel3)
        {
            storeLevel += 1;
            gold -= priceLevel3;
            UpdateLevelText();
        }
        else
        {
            insufficientFundsObject.SetActive(true);
            StartCoroutine(RemoveInsufficientFundsText());
        }
    }
    public void UpgradeWarehouseLevel()
    {
        if (warehouseLevel == 0 && gold >= priceLevel2)
        {
            warehouseLevel += 1;
            gold -= priceLevel2;
            UpdateLevelText();
        }
        else if (warehouseLevel == 1 && gold >= priceLevel3)
        {
            warehouseLevel += 1;
            gold -= priceLevel3;
            UpdateLevelText();
        }
        else
        {
            insufficientFundsObject.SetActive(true);
            StartCoroutine(RemoveInsufficientFundsText());
        }
    }
    public void DowngradeStoreLevel()
    {
        if (storeLevel == 1)
        {
            storeLevel -= 1;
            gold += priceLevel2;
            UpdateLevelText();
        }
        else if (storeLevel == 2)
        {
            storeLevel -= 1;
            gold += priceLevel3;
            UpdateLevelText();
        }
        else
        {
            insufficientFundsObject.SetActive(true);
            StartCoroutine(RemoveInsufficientFundsText());
        }
    }
    public void DowngradeWarehouseLevel()
    {
        if (warehouseLevel == 1)
        {
            warehouseLevel -= 1;
            gold += priceLevel2;
            UpdateLevelText();
        }
        else if (warehouseLevel == 2)
        {
            warehouseLevel += 1;
            gold += priceLevel3;
            UpdateLevelText();
        }
        else
        {
            insufficientFundsObject.SetActive(true);
            StartCoroutine(RemoveInsufficientFundsText());
        }
    }
    public void BuyItems()
    {
        if (itemsObject.GetComponent<Firewood>().total + itemsObject.GetComponent<Furniture>().total + itemsObject.GetComponent<Jewelry>().total + itemsObject.GetComponent<Grain>().total + itemsObject.GetComponent<Flowers>().total <= gold)
        {
            gold -= (itemsObject.GetComponent<Firewood>().total + itemsObject.GetComponent<Furniture>().total + itemsObject.GetComponent<Jewelry>().total + itemsObject.GetComponent<Grain>().total + itemsObject.GetComponent<Flowers>().total);
            storefrontObject.GetComponent<StoreFirewood>().unit += itemsObject.GetComponent<Firewood>().unit;
            while (itemsObject.GetComponent<Firewood>().unit != 0)
            {
                itemsObject.GetComponent<Firewood>().UnitChangeDown();
            }
            storefrontObject.GetComponent<StoreFurniture>().unit += itemsObject.GetComponent<Furniture>().unit;
            while (itemsObject.GetComponent<Furniture>().unit != 0)
            {
                itemsObject.GetComponent<Furniture>().UnitChangeDown();
            }
            storefrontObject.GetComponent<StoreJewelry>().unit += itemsObject.GetComponent<Jewelry>().unit;
            while (itemsObject.GetComponent<Jewelry>().unit != 0)
            {
                itemsObject.GetComponent<Jewelry>().UnitChangeDown();
            }
            storefrontObject.GetComponent<StoreGrain>().unit += itemsObject.GetComponent<Grain>().unit;
            while (itemsObject.GetComponent<Grain>().unit != 0)
            {
                itemsObject.GetComponent<Grain>().UnitChangeDown();
            }
            storefrontObject.GetComponent<StoreFlowers>().unit += itemsObject.GetComponent<Flowers>().unit;
            while (itemsObject.GetComponent<Flowers>().unit != 0)
            {
                itemsObject.GetComponent<Flowers>().UnitChangeDown();
            }
            storefrontObject.GetComponent<StoreFirewood>().UpdateUnitText();
            storefrontObject.GetComponent<StoreFurniture>().UpdateUnitText();
            storefrontObject.GetComponent<StoreJewelry>().UpdateUnitText();
            storefrontObject.GetComponent<StoreGrain>().UpdateUnitText();
            storefrontObject.GetComponent<StoreFlowers>().UpdateUnitText();
        }
        else
        {
            insufficientFundsObject.SetActive(true);
            StartCoroutine(RemoveInsufficientFundsText());
        }
    }
    private int DemandCheck(int d, int u)
    {
        if (demandLevel * d > u)
        {
            return u;
        }
        else
        {
            return demandLevel * d;
        }
    }
    public void ExpectedTotalSales()
    {
        storefrontObject.GetComponent<StoreFirewood>().expectedTotalSale = DemandCheck(itemsObject.GetComponent<Firewood>().demand, storefrontObject.GetComponent<StoreFirewood>().unit) * itemsObject.GetComponent<Firewood>().price * storefrontObject.GetComponent<StoreFirewood>().sale;
        storefrontObject.GetComponent<StoreFurniture>().expectedTotalSale = DemandCheck(itemsObject.GetComponent<Furniture>().demand, storefrontObject.GetComponent<StoreFurniture>().unit) * itemsObject.GetComponent<Furniture>().price * storefrontObject.GetComponent<StoreFurniture>().sale;
        storefrontObject.GetComponent<StoreJewelry>().expectedTotalSale = DemandCheck(itemsObject.GetComponent<Jewelry>().demand, storefrontObject.GetComponent<StoreJewelry>().unit) * itemsObject.GetComponent<Jewelry>().price * storefrontObject.GetComponent<StoreJewelry>().sale;
        storefrontObject.GetComponent<StoreGrain>().expectedTotalSale = DemandCheck(itemsObject.GetComponent<Grain>().demand, storefrontObject.GetComponent<StoreGrain>().unit) * itemsObject.GetComponent<Grain>().price * storefrontObject.GetComponent<StoreGrain>().sale;
        storefrontObject.GetComponent<StoreFlowers>().expectedTotalSale = DemandCheck(itemsObject.GetComponent<Flowers>().demand, storefrontObject.GetComponent<StoreFlowers>().unit) * itemsObject.GetComponent<Flowers>().price * storefrontObject.GetComponent<StoreFlowers>().sale;
        storefrontObject.GetComponent<StoreFirewood>().UpdateTotalText();
        storefrontObject.GetComponent<StoreFurniture>().UpdateTotalText();
        storefrontObject.GetComponent<StoreJewelry>().UpdateTotalText();
        storefrontObject.GetComponent<StoreGrain>().UpdateTotalText();
        storefrontObject.GetComponent<StoreFlowers>().UpdateTotalText();
    }
    public void UpdatePriceText()
    {
        storefrontObject.GetComponent<StoreFirewood>().priceText.text = itemsObject.GetComponent<Firewood>().price.ToString();
        storefrontObject.GetComponent<StoreFurniture>().priceText.text = itemsObject.GetComponent<Furniture>().price.ToString();
        storefrontObject.GetComponent<StoreJewelry>().priceText.text = itemsObject.GetComponent<Jewelry>().price.ToString();
        storefrontObject.GetComponent<StoreGrain>().priceText.text = itemsObject.GetComponent<Grain>().price.ToString();
        storefrontObject.GetComponent<StoreFlowers>().priceText.text = itemsObject.GetComponent<Flowers>().price.ToString();
    }
}
