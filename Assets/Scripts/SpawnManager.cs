using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnManager : NetworkBehaviour
{
    public GameObject[] lilyPadObjs = null;
    // Start is called before the first frame update
    public override void OnStartServer()
    {
        InvokeRepeating("SpawnLilyPad", 2.0f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnLilyPad()
    {
        foreach(GameObject lilyPad in lilyPadObjs)
        {
            GameObject tempLilyPad = Instantiate(lilyPad);
            Mirror.NetworkServer.Spawn(tempLilyPad);
        }
       
    }
}
