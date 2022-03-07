using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AirPlaneMovement))]
public class PlayerController : MonoBehaviour
{
    private AirPlaneMovement airPlaneMovement;
    /*private BombLaucher projectileLaucher;*/
    [SerializeField] private MachineGun machineGun;

    private void Awake()
    {
        airPlaneMovement = GetComponent<AirPlaneMovement>();
        /*projectileLaucher = GetComponent<BombLaucher>();*/
        machineGun = GetComponentInChildren<MachineGun>();
    }

    private void Update()
    {
        var frameInput = new Vector2(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal"));
        airPlaneMovement.SetSteerInput(frameInput);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            machineGun.StartShoot();
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            machineGun.StopShoot();
        }
        
        // if(Input.GetKey(KeyCode.Space))
        // {
        //     projectileLaucher.TryShoot();
        // }
    }
}
