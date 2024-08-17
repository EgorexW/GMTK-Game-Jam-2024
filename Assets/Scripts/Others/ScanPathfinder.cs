using System.Collections;
using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

[RequireComponent(typeof(AstarPath))]
public class ScanPathfinder : MonoBehaviour
{
    [GetComponent][SerializeField] AstarPath astarPath;

    public void Scan()
    {
        astarPath.Scan();
    }
}
