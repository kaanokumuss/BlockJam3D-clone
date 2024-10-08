using System;
using System.Collections;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    [SerializeField] Camera cam;
    bool _canTouch;

    void Start()
    {
        _canTouch = false;
    }

    void Update()
    {
        GetTouch(Input.mousePosition);
    }

    void GetTouch(Vector3 pos)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(pos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.TryGetComponent(out ITouchable selectedElement))
                {
                    TouchEvents.OnElementTapped?.Invoke(selectedElement);
                }
            }
            else
            {
                TouchEvents.OnEmptyTapped?.Invoke();
            }
        }
    }
}

public static class TouchEvents
{
    public static Action<ITouchable> OnElementTapped;
    public static Action OnEmptyTapped;
}

public interface ITouchable
{
    GameObject gameObject { get; }
}