using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;

public class CheckPlayerPref : MonoBehaviour
{
    [SerializeField] string prefName;
    [SerializeField] ObjectType prefType;
    [ShowIf("IsInt")]
    [SerializeField] int nrInt;
    [SerializeField] bool IsInt{
        get {
            return prefType == ObjectType.Int;
        }
    }
    [ShowIf("IsFloat")]
    [SerializeField] float nrFloat;
    [SerializeField] bool IsFloat{
        get {
            return prefType == ObjectType.Float;
        }
    }
    [HideIf("IsString")]
    [SerializeField] CompareType compareType = CompareType.Equal;
    [ShowIf("IsString")]
    [SerializeField] string text;
    [SerializeField] bool IsString{
        get {
            return prefType == ObjectType.String;
        }
    }
    [Foldout("Events")]
    public UnityEvent onTrue;
    [Foldout("Events")]
    public UnityEvent onFalse;
    [Foldout("Events")]
    public UnityEvent<bool> onCheck;

    public void Check(){
        GetCheck();
    }
    public void Check(UnityAction<bool> callback){
        GetCheck(callback);
    }
    public bool GetCheck(UnityAction<bool> callback = null){
        bool result = false;
        switch (prefType){
            case ObjectType.Int:
                result = CheckInt();
                break;
            case ObjectType.Float:
                result = CheckFloat();
                break;
            case ObjectType.String:
                result = CheckString();
                break;
        }
        if (callback != null){
            callback.Invoke(result);
        }
        if (result){
            onTrue.Invoke();
        } else {
            onFalse.Invoke();
        }
        onCheck.Invoke(result);
        return result;
    }
    public bool CheckInt(){
        int value = PlayerPrefs.GetInt(prefName, 0);
        switch (compareType){
            case CompareType.Bigger:
                return value > nrInt;
            case CompareType.Smaller:
                return value < nrInt;
            case CompareType.Equal:
                return value == nrInt;
        }
        return false;
    }
    public bool CheckFloat(){
        float value = PlayerPrefs.GetFloat(prefName, 0f);
        switch (compareType){
            case CompareType.Bigger:
                return value > nrInt;
            case CompareType.Smaller:
                return value < nrInt;
            case CompareType.Equal:
                return value == nrInt;
        }
        return false;
    }
    public bool CheckString(){
        string value = PlayerPrefs.GetString(prefName, "");
        return value == text;
    }
}
