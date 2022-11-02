using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Bullet
{

    // Start is called before the first frame update
    void Start()
    {

        Destroy(gameObject, this.Lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * this.Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("Entered " + collision.gameObject.name);
    }
}