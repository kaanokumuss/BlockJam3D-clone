using System;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Planet planetPrefab;
    [SerializeField] private Transform planetParent;

    public Planet[] Tiles { get; private set;}

    void Awake()
    {
        TouchEvents.OnElementTapped += PlanetTapped;
        PreparePlanets();
    }

    private void OnDestroy()
    {
        TouchEvents.OnElementTapped -= PlanetTapped;
    }

    void PreparePlanets()
    {
        var planetCount = 5; 
        Tiles = new Planet[planetCount]; // todo: change with level tile amount 

        for (int i = 0; i < planetCount; i++)
        {
            Tiles[i] = Instantiate(planetPrefab, planetParent);
        }
    }

    void PlanetTapped(ITouchable touchable)
    {
        var tappedPlanet = touchable.gameObject.GetComponent<Planet>();
    }
} 
