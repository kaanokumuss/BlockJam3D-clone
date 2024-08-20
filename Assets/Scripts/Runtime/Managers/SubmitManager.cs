using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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
        if (isCheckingForMatch || !canTap) return; // `canTap` kontrolü eklendi.

        isCheckingForMatch = true;
        canTap = false; // Dokunma işlemi başladıktan sonra dokunma devre dışı bırakılır.

        GameObject touchedSphere = touchedElement.gameObject;
        Renderer renderer = touchedSphere.GetComponent<Renderer>();
        Material sphereMaterial = renderer.material;

        int targetIndex = matchManager.GetAvailableIndexForMaterial(sphereMaterial);

        if (targetIndex >= 0)
        {
            sphereMoveController.MoveSphereToPosition(touchedSphere, targetIndex, sphereMaterial);
        }
        else
        {
            Debug.LogError("No available index found for the color.");
            isCheckingForMatch = false;
            canTap = true; // Hata durumunda tekrar dokunma aktif hale getirilir.
        }

        StartCoroutine(EnableTapAfterDelay(delay)); // 2 saniye bekleme başlatılır.
    }

    // Yeni Coroutine metodu eklendi.
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
}
