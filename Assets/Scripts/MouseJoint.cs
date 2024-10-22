using UnityEngine;

public class MouseJoint : MonoBehaviour
{
    public static MouseJoint Instance { get; private set; }

    [SerializeField] SpringJoint2D joint;
    [SerializeField] float attachedDrag;

    float _prevAttachedDrag;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void FixedUpdate()
    {
        joint.attachedRigidbody.position = MainCamera.Instance.MousePosition;
    }
    
    public void Attach(Rigidbody2D rb)
    {
        _prevAttachedDrag = rb.drag;
        joint.connectedBody = rb;
        joint.connectedBody.drag = attachedDrag;
    }

    public void Detach()
    {
        joint.connectedBody.drag = _prevAttachedDrag;
        joint.connectedBody = null;
    }
}