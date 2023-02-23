using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    private Rigidbody rbPlayer;
    private Vector3 direction = Vector3.zero;
    public float speed = 10.0f;
    public GameObject[] spawnPoints = null;

    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        rbPlayer = GetComponent<Rigidbody>();
        spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        float horMove = Input.GetAxis("Horizontal");
        float verMove = Input.GetAxis("Vertical");

        direction = new Vector3(horMove, 0, verMove);
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        //Debug.Log(direction);

        rbPlayer.AddForce(direction * speed, ForceMode.Force);

        if(transform.position.z > 40)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 40);
        }
        else if (transform.position.z < -40)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -40);
        }
    }
    private void Respawn()
    {
        int index = 0;
        while(Physics.CheckBox(spawnPoints[index].transform.position, new Vector3(1.5f, 1.5f, 1.5f)))
        {
            index++;
        }

        rbPlayer.MovePosition(spawnPoints[index].transform.position);
        //Debug.Log("Respawned!");
    }



    private void OnTriggerExit(Collider other)
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (other.CompareTag("Hazard"))
        {
            Respawn();
        }
    }
}
