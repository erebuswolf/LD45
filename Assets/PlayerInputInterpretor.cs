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

    static void QuitGame()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetAxis("Cancel") != 0)
        {
            QuitGame();
        }
        if (playerController == null)
        {
            return;
        }

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

            playerController.TakeSelfie();
        }
        /*
        if (Input.GetAxis("TakeSelfie") != 0)
        {
            playerController.TakeSelfie();
        }*/

        if (Input.GetAxis("ExitCameraMode") != 0)
        {
            playerController.ExitSelfieMode();
        }
    }
}
