using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputInterpretor : MonoBehaviour
{


    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    float velocityMultiplier = .5f;


    [SerializeField]
    float turnMultiplier = .5f;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vel = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));


        Vector2 look = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * turnMultiplier;

        if (vel.magnitude >1)
        {
            vel.Normalize();
        }

        vel *= velocityMultiplier;
        
        vel = Quaternion.FromToRotation(Vector3.forward, this.transform.forward) * vel;


        playerController.HandleMovement(vel, look);

        if (Input.GetAxis("EnterCameraMode") != 0)
        {
            playerController.EnterSelfieMode();
        }

        if (Input.GetAxis("TakeSelfie") != 0)
        {
            playerController.TakeSelfie();
        }

        if (Input.GetAxis("ExitCameraMode") != 0)
        {
            playerController.ExitSelfieMode();
        }
    }
}
