using UnityEngine;

public class CollisionRecolor : MonoBehaviour
{
    [SerializeField] private Material _defaultMaterial;

    private bool _isChanged = false;

    public void ChangeColorOnce(Cube cube)
    {
        if (cube.TryGetComponent<Renderer>(out Renderer renderer) == false && _isChanged == false)
        {
            return;
        }


        Color color = Random.ColorHSV(0f, 1f, 0.7f, 1f, 0.7f, 1f);
        renderer.material.color = color;

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
