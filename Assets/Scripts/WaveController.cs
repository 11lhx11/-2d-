using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public Texture[] textureNums;
    private Material m;
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        m = gameObject.GetComponent<MeshRenderer>().material;
        InvokeRepeating("UpdateTexture",0,0.1F);
    }
    void UpdateTexture()
    {
        m.mainTexture = textureNums[index];
        index = (index + 1) % textureNums.Length;
    }
}
