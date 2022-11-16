using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// 
//C292 Main Project - Nebula Guards Partial Code from How to: Dynamic Scroll View in Unity used in UI Development : https://www.youtube.com/watch?v=Q-G-W93jhYc
//
public class DynamicScrollView : MonoBehaviour
{
    [SerializeField] private Transform scrollViewContent;

    [SerializeField] private GameObject prefab;

    [SerializeField] private List<Sprite> Items;

    [SerializeField] private Sprite DefaultSprite;

    [SerializeField] private string ShopName;


    private void Start()
    {
        DestroyOldIcons();
    }
    private void GenerateIcons()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            //Create New UI prefab, then from the list of avaliable sprites, change the icon to that
            GameObject newSpaceshipSprite = Instantiate(prefab, scrollViewContent);
            if (newSpaceshipSprite.TryGetComponent<ScrollViewItem>(out ScrollViewItem spaceshipItem))
            {
                spaceshipItem.ChangeImage(Items[i]);
                spaceshipItem.IndexInList = i;
                spaceshipItem.ShopName = this.ShopName;
            }
        }
    }

    public void LoadAvailableStoreItems(List<GameObject> Entities)
    {
        //Debug.Log("Clearing old store");
        Items.Clear();
        DestroyOldIcons();
        foreach (GameObject ent in Entities)
        {
            if (ent.GetComponent<Entity>())
            {
                Entity thisEnt = ent.GetComponent<Entity>();
                if (thisEnt.MySprite != null)
                { 
                    if(thisEnt.MySprite != null)
                    {
                        Items.Add(thisEnt.MySprite);
                    }
                }
            }
            else
            {
                Items.Add(DefaultSprite);
            }

        }
        GenerateIcons();
    }

    public void LoadAvailableUpgradeItems(List<EntityPart> Parts)
    {
        //Debug.Log("Clearing old upgrade store");
        Items.Clear();
        DestroyOldIcons();
        foreach (EntityPart part in Parts)
        {
            if(part.PartIcon != null)
            {

                Items.Add(part.PartIcon);
            }
            else
            {
                Items.Add(DefaultSprite);
            }
        }
        GenerateIcons();
    }

    void DestroyOldIcons()
    {
        int childs = transform.childCount;
        for (int i = childs - 1; i > 0; i--)
        {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }
    }
}
