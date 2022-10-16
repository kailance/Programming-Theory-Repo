using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class DataManagment : MonoBehaviour
{
    public static DataManagment Instance;
    public string fileName;
    [SerializeField] private int saveFileNumber;
    public int firewoodUnit;
    public int furnitureUnit;
    public int jewelryUnit;
    public int grainUnit;
    public int flowersUnit;
    public List<int> flowersExperationDates;
    public List<int> grainExperationDates;
    public int gold;
    public int date;
    public int season;
    public int storeLevel;
    public int warehouseLevel;
    public int forestFireDate;
    public bool loadGame;
    [SerializeField] private List<GameObject> slotItems;
    [SerializeField] private GameObject nameObject;
    private void Awake()
    {
        //Insures only one of the object is caried over. Also refered to as a singleton.
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        //makes object aviable in any class and not destroy the object when a new scene is loaded.
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void NewGame()
    {
        loadGame = false;
        LoadSlotInfo();
    }
    public void LoadGame()
    {
        loadGame = true;
        LoadSlotInfo();
    }
    public void FileSelect1()
    {
        saveFileNumber = 1;
        LoadFile();
    }
    public void FileSelect2()
    {
        saveFileNumber = 2;
        LoadFile();
    }
    public void FileSelect3()
    {
        saveFileNumber = 3;
        LoadFile();
    }
    public void NameSelected()
    {
        fileName = "Defualt";
        if (nameObject.GetComponent<TextMeshProUGUI>().text != "")
        {
            fileName = nameObject.GetComponent<TextMeshProUGUI>().text;
        }
        SceneManager.LoadScene(1);

    }
    public void StartGame()
    {
            if (loadGame && fileName != "")
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                DefualtValues();
            }
    }
    private void LoadSlotInfo()
    {
        for(int i = 0; i<=2; i++)
        {
            string path = Application.persistentDataPath + "/savefile" + (1+i) + ".json";
            if (File.Exists(path))
            {
                saveFileNumber = i+1;
                LoadFile();
                    slotItems[i * 6].SetActive(false);
                    slotItems[(i * 6) + 1].SetActive(true);
                    slotItems[(i * 6) + 2].SetActive(true);
                    slotItems[(i * 6) + 3].SetActive(true);
                    slotItems[(i * 6) + 4].SetActive(true);
                    slotItems[(i * 6) + 5].SetActive(true);
                    slotItems[(i * 6) + 1].GetComponent<TextMeshProUGUI>().text = fileName;
                    slotItems[(i * 6) + 2].GetComponent<TextMeshProUGUI>().text = Season() + " " + date.ToString();
                    slotItems[(i * 6) + 3].GetComponent<TextMeshProUGUI>().text = "Gold: " + gold.ToString();
                    slotItems[(i * 6) + 4].GetComponent<TextMeshProUGUI>().text = "Store: LV" + (1+storeLevel);
                    slotItems[(i * 6) + 5].GetComponent<TextMeshProUGUI>().text = "Warehouse: LV" + (1+warehouseLevel);
            }
        }
    }
    private void DefualtValues()
    {
        firewoodUnit = 0;
        furnitureUnit = 0;
        jewelryUnit = 0;
        grainUnit = 0;
        flowersUnit = 0;
        gold = 1000;
        date = 1;
        season = 0;
        storeLevel = 0;
        warehouseLevel = 0;
        if (flowersExperationDates.Equals(null) != true)
        {
            flowersExperationDates.Clear();
        }
        if (grainExperationDates.Equals(null) != true)
        {
            grainExperationDates.Clear();
        }
    }
    public string Season()
    {
        if (season == 0)
        {
            return "Spring";
        }
        else if (season == 1)
        {
            return "Summer";
        }
        else if (season == 2)
        {
            return "Fall";
        }
        else
        {
            return "Winter";
        }
    }
    public void SaveFile()
    {
        SaveData data = new SaveData();
        data.fileName = fileName;
        data.firewoodUnit = firewoodUnit;
        data.furnitureUnit = furnitureUnit;
        data.jewelryUnit = jewelryUnit;
        data.grainUnit = grainUnit;
        data.flowersUnit = flowersUnit;
        data.flowersExperationDates = flowersExperationDates;
        data.grainExperationDates = grainExperationDates;
        data.gold = gold;
        data.date = date;
        data.season = season;
        data.storeLevel = storeLevel;
        data.warehouseLevel = warehouseLevel;
        data.forestFireDate = forestFireDate;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile" + saveFileNumber +".json", json);
    }
    public void LoadFile()
    {
        string path = Application.persistentDataPath + "/savefile" + saveFileNumber + ".json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            fileName = data.fileName;
            firewoodUnit = data.firewoodUnit;
            furnitureUnit = data.furnitureUnit;
            jewelryUnit = data.jewelryUnit;
            grainUnit = data.grainUnit;
            flowersUnit = data.flowersUnit;
            if (flowersExperationDates.Equals(null) != true)
            {
                flowersExperationDates.Clear();
            }
            if (grainExperationDates.Equals(null) != true)
            {
                grainExperationDates.Clear();
            }
            flowersExperationDates = data.flowersExperationDates;
            grainExperationDates = data.grainExperationDates;
            gold = data.gold;
            date = data.date;
            season = data.season;
            storeLevel = data.storeLevel;
            warehouseLevel = data.warehouseLevel;
            forestFireDate = data.forestFireDate;
        }
    }
    [System.Serializable]
    class SaveData
    {
        public string fileName;
        public int firewoodUnit;
        public int furnitureUnit;
        public int jewelryUnit;
        public int grainUnit;
        public int flowersUnit;
        public List<int> flowersExperationDates;
        public List<int> grainExperationDates;
        public int gold;
        public int date;
        public int season;
        public int storeLevel;
        public int warehouseLevel;
        public int forestFireDate;
    }
}
