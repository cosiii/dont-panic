using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject circle;
    public int itemLength;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < itemLength; i++)
        {
            var spawnedItem = Instantiate (circle, transform.position, Quaternion.identity);
            spawnedItem.name = $"Item {i}";
        }

    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
