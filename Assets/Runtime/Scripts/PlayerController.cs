using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AirPlaneMovement), typeof(BombLaucher))]
public class PlayerController : MonoBehaviour
{
    private AirPlaneMovement airPlaneMovement;
    private BombLaucher projectileLaucher;

    private void Awake()
    {
        airPlaneMovement = GetComponent<AirPlaneMovement>();
        projectileLaucher = GetComponent<BombLaucher>();
    }

    private void Update()
    {
        var frameInput = new Vector2(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal"));
        airPlaneMovement.SetSteerInput(frameInput);
        
        if(Input.GetKey(KeyCode.Space))
        {
            projectileLaucher.TryShoot();
        }
    }
}
