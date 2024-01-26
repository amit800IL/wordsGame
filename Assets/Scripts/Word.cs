using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Word : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private string wordString;
    [SerializeField] private float letterScale;

    public int Length => wordString.Length;
}
