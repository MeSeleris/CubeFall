using UnityEngine;
using System.Collections.Generic;

public class CollisionRecolor : MonoBehaviour
{
    [SerializeField] private Material _defaultMaterial;

    private readonly HashSet<Cube> _alreadyChanged = new HashSet<Cube>();
    public void ChangeColorOnce(Cube cube)
    {
        if (_alreadyChanged.Add(cube) == false)
            return;

        var renderer = cube.GetComponent<Renderer>();
        var instanceMaterial = new Material(renderer.sharedMaterial);
        instanceMaterial.color = Random.ColorHSV(0f, 1f, 0.7f, 1f, 0.7f, 1f);
        renderer.material = instanceMaterial;
    }

    public void ResetToDefault(Cube cube)
    {
        _alreadyChanged.Remove(cube);

        if (_defaultMaterial != null)
        {
            var renderer = cube.GetComponent<Renderer>();
            renderer.material = _defaultMaterial;
        }
    }
}
