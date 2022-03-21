using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private Transform wood;
    private Building buildingToPlace;
    public CustomCursor customCursor;
    private bool build = false;
    public void Build(Building building)
    {
        customCursor.gameObject.SetActive(true);
        customCursor.GetComponent<SpriteRenderer>().sprite = building.GetComponent<SpriteRenderer>().sprite;
        Cursor.visible = false;
        buildingToPlace = building;
        build = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && build == true )
        {
            Vector3 positioning = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10f));
            Instantiate(buildingToPlace, positioning, Quaternion.identity);
            customCursor.gameObject.SetActive(false);
            Cursor.visible = true;
            build = false;
        }
    }
}
