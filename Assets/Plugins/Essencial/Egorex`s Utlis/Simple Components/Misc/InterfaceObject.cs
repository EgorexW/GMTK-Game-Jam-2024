using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;


public class InterfaceObject{

}
[System.Serializable][Obsolete]
public class InterfaceObject<InterfaceType, InterfaceObj> : InterfaceObject where InterfaceObj : Object, InterfaceType{
    InterfaceType interfaceClass;
    public InterfaceType I {
        get{
            if (interfaceClass != null){
                return interfaceClass;
            }
            if (interfaceObject != null){
                return interfaceObject;
            }
            return default;
        }
        set {
            interfaceClass = value;
        }
    }
    [SerializeField] InterfaceObj interfaceObject;

    public InterfaceObject(InterfaceType interfaceClass)
    {
        I = interfaceClass;
    }
    public static implicit operator InterfaceType(InterfaceObject<InterfaceType, InterfaceObj> interfaceObject){
        return interfaceObject.I;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(InterfaceObject), true)]
class InterfaceObjectDrawer : PropertyDrawer
{
    public override void OnGUI(
        Rect position,
        SerializedProperty property,
        GUIContent label
    )
    {
        SerializedProperty objectProperty = property.FindPropertyRelative("interfaceObject");

        // position.width -= 45;
        EditorGUI.PropertyField(position, objectProperty, label, true);

        // position.x += position.width + 40;
        // position.width = 40;
        // position.x -= position.width - 5;
        // position.height = EditorGUI.GetPropertyHeight(scoreProperty);
        // EditorGUI.PropertyField(position, scoreProperty, GUIContent.none);
    }
}
#endif