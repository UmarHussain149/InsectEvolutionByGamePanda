using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertToSingleMesh : MonoBehaviour
{
    public Mesh cubeMesh;


    [ContextMenu("SetData")]
    public void SetData()
    {
        SkinnedMeshRenderer[] skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < skinnedMeshRenderers.Length; i++)
        {
            skinnedMeshRenderers[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            AddComponents(skinnedMeshRenderers[i]);

        }

        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            AddComponents(meshRenderers[i]);
        }

    }

    void AddComponents(Renderer obj)
    {
        obj.transform.GetComponent<MeshFilter>().mesh = cubeMesh;
        //if (!obj.GetComponent<BoxCollider>())
        //    obj.AddComponent<BoxCollider>().isTrigger = true;

        //obj.AddComponent<PixelCube>();
    }
    void FallCube()
    {
        var randPosition = Random.insideUnitSphere;
        randPosition.y = Random.Range(1f, 5f);

        Vector3 targetPosition = transform.position + randPosition.normalized * 5.1f;
        targetPosition.y = 0; //set it according to ground 
        transform.DOJump(targetPosition, Random.Range(0.5f, 1.2f), 1, 1.3f).SetEase(Ease.InSine);

        this.gameObject.tag = "Finish";
    }
}
