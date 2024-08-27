using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UI;

public class SubmitManager : MonoBehaviour
{
    [SerializeField] private SphereMoveController sphereMoveController;
    [SerializeField] MatchManager matchManager;
    [SerializeField] public Transform[] submitPositions;
    [SerializeField] private Button undoButton;
    public List<Sphere> sphereInfos = new List<Sphere>();
    public bool isCheckingForMatch = false;
    public float delay = 1f;
    public Stack<Sphere> undoStack = new Stack<Sphere>();
    private bool canTap = true; // Yeni değişken eklendi.
    public float rayLength = 3f;
    public string[] collisionTags;


    void OnEnable()
    {
        TouchEvents.OnElementTapped += HandleElementTapped;
    }

    void OnDisable()
    {
        TouchEvents.OnElementTapped -= HandleElementTapped;
    }

    void HandleElementTapped(ITouchable touchedElement)
    {
        if (isCheckingForMatch || !canTap) return;

        // Cast the element to a GameObject for raycasting
        GameObject touchedSphere = touchedElement.gameObject;

        int collisionCount = CheckRaycastCollisions(touchedSphere);
        isCheckingForMatch = true;
        canTap = false;

        Renderer renderer = touchedSphere.GetComponent<Renderer>();
        if (renderer == null)
        {
            Debug.LogError("Renderer component not found on the touched sphere.");
            isCheckingForMatch = false;
            StartCoroutine(EnableTapAfterDelay(delay)); // Re-enable tap after delay
            return;
        }

        Material sphereMaterial = renderer.material;
        if (sphereMaterial.name.Equals("Lava (Instance)", System.StringComparison.OrdinalIgnoreCase))
        {
            GameEvents.CantTouch?.Invoke();
            Debug.Log("Lava material is not touchable.");
            isCheckingForMatch = false;
            StartCoroutine(EnableTapAfterDelay(delay)); // Re-enable tap after delay
            return; // Exit the method if the material is Lava
        }

        if (collisionCount < 4)
        {
            int targetIndex = matchManager.GetAvailableIndexForMaterial(sphereMaterial);

            if (targetIndex >= 0)
            {
                sphereMoveController.MoveSphereToPosition(touchedSphere, targetIndex, sphereMaterial);
            }
            else
            {
                GameEvents.FailPanel?.Invoke();
                GameEvents.OnFail?.Invoke();
            }

            StartCoroutine(EnableTapAfterDelay(delay));
        }
        else
        {
            isCheckingForMatch = false;
            canTap = true;
        }
    }


    private IEnumerator EnableTapAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canTap = true;
    }

    public void UpdateSphereInfo(GameObject sphere, int index, Material material)
    {
        Sphere existingSphere = sphere.GetComponent<Sphere>();

        if (existingSphere != null)
        {
            existingSphere.Index = index;
            existingSphere.Material = material;
            existingSphere.SphereObject = sphere;

            sphereInfos.RemoveAll(info => info.SphereObject == sphere);
            sphereInfos.Add(existingSphere);
        }
        else
        {
            Debug.LogError("Sphere component not found on the provided GameObject.");
        }
    }

    private int CheckRaycastCollisions(GameObject touchedElement)
    {
        int tagCount = 0; // Initialize count
        Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };

        foreach (Vector3 direction in directions)
        {
            RaycastHit hit;
            Debug.DrawRay(touchedElement.transform.position, direction * rayLength, Color.red, 1f);

            if (Physics.Raycast(touchedElement.transform.position, direction, out hit, rayLength))
            {
                Debug.Log("Hit detected with object: " + hit.collider.name + " with tag: " + hit.collider.tag);

                tagCount++;
            }
            else
            {
                Debug.Log("No hit detected in direction: " + direction);
            }
        }

        Debug.Log("Total tags matched: " + tagCount);
        return tagCount;
    }
}