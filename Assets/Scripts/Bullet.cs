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

        Destroy(gameObject, Lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * Speed;
    }
}
