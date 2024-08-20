using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class StoreSfx : MonoBehaviour
{
    [SerializeField] EventReference storeOpenEvent;
    [SerializeField] EventReference storeCloseEvent;
    EventInstance storeOpenInstance;
    EventInstance storeCloseInstance;

    void Awake()
    {
        storeOpenInstance = RuntimeManager.CreateInstance(storeOpenEvent);
        storeCloseInstance = RuntimeManager.CreateInstance(storeCloseEvent);
        PlayStoreClose();
    }

    public void PlayStoreClose()
    {
        storeOpenInstance.stop(STOP_MODE.ALLOWFADEOUT);
        storeCloseInstance.start();
    }
    public void PlayStoreOpen()
    {
        storeCloseInstance.stop(STOP_MODE.ALLOWFADEOUT);
        storeOpenInstance.start();
    }
}
