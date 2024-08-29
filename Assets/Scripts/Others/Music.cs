using FMOD.Studio;
using FMODUnity;
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
                break;
            case PlayingState.BadDay:
                break;
        }   
    }

    public void PlayGameplayLoop()
    {
        StopCurrentMusic();
        gameplayLoopInstance.start();
        playingState = PlayingState.GameplayLoop;
    }

    public void EndDay(DaySummary daySummary)
    {
        StopCurrentMusic();
        if (daySummary.income > 0){
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

    void OnDestroy()
    {
        StopCurrentMusic();
        gameplayLoopInstance.release();
        goodDayInstance.release();
        badDayInstance.release();
    }
}
