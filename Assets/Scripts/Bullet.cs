using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [field: SerializeField] public float Speed { get; set; }
    [field: SerializeField] public float Lifetime { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        if(Speed == null)
        {
            this.Speed = 10f;
        }
        if(Lifetime == null)
        {
            this.Lifetime = 4f;
        }
        Destroy(gameObject, Lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * Speed;
    }
}
