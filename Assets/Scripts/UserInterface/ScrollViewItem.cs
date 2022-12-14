using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//
//C292 Main Project - Nebula Guards Partial Code from How to: Dynamic Scroll View in Unity used in UI Development : https://www.youtube.com/watch?v=Q-G-W93jhYc
//
public class ScrollViewItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image childImage;
    [field: SerializeField] public int IndexInList { get; set; }
    [field: SerializeField] public string ShopName { get; set; }
    public void ChangeImage(Sprite image)
    {
        childImage.sprite = image;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log(this.IndexInList);
        if(ShopName == "ShipStore")
        {
            StoreSelectEvent.InvokeSelectionChanged(IndexInList);
        }
        if (ShopName == "UpgradeStore")
        {
            UpgradeSelectEvent.InvokeSelectionChanged(IndexInList);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       this.gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.gameObject.transform.localScale = Vector3.one;
    }
}
