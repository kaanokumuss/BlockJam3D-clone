using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] string collisionTag = "Untagged";
    bool _canTouch;

    void Start()
    {
        _canTouch = false;
        StartCoroutine(WaitForTouch_Cor());
    }
    
    

    void Update()
    {
        if (_canTouch)
        {
            GetTouch(Input.mousePosition);
        }
    }
    
    private void GetTouch(Vector3 pos)
    {
        if (Input.GetMouseButtonDown(0))
        {
            var hit = Physics2D.OverlapPoint(cam.ScreenToWorldPoint(pos));
            if (CanTouch(hit))
            {
                
                if (hit.gameObject.TryGetComponent(out ITouchable selectedElement))
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

    private bool CanTouch(Collider2D hit)
    {
        return hit != null && hit.CompareTag(collisionTag);
    }
    
    IEnumerator WaitForTouch_Cor()
    {
        yield return new WaitForSeconds(1.5f);

        _canTouch = true;
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