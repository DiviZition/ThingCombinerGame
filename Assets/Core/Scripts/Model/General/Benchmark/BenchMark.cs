using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

public class BenchMark : MonoBehaviour
{
    [SerializeField, Range(1, 1000000)] private int _iterations = 1;

    [SerializeField] private BenchMarkTest _test;

    private void OnValidate()
    {
        _test = this.GetComponent<BenchMarkTest>();
    }

    public void RunBenchmark()
    {
        Stopwatch stopWatch = Stopwatch.StartNew();
        stopWatch.Start();

        for (int i = 0; i < _iterations; i++)
        {
            _test.PerformTestAction();

            if(stopWatch.ElapsedMilliseconds > 10000)
            {
                UnityEngine.Debug.Log("Breaking benchmark*");
                UnityEngine.Debug.Log("Escaping possibility of too long waiting time");
                break;
            }
        }

        stopWatch.Stop();

        UnityEngine.Debug.Log($"Benchmark result is over than: {stopWatch.ElapsedMilliseconds} ms");
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(BenchMark))]
public class BenchMarkCustomEditor : Editor
{
    private BenchMark _benchmark;

    private void OnEnable()
    {
        _benchmark = (BenchMark)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Start benchmark"))
        {
            _benchmark.RunBenchmark();
        }
    }
}
#endif