using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SubmitManager : MonoBehaviour
{
    [SerializeField] MatchManager matchManager;
    [SerializeField] public Transform[] submitPositions;
    [SerializeField] private Button undoButton;
    public List<Sphere> sphereInfos = new List<Sphere>();
    public bool isCheckingForMatch = false;
    private Stack<Sphere> undoStack = new Stack<Sphere>(); 

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
        if (isCheckingForMatch) return;

        isCheckingForMatch = true;

        GameObject touchedSphere = touchedElement.gameObject;
        Renderer renderer = touchedSphere.GetComponent<Renderer>();
        Material sphereMaterial = renderer.material;

        int targetIndex = GetAvailableIndexForMaterial(sphereMaterial);

        if (targetIndex >= 0)
        {
            MoveSphereToPosition(touchedSphere, targetIndex, sphereMaterial);
        }
        else
        {
            Debug.LogError("No available index found for the color.");
            isCheckingForMatch = false;
        }
    }

    void MoveSphereToPosition(GameObject sphere, int targetIndex, Material material)
    {
        Vector3 newPosition = submitPositions[targetIndex].position;
        newPosition.y += 0.46f;
        // var spphere gget comp yaz
        
        sphere.GetComponent<Sphere>().MoveTo(newPosition).OnComplete(() =>
        {
            // Sphere nesnesini undo için sakla
            undoStack.Push(sphere.GetComponent<Sphere>());

            UpdateSphereInfo(sphere, targetIndex, material);
            matchManager.CheckForMatchingMaterials();
            isCheckingForMatch = false;
        });
    }

    void UpdateSphereInfo(GameObject sphere, int index, Material material)
    {
        sphereInfos.RemoveAll(info => info.SphereObject == sphere);
        sphereInfos.Add(new Sphere { Index = index, Material = material, SphereObject = sphere });
    }

    int GetAvailableIndexForMaterial(Material material)
    {
        List<int> sameMaterialIndices = new List<int>();
        foreach (var info in sphereInfos)
        {
            if (info.Material.name == material.name)
            {
                sameMaterialIndices.Add(info.Index);
            }
        }

        if (sameMaterialIndices.Count > 0)
        {
            int lastSameMaterialIndex = sameMaterialIndices[sameMaterialIndices.Count - 1];
            int nextIndex = lastSameMaterialIndex + 1;

            if (IsPositionAvailable(nextIndex))
            {
                return nextIndex;
            }
            else
            {
                ShiftSpheresRight(nextIndex);
                return nextIndex;
            }
        }

        for (int i = 0; i < submitPositions.Length; i++)
        {
            if (IsPositionAvailable(i))
            {
                return i;
            }
        }

        return -1;
    }

    bool IsPositionAvailable(int index)
    {
        return index >= 0 && index < submitPositions.Length && !sphereInfos.Exists(info => info.Index == index);
    }

    void ShiftSpheresRight(int startIndex)
    {
        for (int i = sphereInfos.Count - 1; i >= 0; i--)
        {
            if (sphereInfos[i].Index >= startIndex)
            {
                int newIndex = sphereInfos[i].Index + 1;
                if (IsPositionAvailable(newIndex))
                {
                    Vector3 newPosition = submitPositions[newIndex].position;
                    newPosition.y += 0.46f;
                    sphereInfos[i].MoveTo(newPosition);
                    sphereInfos[i].Index = newIndex;
                }
                else
                {
                    Debug.LogError("No more space to shift spheres right!");
                    break;
                }
            }
        }
    }

    // Undo işlemi için yeni bir fonksiyon ekleyelim
    public void UndoLastMove()
    {
        if (undoStack.Count > 0)
        {
            Sphere lastMovedSphere = undoStack.Pop();
            Debug.Log("Popped");

            // Sphere'i undo işlemi için geri taşı
            lastMovedSphere.MoveBack().OnComplete(() =>
            {
                // Sphere'in info listesinden çıkarılması
                sphereInfos.RemoveAll(info => info.SphereObject == lastMovedSphere.gameObject);
                isCheckingForMatch = false;
            });
        }
    }
}
