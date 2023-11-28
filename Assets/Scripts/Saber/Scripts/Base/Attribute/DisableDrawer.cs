using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DisableAttribute : PropertyAttribute
{
    public bool isShow { get; set; }

    public DisableAttribute() : base()
    {

    }
    public DisableAttribute(bool isShow) : base()
    {
        this.isShow = isShow;
    }
    public DisableAttribute(bool isShow,int order) : base()
    {
        this.isShow = isShow;
        this.order = order;
    }
}

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(DisableAttribute))]
public class DisableDrawer : PropertyDrawer
{
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var aab = ((DisableAttribute)attribute);
        bool isShow = aab.isShow;
        var togglePos = new Rect(position);
        togglePos.center = new Vector2(togglePos.center.x + position.size.x-15, togglePos.center.y);
        aab.isShow = EditorGUI.Toggle(togglePos, isShow);
        position.xMax-= 30;
        //EditorGUI.ToggleLeft(togglePos, "IsShow", isShow);
        EditorGUI.BeginDisabledGroup(!isShow);
        EditorGUI.PropertyField(position, property, label);

        //togglePos.position
        EditorGUI.EndDisabledGroup();
    }
}
#endif