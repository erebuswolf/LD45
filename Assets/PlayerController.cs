using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool InSelfieMode;
    bool CameraBusy;

    [SerializeField]
    CharacterController controller;

    [SerializeField]
    CameraMovement cameraMovement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterSelfieMode()
    {
        if(InSelfieMode || CameraBusy)
        {
            return;
        }
        //Start enter selfie mode animation
        CameraBusy = true;
        cameraMovement.MoveToSelfie();
    }

    public void ExitSelfieMode()
    {
        if (!InSelfieMode || CameraBusy)
        {
            return;
        }

        //Start exit selfie mode animation
        CameraBusy = true;
        cameraMovement.MoveToWalking();
    }

    public void TakeSelfie()
    {
        if (!InSelfieMode || CameraBusy)
        {
            return;
        }
        // Start selfie animation.
        CameraBusy = true;
    }

    public void HandleMovement(Vector3 Movement, Vector2 look)
    {
        if (InSelfieMode || CameraBusy)
        {
            // From camera look controls.

            return;
        }

        // None Selfie Mode Logic.

        Vector3 vel = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        vel = Quaternion.FromToRotation(Vector3.forward, this.transform.forward) * vel;

        this.transform.Rotate(new Vector3(0, look.x, 0));

        controller.Move(Movement);
    }

    public void AnimationFinished(bool EndedInSelfieMode)
    {
        CameraBusy = false;
        InSelfieMode = EndedInSelfieMode;
    }
}
