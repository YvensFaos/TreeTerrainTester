using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ArrayTester : MonoBehaviour
{
    [SerializeField]
    private List<float> values;
    [SerializeField] 
    private MeshRenderer selfRenderer;
    [SerializeField]
    private Material mat;
    [SerializeField] 
    private int number;

    private static readonly int Array = Shader.PropertyToID("_MyFloatArray");
    private static readonly int Number = Shader.PropertyToID("_Number");

    private void Update()
    {
        mat = selfRenderer.material;
        mat.SetInt(Number, number);
        mat.SetFloatArray(Array, values);
    }
}
