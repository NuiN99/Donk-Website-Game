using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public static MainCamera Instance { get; private set; }

    [field: SerializeField] public Camera Camera { get; private set; }

    public Vector2 MousePosition => Camera.ScreenToWorldPoint(Input.mousePosition);

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
}