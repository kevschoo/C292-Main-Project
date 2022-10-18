using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class SelectableObj : MonoBehaviour
{
    [SerializeField] bool IsMouseOver = false;
    [SerializeField] Collider2D SelectableZone;
    void Start()
    {
       
        if (this.gameObject.GetComponent<Collider2D>())
        {
            SelectableZone = this.gameObject.GetComponent<Collider2D>();
        }
        else
        {
            SelectableZone = this.gameObject.AddComponent<CircleCollider2D>();
            SelectableZone.isTrigger = true;
        }
    }

    private void OnMouseOver()
    {
        IsMouseOver = true;
        //call event hoverUI(gameobject) 
    }

    private void OnMouseDown()
    {
       if (IsMouseOver == true)
        {
            Debug.Log(this.gameObject.name);
            //call event(gameobject)
        }
    }

    private void OnMouseExit()
    {
        IsMouseOver = false;
        //call event closeHoverUi()
    }
}
