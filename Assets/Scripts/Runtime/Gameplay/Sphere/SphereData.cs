using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct SphereData
{
    public int id;
    public Vector3 position;
    public string planet;
    public string material;  
    public int[] children;
}