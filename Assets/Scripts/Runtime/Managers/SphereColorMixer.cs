using System.Collections.Generic;
using UnityEngine;

public class SphereColorMixer : MonoBehaviour
{
    [SerializeField] private Color[] possibleColors; 
    private Dictionary<Color, int> colorCount = new Dictionary<Color, int>();
    private int minColorCount = 3; 

    private void Awake()
    {
        foreach (Color color in possibleColors)
        {
            colorCount[color] = 0;
        }
    }

    public void RandomColor(GameObject sphere)
    {
        List<Color> availableColors = new List<Color>();
        foreach (Color color in possibleColors)
        {
            if (colorCount[color] < minColorCount || colorCount.Count <= possibleColors.Length - minColorCount)
            {
                availableColors.Add(color);
            }
        }
        
        if (availableColors.Count > 0)
        {
            Color chosenColor = availableColors[Random.Range(0, availableColors.Count)];
            Renderer sphereRenderer = sphere.GetComponent<Renderer>();
            if (sphereRenderer != null)
            {
                sphereRenderer.material.color = chosenColor;
                colorCount[chosenColor]++;
            }
        }
    }
}