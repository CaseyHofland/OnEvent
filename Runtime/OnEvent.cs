using System;
using UnityEngine;
using UnityEngine.Events;

public class OnEvent : MonoBehaviour
{
    [SerializeField] private EventTrigger eventTrigger = EventTrigger.None;
    [SerializeField] private bool checkTag = false;
    [SerializeField] private string collisionTag = "Untagged";
    [SerializeField] private bool triggerOnce = false;

    [SerializeField] private UnityEvent unityEvent = new UnityEvent();
    [SerializeField] private ColliderEvent colliderEvent = new ColliderEvent();
    [SerializeField] private Collider2DEvent collider2DEvent = new Collider2DEvent();
    [SerializeField] private CollisionEvent collisionEvent = new CollisionEvent();
    [SerializeField] private Collision2DEvent collision2DEvent = new Collision2DEvent();

    public enum EventTrigger
    {
        None,
        Awake,
        Start,
        OnDestroy,
        OnEnable,
        OnDisable,
        OnTriggerEnter,
        OnTriggerStay,
        OnTriggerExit,
        OnTriggerEnter2D,
        OnTriggerStay2D,
        OnTriggerExit2D,
        OnCollisionEnter,
        OnCollisionStay,
        OnCollisionExit,
        OnCollisionEnter2D,
        OnCollisionStay2D,
        OnCollisionExit2D,
        MouseEnter,
        MouseExit,
        MouseDown,
        MouseUp,
    }

    [Serializable] public class ColliderEvent : UnityEvent<Collider> { }
    [Serializable] public class Collider2DEvent : UnityEvent<Collider2D> { }
    [Serializable] public class CollisionEvent : UnityEvent<Collision> { }
    [Serializable] public class Collision2DEvent : UnityEvent<Collision2D> { }

    private bool CheckTag(string tag)
    {
        return !checkTag || collisionTag == tag;
    }

    private void Awake()
    {
        HandleGameEvent(EventTrigger.Awake);
    }

    private void Start()
    {
        HandleGameEvent(EventTrigger.Start);
    }

    private void OnDestroy()
    {
        HandleGameEvent(EventTrigger.OnDestroy);
    }

    private void OnEnable()
    {
        HandleGameEvent(EventTrigger.OnEnable);
    }

    private void OnDisable()
    {
        HandleGameEvent(EventTrigger.OnDisable);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CheckTag(other.tag))
        {
            HandleGameEvent(EventTrigger.OnTriggerEnter, other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(CheckTag(other.tag))
        {
            HandleGameEvent(EventTrigger.OnTriggerStay, other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (CheckTag(other.tag))
        {
            HandleGameEvent(EventTrigger.OnTriggerExit, other);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CheckTag(other.tag))
        {
            HandleGameEvent(EventTrigger.OnTriggerEnter2D, other);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(CheckTag(other.tag))
        {
            HandleGameEvent(EventTrigger.OnTriggerStay2D, other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (CheckTag(other.tag))
        {
            HandleGameEvent(EventTrigger.OnTriggerExit2D, other);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (CheckTag(collision.transform.tag))
        {
            HandleGameEvent(EventTrigger.OnCollisionEnter, collision);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(CheckTag(collision.transform.tag))
        {
            HandleGameEvent(EventTrigger.OnCollisionStay, collision);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (CheckTag(collision.transform.tag))
        {
            HandleGameEvent(EventTrigger.OnCollisionExit, collision);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CheckTag(collision.transform.tag))
        {
            HandleGameEvent(EventTrigger.OnCollisionEnter2D, collision);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(CheckTag(collision.transform.tag))
        {
            HandleGameEvent(EventTrigger.OnCollisionStay2D, collision);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (CheckTag(collision.transform.tag))
        {
            HandleGameEvent(EventTrigger.OnCollisionExit2D, collision);
        }
    }

    private void OnMouseEnter()
    {
        HandleGameEvent(EventTrigger.MouseEnter);
    }

    private void OnMouseExit()
    {
        HandleGameEvent(EventTrigger.MouseExit);
    }

    private void OnMouseDown()
    {
        HandleGameEvent(EventTrigger.MouseDown);
    }

    private void OnMouseUp()
    {
        HandleGameEvent(EventTrigger.MouseUp);
    }

    private void HandleGameEvent(EventTrigger gameEvent, object obj = null)
    {
        if (eventTrigger != gameEvent || !this)
        {
            return;
        }

        switch (gameEvent)
        {
            case EventTrigger.OnTriggerEnter:
            case EventTrigger.OnTriggerStay:
            case EventTrigger.OnTriggerExit:
                colliderEvent.Invoke(obj as Collider);
                break;
            case EventTrigger.OnTriggerEnter2D:
            case EventTrigger.OnTriggerStay2D:
            case EventTrigger.OnTriggerExit2D:
                collider2DEvent.Invoke(obj as Collider2D);
                break;
            case EventTrigger.OnCollisionEnter:
            case EventTrigger.OnCollisionStay:
            case EventTrigger.OnCollisionExit:
                collisionEvent.Invoke(obj as Collision);
                break;
            case EventTrigger.OnCollisionEnter2D:
            case EventTrigger.OnCollisionStay2D:
            case EventTrigger.OnCollisionExit2D:
                collision2DEvent.Invoke(obj as Collision2D);
                break;
            default:
                unityEvent.Invoke();
                break;
        }

        if (triggerOnce && gameEvent != EventTrigger.OnDestroy)
        {
            Destroy(this);
        }
    }
}
