using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Data
{
    [SerializeField]
    public int frame;
    [SerializeField]
    public float time;
    [SerializeField]
    public int triangles;
    [SerializeField]
    public int vertices;
    [SerializeField]
    public int batches;
    [SerializeField]
    public int drawCalls;
    [SerializeField]
    public int dynamicBatches;
    [SerializeField]
    public float frameTime;
    [SerializeField]
    public float renderTime;
    [SerializeField]
    public int instancedBatches;
    [SerializeField]
    public int shadowCasters;
    [SerializeField]
    public int staticBatches;
    [SerializeField]
    public int dynamicBatchedDrawCalls;
    [SerializeField] 
    public float combinedTime;
    [SerializeField] 
    public float differenceTime;
    [SerializeField]
    public string extra;
}

[Serializable]
public class DataList
{
    [SerializeField]
    public List<Data> data;

    public DataList()
    {
        data = new List<Data>();
    }

    public void AddToList(Data d) => data.Add(d);
}

public class Measurer : MonoBehaviour
{
    [SerializeField]
    private int stepsToRecord;
    [SerializeField, TextArea]
    private string extraNote;
    private int _frame;
    [SerializeField]
    private DataList dataList;

    [SerializeField] private bool destroyOnEnd;
    
    private void Start()
    {
        dataList = new DataList();
        _frame = 1;
    }

    private void LateUpdate()
    {
        if (_frame <= stepsToRecord)
        {
            var combined = UnityEditor.UnityStats.frameTime + UnityEditor.UnityStats.renderTime;
            var data = new Data
            {
                frame = _frame++,
                time = Time.deltaTime,
                triangles = UnityEditor.UnityStats.triangles,
                vertices = UnityEditor.UnityStats.vertices,
                batches = UnityEditor.UnityStats.batches,
                drawCalls = UnityEditor.UnityStats.drawCalls,
                dynamicBatches = UnityEditor.UnityStats.dynamicBatches,
                frameTime = UnityEditor.UnityStats.frameTime,
                renderTime = UnityEditor.UnityStats.renderTime,
                instancedBatches = UnityEditor.UnityStats.instancedBatches,
                shadowCasters = UnityEditor.UnityStats.shadowCasters,
                staticBatches = UnityEditor.UnityStats.staticBatches,
                dynamicBatchedDrawCalls = UnityEditor.UnityStats.dynamicBatches,
                combinedTime = combined,
                differenceTime = Time.deltaTime - combined, 
                extra = extraNote
            };
            // Debug.Log($"Add new dataList: {data} | {JsonUtility.ToJson(data)}.");
            dataList.AddToList(data);
        }
        else
        {
            var json = JsonUtility.ToJson(dataList);
            Debug.Log(json);
            var path = $"results-[{DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")}]-{extraNote}.json";
            System.IO.File.WriteAllText(path, json);
            Debug.Log($"Save dataList to: {path}");
            gameObject.SetActive(false);
            if (destroyOnEnd)
            {
                Destroy(gameObject); 
            }
        }
    }
}
