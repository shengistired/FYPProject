using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public Transform pitemWorld;
    public Sprite weaponSprite;
    public Sprite axeSprite;
    public Sprite potionSprite;
    public Sprite foodSprite;
    public Sprite coinSprite;
    public Sprite dirtSprite;
    public Sprite coalSprite;
    public Sprite diamondSprite;
    public Sprite goldSprite;
    public Sprite grassSprite;
    public Sprite ironSprite;
    public Sprite leafSprite;
    public Sprite sandSprite;
    public Sprite snowSprite;
    public Sprite snowLeafSprite;
    public Sprite stoneSprite;
    public Sprite treeLogsSprite;
    public Sprite stoneWallSprite;

}
