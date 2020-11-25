using UnityEditor;

[CustomEditor(typeof(OnEvent))]
[CanEditMultipleObjects]
public class OnEventEditor : Editor
{
    private SerializedProperty eventTrigger;
    private SerializedProperty checkTag;
    private SerializedProperty collisionTag;
    private SerializedProperty triggerOnce;

    private SerializedProperty unityEvent;
    private SerializedProperty colliderEvent;
    private SerializedProperty collider2DEvent;
    private SerializedProperty collisionEvent;
    private SerializedProperty collision2DEvent;

    private SerializedProperty currentEvent;

    private bool showTagFields = false;

    private void OnEnable()
    {
        eventTrigger = serializedObject.FindProperty("eventTrigger");
        checkTag = serializedObject.FindProperty("checkTag");
        collisionTag = serializedObject.FindProperty("collisionTag");
        triggerOnce = serializedObject.FindProperty("triggerOnce");
        unityEvent = serializedObject.FindProperty("unityEvent");
        colliderEvent = serializedObject.FindProperty("colliderEvent");
        collider2DEvent = serializedObject.FindProperty("collider2DEvent");
        collisionEvent = serializedObject.FindProperty("collisionEvent");
        collision2DEvent = serializedObject.FindProperty("collision2DEvent");

        UpdateVisibility();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(eventTrigger);
        if (EditorGUI.EndChangeCheck())
        {
            UpdateVisibility();
        }

        if(currentEvent != null)
        {
            if(showTagFields)
            {
                EditorGUILayout.PropertyField(checkTag);
                if(checkTag.boolValue)
                {
                    collisionTag.stringValue = EditorGUILayout.TagField(collisionTag.displayName, collisionTag.stringValue);
                }
            }

            EditorGUILayout.PropertyField(triggerOnce);
            EditorGUILayout.PropertyField(currentEvent);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void UpdateVisibility()
    {
        switch (eventTrigger.enumValueIndex)
        {
            case (int)OnEvent.EventTrigger.OnTriggerEnter:
            case (int)OnEvent.EventTrigger.OnTriggerStay:
            case (int)OnEvent.EventTrigger.OnTriggerExit:
                showTagFields = true;
                currentEvent = colliderEvent;
                break;
            case (int)OnEvent.EventTrigger.OnTriggerEnter2D:
            case (int)OnEvent.EventTrigger.OnTriggerStay2D:
            case (int)OnEvent.EventTrigger.OnTriggerExit2D:
                showTagFields = true;
                currentEvent = collider2DEvent;
                break;
            case (int)OnEvent.EventTrigger.OnCollisionEnter:
            case (int)OnEvent.EventTrigger.OnCollisionStay:
            case (int)OnEvent.EventTrigger.OnCollisionExit:
                showTagFields = true;
                currentEvent = collisionEvent;
                break;
            case (int)OnEvent.EventTrigger.OnCollisionEnter2D:
            case (int)OnEvent.EventTrigger.OnCollisionStay2D:
            case (int)OnEvent.EventTrigger.OnCollisionExit2D:
                showTagFields = true;
                currentEvent = collision2DEvent;
                break;
            case (int)OnEvent.EventTrigger.None:
                showTagFields = false;
                currentEvent = null;
                break;
            default:
                showTagFields = false;
                currentEvent = unityEvent;
                break;
        }
    }
}
