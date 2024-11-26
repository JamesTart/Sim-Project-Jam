using UnityEngine;

public class UpdatedMaterialSwapper : MonoBehaviour
{
    public Material newMaterial; // The material to swap to

    // Private Variables
    private Material oldMaterial; // Stores the original material
    private bool isNewMaterialActive = false; // Tracks which material is active

    void Start()
    {
        // Store the current material as the old material
        oldMaterial = GetComponent<MeshRenderer>().material;

        // Ensure `oldMaterial` is not null
        if (oldMaterial == null)
        {
            Debug.LogError("The MeshRenderer does not have a material assigned. Assign a material to the object.");
        }
    }

    public void SwapMaterial()
    {
        // Get the MeshRenderer component
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer == null)
        {
            Debug.LogError("MeshRenderer not found on the GameObject.");
            return;
        }

        // Toggle the material based on the current state
        isNewMaterialActive = !isNewMaterialActive;
        if (isNewMaterialActive)
        {
            meshRenderer.material = newMaterial;
        }
        else
        {
            meshRenderer.material = oldMaterial;
        }
    }
}
