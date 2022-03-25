using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemWorld : MonoBehaviour
{

    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {

        ItemAssets.Instance.pitemWorld.gameObject.GetComponent<BoxCollider2D>().size = Vector2.one;
        ItemAssets.Instance.pitemWorld.Find("Rigid").GetComponent<BoxCollider2D>().size = Vector2.one;
        Transform transform = Instantiate(ItemAssets.Instance.pitemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    public static ItemWorld DropItem(Vector3 direction,  Vector3 dropPosition, Item item)
    {
        ItemWorld itemWorld = SpawnItemWorld(dropPosition + direction * 2f, item);
        itemWorld.GetComponent<Rigidbody2D>().AddForce(direction * 2f, ForceMode2D.Impulse);

        return itemWorld;
    }

    private Item item;
    private SpriteRenderer spriteRenderer;
    private TextMeshPro textMeshPro;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }
    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        if(item.amount > 1)
        {
            textMeshPro.SetText(item.amount.ToString());
        }
        else
        {
            textMeshPro.SetText("");

        }

    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
