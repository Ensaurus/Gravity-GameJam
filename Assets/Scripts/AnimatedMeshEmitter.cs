using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedMeshEmitter : MonoBehaviour
{
    public SkinnedMeshRenderer meshRenderer;
    public Mesh mesh;

    // Update is called once per frame
    void Update()
    {
        meshRenderer.BakeMesh (mesh);
    }
}
