using UnityEngine;

public class CollisionRecolor : MonoBehaviour
{
    [SerializeField] private Material _defaultMaterial;

    private Renderer resource;
    private bool _isChanged = false;

    public void SetRandomColor(Cube cube)
    {
        if (cube.TryGetComponent<Renderer>(out resource ) == false && _isChanged == false)
        {
            return;
        }

        Color color = Random.ColorHSV(0f, 1f, 0.7f, 1f, 0.7f, 1f);
        resource.material.color = color;

        _isChanged = true;
    }

    public void ResetToDefault(Cube cube)
    {
        _isChanged = false;

        if (cube.TryGetComponent<Renderer>(out Renderer renderer)  && _defaultMaterial != null)
        {
            renderer.material = _defaultMaterial;
        }
    }
}
