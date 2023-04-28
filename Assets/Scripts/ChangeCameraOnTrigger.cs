using Cinemachine;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraOnTrigger : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera OnCamera;
    [SerializeField] CinemachineVirtualCamera OffCamera;

    CinemachineComponentBase body;

    WarpDoor warpDoor;
    void Start()
    {
        warpDoor = GetComponent<WarpDoor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (warpDoor)
        {
            if (!warpDoor.CanEnter) return;
        }


        if (OnCamera != null)
            OnCamera.gameObject.SetActive(true);
        if (OffCamera != null)
            OffCamera.gameObject.SetActive(false);
        //Invoke("RefreshPos", 2);
        //   OffCamera.gameObject.SetActive(false);

    }
    void RefreshPos()
    {
        var cameraController = OnCamera.GetComponent<CinemachineCameraController>();
        cameraController.SetTarget(LevelManager.Instance.Players[0]);
        MMCameraEvent.Trigger(MMCameraEventTypes.RefreshPosition);

    }
}
