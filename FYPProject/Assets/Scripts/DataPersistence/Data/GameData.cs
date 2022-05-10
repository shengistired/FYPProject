using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int portalEntered;
    public int hour;
    public int min;
    public int second;
    public int allSecond;
    public float timer;
    public string playerClass;
    public string biome;
    public string mode; //??
    public int life;
    public string difficulty;
    public string worldSizeSet;
    public List<Item> equipmentItems;
    public List<Item> inventoryItems;
    public Vector3 tilesPosition;
    public Vector3 playerPosition;
    public int playerlevel;
    public int currentExp;
    public int expNeededToNextLevel;
    public int statPoints;
    public int skillPoint;
    public int str;
    public int dex;
    public int intelligence;
    public int luck;
    public float currentHP;
    public float currentMana;
    public float currentFood;
    public float currentStamina;
    public float maxHpBar;
    public float maxManaBar;
    public float maxStamina;
    public float healthRegen;
    public float manaRegen;
    public float staminaRegen;
    public float hungerDegen;
    public int healthRegenSkillValue;
    public int staminaRegenSkillValue;
    public int manaRegenSkillValue;
    public int hungerResistSkillValue;
    public int biomeResistSkillValue;
    public int score;
    public bool gameCleared;
    public bool[] crafted;
    public SerializeDictionary<string, Vector3> tilePosition;
    public SerializeDictionary<string, Vector3> treePosition;
    public SerializeDictionary<string, string> treeTypeDictionary;
    public SerializeDictionary<string, string> allTilesTypeDictionary;
    public bool worldGenerated;
    public bool worldRegenerated;


    public GameData()
    {
        portalEntered = 0;
        hour = 0;
        min = 0;
        second = 0;
        allSecond = 0;
        timer = 0.5f;
        playerClass = "";
        biome = "";
        mode = ""; //??
        life = -1;
        difficulty = "";
        worldSizeSet = "";
        equipmentItems = new List<Item>();
        inventoryItems = new List<Item>();
        playerlevel = 1;
        currentExp = 0;
        expNeededToNextLevel = playerlevel * 100;
        statPoints = 5;
        skillPoint = 0;
        str = 10;
        dex = 10;
        intelligence = 10;
        luck = 10;
        maxHpBar = 120;
        maxStamina = 120;
        maxManaBar = 120;
        currentHP = maxHpBar;
        currentFood = 200f;
        currentMana = maxManaBar;
        currentStamina = maxStamina;
        healthRegen = 0.5f;
        manaRegen = 1f;
        staminaRegen = 1f;
        hungerDegen = 1f;
        healthRegenSkillValue = 0;
        staminaRegenSkillValue = 0;
        hungerResistSkillValue = 0;
        biomeResistSkillValue = 0;
        score = 0;
        gameCleared = false;
        crafted = new bool[6] { false, false, false, false, false, false };
        tilePosition = new SerializeDictionary<string, Vector3>();
        treePosition = new SerializeDictionary<string, Vector3>();
        treeTypeDictionary = new SerializeDictionary<string, string>();
        allTilesTypeDictionary = new SerializeDictionary<string, string>();
        worldGenerated = false;
        worldRegenerated = false;

        //equipment = null;

    }
}
