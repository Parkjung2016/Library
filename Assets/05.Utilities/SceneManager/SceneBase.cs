using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Parkjung2016;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class SceneBase : MonoBehaviour
{
    #region Variables

    public Camera MainCam { get; private set; }
    public CinemachineVirtualCamera[] CinemachineVCams { get; private set; }

    #endregion

    protected virtual void Awake()
    {
        CinemachineVCams = FindObjectsOfType<CinemachineVirtualCamera>();
        MainCam = Camera.main;

    }

}