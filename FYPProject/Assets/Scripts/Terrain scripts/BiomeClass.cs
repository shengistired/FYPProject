using System.Collections;
using UnityEngine;

[System.Serializable]
public class BiomeClass
{
    [Header("Tree settings")]
    public int treeSpawnRate = 15;
    public int minTreeHeight = 3;
    public int maxTreeHeight = 7;

    [Header("World Generation settings")]
    public bool generateCave = true;
    public int dirtLayerHeight = 5;
    public float surfaceValue = 0.25f;
    public float heightMultiplier = 4f;

    [Header("Noise settings")]
    public float caveFreq = 0.05f;
    public float terrainFreq = 0.1f;
    public Texture2D caveNoiseTexture;

    [Header("Ore settings")]
    public OreClass[] ores;



}
