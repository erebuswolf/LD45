using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowImage : MonoBehaviour
{
    [SerializeField]
    CameraMovement camMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowSelfie()
    {
        camMove.SetPhotoVisible();
    }
}
