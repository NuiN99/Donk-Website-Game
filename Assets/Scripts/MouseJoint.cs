using UnityEngine;

public class MouseJoint : MonoBehaviour
{
    public static MouseJoint Instance { get; private set; }

    [SerializeField] SpringJoint2D joint;
    [SerializeField] float attachedDrag;
    [SerializeField] float attachedAngularDrag;

    float _prevAttachedDrag;
    float _prevAttachedAngularDrag;

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
        _prevAttachedDrag = rb.linearDamping;
        _prevAttachedAngularDrag = rb.angularDamping;
        
        joint.connectedBody = rb;
        joint.connectedBody.linearDamping = attachedDrag;
        joint.connectedBody.angularDamping = attachedAngularDrag;
    }

    public void Detach()
    {
        joint.connectedBody.linearDamping = _prevAttachedDrag;
        joint.connectedBody.angularDamping = _prevAttachedAngularDrag;
        joint.connectedBody = null;
    }
}