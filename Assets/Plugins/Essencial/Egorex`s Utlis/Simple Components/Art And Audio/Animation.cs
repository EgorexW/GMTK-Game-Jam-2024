using UnityEngine;
using NaughtyAttributes;

[System.Serializable]
public class Animation
{
    public string name;
    public AnimationCell[] animationCells;
#if UNITY_EDITOR
    [OnValueChanged("SetCellsDuration")][AllowNesting]
    [SerializeField] float defaultCellDuration;
#endif
    public bool loop = true;
    [HideIf("loop")][AllowNesting]
    public Sprite spriteOnEnd;
    float index = 0;
    float cycleDuration = -10;

#if UNITY_EDITOR
    void SetCellsDuration(){
        float cellDuration = defaultCellDuration;
        for (int i = 0; i < animationCells.Length; i++)
        {
            animationCells[i].duration = cellDuration;
        }
    }
#endif

    public void Restart(){
        if (loop){
            return;
        }
        index = 0;
    }
    public Sprite GetNextFrame(float timeSinceLastFrame){
        index += timeSinceLastFrame;
        float indexTmp = index;
        Sprite frame = null;
        foreach (var animationCell in animationCells){
            if (indexTmp <= animationCell.duration){
                frame = animationCell.sprite;
                break;
            }
            indexTmp -= animationCell.duration;
        }
        if (frame == null){
            if (loop){
                index -= GetCycleDuration();
                return GetNextFrame(0);
            } else {
                frame = spriteOnEnd;
            }
        }
        return frame;
    }
    public float GetCycleDuration(){
        if (cycleDuration > 0){
            return cycleDuration;
        }
        cycleDuration = 0;
        foreach(var animationCell in animationCells){
            cycleDuration += animationCell.duration;
        }
        return cycleDuration;
    }
}
