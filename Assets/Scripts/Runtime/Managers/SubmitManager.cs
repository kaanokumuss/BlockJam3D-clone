using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class SubmitManager : MonoBehaviour
{
    [SerializeField] MatchManager matchManager;
    [SerializeField] public Transform[] submitPositions;
    public   List<Sphere> sphereInfos = new List<Sphere>();
    public bool isCheckingForMatch = false;
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
        Color sphereColor = renderer.material.color;

        int targetIndex = GetAvailableIndexForColor(sphereColor);

        if (targetIndex >= 0)
        {
            MoveSphereToPosition(touchedSphere, targetIndex, sphereColor);
        }
        else
        {
            Debug.LogError("No available index found for the color.");
            isCheckingForMatch = false;
        }
    }

    void MoveSphereToPosition(GameObject sphere, int targetIndex, Color color)
    {
        Vector3 newPosition = submitPositions[targetIndex].position;
        newPosition.y += 0.46f;
        sphere.GetComponent<Sphere>().MoveTo(newPosition).OnComplete(() =>
        {
            UpdateSphereInfo(sphere, targetIndex, color); matchManager.CheckForMatchingColors();
            isCheckingForMatch = false; // Hareket tamamlandığında tıklama tekrar aktif
        });
    }

    void UpdateSphereInfo(GameObject sphere, int index, Color color)
    {
        sphereInfos.RemoveAll(info => info.SphereObject == sphere);
        sphereInfos.Add(new Sphere { Index = index, Color = color, SphereObject = sphere });
    }

    int GetAvailableIndexForColor(Color color)
    {
        // Aynı renkteki küplerin bulunduğu mevcut indeksleri bul
        List<int> sameColorIndices = new List<int>();
        foreach (var info in sphereInfos)
        {
            if (info.Color == color)
            {
                sameColorIndices.Add(info.Index);
            }
        }

        if (sameColorIndices.Count > 0)
        {
            int lastSameColorIndex = sameColorIndices[sameColorIndices.Count - 1];
            int nextIndex = lastSameColorIndex + 1;

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

        // Eğer aynı renkten bir küp yoksa, ilk boş pozisyonu bul
        for (int i = 0; i < submitPositions.Length; i++)
        {
            if (IsPositionAvailable(i))
            {
                return i;
            }
        }

        return -1; // Uygun pozisyon yok
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
                    sphereInfos[i].SphereObject.GetComponent<Sphere>().MoveTo(newPosition);
                    sphereInfos[i].Index = newIndex;
                }
                else
                {
                    Debug.LogError("No more space to shift cubes right!");
                    break;
                }
            }
        }
    }
}