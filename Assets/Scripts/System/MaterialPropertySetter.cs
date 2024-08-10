using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialPropertySetter : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;

    public void SetValue(string name, float value)
    {
        var material = _renderer.material;
        material.SetFloat(name, value);
    }
}
