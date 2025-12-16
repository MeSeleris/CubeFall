using UnityEngine;

public class Recolor : MonoBehaviour
{
    [SerializeField] private Material _defaultMaterial;

    private Renderer resource;

    public void SetRandomColor(Cube cube)
    {
        if (cube.TryGetComponent(out resource ) == false)
        {
            return;
        }

        Color color = Random.ColorHSV(0f, 1f, 0.7f, 1f, 0.7f, 1f);
        resource.material.color = color;
    }

    public void ResetToDefault(Cube cube)
    {
        if (cube.TryGetComponent(out Renderer renderer)  && _defaultMaterial != null)
        {
            renderer.material = _defaultMaterial;
        }
    }
}
