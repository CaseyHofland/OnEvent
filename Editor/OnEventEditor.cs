using UnityEditor;

[CustomEditor(typeof(OnEvent))]
[CanEditMultipleObjects]
public class OnEventEditor : Editor
{
    private SerializedProperty _eventTrigger;
    private SerializedProperty _checkTag;
    private SerializedProperty _collisionTag;
    private SerializedProperty _collisionForce;
    private SerializedProperty _triggerOnce;

    private SerializedProperty _unityEvent;
    private SerializedProperty _colliderEvent;
    private SerializedProperty _collider2DEvent;
    private SerializedProperty _collisionEvent;
    private SerializedProperty _collision2DEvent;

    private SerializedProperty _sendDebugMessage;

    private SerializedProperty currentEvent;

    private bool showTagFields = false;
    private bool showCollisionFields = false;

    private void OnEnable()
    {
        _eventTrigger = serializedObject.FindProperty(nameof(_eventTrigger));
        _checkTag = serializedObject.FindProperty(nameof(_checkTag));
        _collisionTag = serializedObject.FindProperty(nameof(_collisionTag));
        _collisionForce = serializedObject.FindProperty(nameof(_collisionForce));
        _triggerOnce = serializedObject.FindProperty(nameof(_triggerOnce));

        _unityEvent = serializedObject.FindProperty(nameof(_unityEvent));
        _colliderEvent = serializedObject.FindProperty(nameof(_colliderEvent));
        _collider2DEvent = serializedObject.FindProperty(nameof(_collider2DEvent));
        _collisionEvent = serializedObject.FindProperty(nameof(_collisionEvent));
        _collision2DEvent = serializedObject.FindProperty(nameof(_collision2DEvent));

        _sendDebugMessage = serializedObject.FindProperty(nameof(_sendDebugMessage));

        UpdateVisibility();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(_eventTrigger);
        if (EditorGUI.EndChangeCheck())
        {
            UpdateVisibility();
        }

        if (currentEvent != null)
        {
            if (showTagFields)
            {
                EditorGUILayout.PropertyField(_checkTag);
                if (_checkTag.boolValue)
                {
                    _collisionTag.stringValue = EditorGUILayout.TagField(_collisionTag.displayName, _collisionTag.stringValue);
                }
            }

            if (showCollisionFields)
            {
                EditorGUILayout.PropertyField(_collisionForce);
            }

            EditorGUILayout.PropertyField(_triggerOnce);
            EditorGUILayout.PropertyField(currentEvent);
            EditorGUILayout.PropertyField(_sendDebugMessage);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void UpdateVisibility()
    {
        switch (_eventTrigger.enumValueIndex)
        {
            case (int)OnEvent.EventTrigger.OnTriggerEnter:
            case (int)OnEvent.EventTrigger.OnTriggerStay:
            case (int)OnEvent.EventTrigger.OnTriggerExit:
                showTagFields = true;
                showCollisionFields = false;
                currentEvent = _colliderEvent;
                break;
            case (int)OnEvent.EventTrigger.OnTriggerEnter2D:
            case (int)OnEvent.EventTrigger.OnTriggerStay2D:
            case (int)OnEvent.EventTrigger.OnTriggerExit2D:
                showTagFields = true;
                showCollisionFields = false;
                currentEvent = _collider2DEvent;
                break;
            case (int)OnEvent.EventTrigger.OnCollisionEnter:
            case (int)OnEvent.EventTrigger.OnCollisionStay:
            case (int)OnEvent.EventTrigger.OnCollisionExit:
                showTagFields = true;
                showCollisionFields = true;
                currentEvent = _collisionEvent;
                break;
            case (int)OnEvent.EventTrigger.OnCollisionEnter2D:
            case (int)OnEvent.EventTrigger.OnCollisionStay2D:
            case (int)OnEvent.EventTrigger.OnCollisionExit2D:
                showTagFields = true;
                showCollisionFields = true;
                currentEvent = _collision2DEvent;
                break;
            case (int)OnEvent.EventTrigger.None:
                showTagFields = false;
                showCollisionFields = false;
                currentEvent = null;
                break;
            default:
                showTagFields = false;
                showCollisionFields = false;
                currentEvent = _unityEvent;
                break;
        }
    }
}
