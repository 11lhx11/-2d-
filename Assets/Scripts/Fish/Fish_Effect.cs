using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Effect : MonoBehaviour
{
    public GameObject effectPrefab;
    public void PlayEffect()
    {
        Instantiate(effectPrefab);
    }
}
