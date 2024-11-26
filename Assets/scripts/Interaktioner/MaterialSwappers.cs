using UnityEngine;

public class MaterialSwappers : MonoBehaviour
{
    public Material newMaterial;

    bool isNewMaterialAcive = false;
    Material oldmaterial;
    void Start()
    {
        oldmaterial = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    public void SwapMaterial()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        isNewMaterialAcive |= !isNewMaterialAcive;

        if (isNewMaterialAcive)
            meshRenderer.material = newMaterial;
        else
            meshRenderer.material = oldmaterial;

    }
}
