using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTile : MonoBehaviour
{
    public GameObject[] randEvents;

    // Start is called before the first frame update
    void Start()
    {
        RandomTileSetting();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomTileSetting()
    {
        int itemGive = Random.Range(0, randEvents.Length);
        GameObject eventss = randEvents[itemGive];

        Instantiate(eventss, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
