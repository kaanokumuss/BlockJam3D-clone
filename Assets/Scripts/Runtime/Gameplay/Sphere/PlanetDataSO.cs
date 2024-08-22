using UnityEngine;

[CreateAssetMenu(fileName = "NewPlanetData", menuName = "ScriptableObjects/PlanetData", order = 1)]
public class PlanetDataSO : ScriptableObject
{
    public string planetName;
    public Material planetMaterial;
    public Vector3 position;
    public int[] childrenIDs; 
}