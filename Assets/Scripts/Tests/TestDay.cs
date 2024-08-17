using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class TestDay : MonoBehaviour
{
    [Required][SerializeField] Day day;

    [Button]
    public void RunDay()
    {
        day.RunDay();
    }
}
