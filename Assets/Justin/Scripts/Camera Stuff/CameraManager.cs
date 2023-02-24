using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Available Camara's")]
    public CinemachineVirtualCamera playerCam;

    private void OnEnable()
    {
        CameraSwitcher.playerCam = playerCam;
    }

    private void OnDisable()
    {
        CameraSwitcher.playerCam = null;
    }
}
