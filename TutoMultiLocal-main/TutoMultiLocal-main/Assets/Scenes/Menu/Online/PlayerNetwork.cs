using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    private void Start()
    {
        gameObject.transform.position = new Vector3(25, 25, 25);
    }



    NetworkVariable<int> randomNumber = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public override void OnNetworkSpawn()
    {
        randomNumber.OnValueChanged += (int previousValue, int newValue) =>
        {
            Debug.Log(OwnerClientId + " ; " + randomNumber.Value);
        };
    }
    void Update()
    {
    
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.T))
        {
            randomNumber.Value = Random.Range(0, 100);
        }
    
        Vector3 moveDir = new Vector3(0, 0,0);

        if (Input.GetKey(KeyCode.W)) moveDir = Vector2.up;
        if (Input.GetKey(KeyCode.S)) moveDir = Vector2.down;
        if (Input.GetKey(KeyCode.A)) moveDir = Vector2.left;
        if (Input.GetKey(KeyCode.D)) moveDir = Vector2.right;

        float moveSpeed = 3f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}
