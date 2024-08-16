using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public enum CompareType{
    Bigger,
    Smaller,
    Equal
}
enum ObjectType{
    Int,
    Float,
    String, 
    Bool
}

public class CheckValue : MonoBehaviour
{
    [SerializeField] ObjectType objType;
    [ShowIf("IsInt")]
    [SerializeField] int nrInt;
    bool IsInt{
        get {
            return objType == ObjectType.Int;
        }
    }
    [ShowIf("IsFloat")]
    [SerializeField] float nrFloat;
    bool IsFloat{
        get {
            return objType == ObjectType.Float;
        }
    }
    [ShowIf("InNumber")]
    [SerializeField] CompareType compareType = CompareType.Equal;
    bool InNumber{
        get {
            return IsFloat || IsInt;
        }
    }
    [ShowIf("IsString")]
    [SerializeField] string text;
    bool IsString{
        get {
            return objType == ObjectType.String;
        }
    }
    [ShowIf("IsBool")]
    [SerializeField] bool desiredCondition;
    bool IsBool{
        get {
            return objType == ObjectType.Bool;
        }
    }
    [Foldout("Events")]
    public UnityEvent onTrue;
    [Foldout("Events")]
    public UnityEvent onFalse;
    [Foldout("Events")]
    public UnityEvent<bool> onCheck;

    public bool GetCheck(object obj){
        bool result = false;
        switch (objType){
            case ObjectType.Int:
                result = CheckInt((int)obj);
                break;
            case ObjectType.Float:
                result = CheckFloat((float)obj);
                break;
            case ObjectType.String:
                result = CheckString((string)obj);
                break;
            case ObjectType.Bool:
                result = CheckBool((bool)obj);
                break;
        }
        if (result){
            onTrue.Invoke();
        } else {
            onFalse.Invoke();
        }
        onCheck.Invoke(result);
        return result;
    }

    public bool CheckBool(bool condition)
    {
        return condition == desiredCondition;
    }

    public bool CheckInt(int value){
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
    public bool CheckFloat(float value){
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
    public bool CheckString(string value){
        return value == text;
    }
}
