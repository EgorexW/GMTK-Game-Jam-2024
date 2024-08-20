using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using NaughtyAttributes;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class Music : MonoBehaviour
{
    enum PlayingState
    {
        GameplayLoop,
        GoodDay,
        BadDay
    }

    [SerializeField] EventReference gameplayLoopEvent;
    [SerializeField] EventReference badDayEvent;
    [SerializeField] EventReference goodDayEvent;
    
    EventInstance gameplayLoopInstance;
    EventInstance goodDayInstance;
    EventInstance badDayInstance;

    PlayingState playingState;

    void Start()
    {
        gameplayLoopInstance = RuntimeManager.CreateInstance(gameplayLoopEvent);
        goodDayInstance = RuntimeManager.CreateInstance(goodDayEvent);
        badDayInstance = RuntimeManager.CreateInstance(badDayEvent);
        PlayGameplayLoop();
    }

    void Update()
    {
        switch (playingState){
            case PlayingState.GameplayLoop:
                break;
            case PlayingState.GoodDay:
                goodDayInstance.getPlaybackState(out PLAYBACK_STATE goodState);
                if (goodState == PLAYBACK_STATE.STOPPED){
                    PlayGameplayLoop();
                }
                break;
            case PlayingState.BadDay:
                badDayInstance.getPlaybackState(out PLAYBACK_STATE badState);
                if (badState == PLAYBACK_STATE.STOPPED){
                    PlayGameplayLoop();
                }
                break;
        }   
    }

    void PlayGameplayLoop()
    {
        StopCurrentMusic();
        gameplayLoopInstance.start();
        playingState = PlayingState.GameplayLoop;
    }

    public void EndDay(DaySummary daySummary)
    {
        StopCurrentMusic();
        if (daySummary.income < 0){
            goodDayInstance.start();
            playingState = PlayingState.GoodDay;
        }
        else{
            badDayInstance.start();
            playingState = PlayingState.BadDay;
        }
    }

    void StopCurrentMusic()
    {
        gameplayLoopInstance.stop(STOP_MODE.ALLOWFADEOUT);
        goodDayInstance.stop(STOP_MODE.ALLOWFADEOUT);
        badDayInstance.stop(STOP_MODE.ALLOWFADEOUT);
    }
}
