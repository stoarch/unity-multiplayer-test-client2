using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStatusManager : MonoBehaviour {
    const int MIN_SIZE = 100;//bytes
    const int MAX_SIZE = 5 * 1024;//bytes

    const float MIN_X = -4.0f;
    const float MAX_X = 5.0f;

    const float MIN_Y = 0.0f;
    const float MAX_Y = 3.0f;

    const float MIN_Z = -6.0f;
    const float MAX_Z = 3.0f;

    const int UNKNOWN_COLOR = -1;

    [SerializeField]
    Color _color;
    [SerializeField]
    int _dataSize = MIN_SIZE;

    [SerializeField]
    Material[] _colorsMaterials;

    byte[] _data;

    System.Random _rnd;

    void Start()
    {
        GenerateData();

        CreateRandomGenerator();

        if(_dataSize == 0)
        {
            _dataSize = UnityEngine.Random.Range(MIN_SIZE, MAX_SIZE);
        }
    }

    private void CreateRandomGenerator()
    {
        _rnd = new System.Random();
    }

    void GenerateData()
    {
        _data = new byte[_dataSize];

        if (_rnd == null)
            CreateRandomGenerator();

        _rnd.NextBytes(_data);
    }

    internal void SetRandomValues()
    {
        SetRandomTransform();
        SetRandomColor();
        GenerateData();
    }

    private void SetRandomColor()
    {
        int id = UnityEngine.Random.Range(0, _colorsMaterials.Length);
        SetColor(id);
    }

    internal void SetColor(Color selectionColor)
    {
        int id = FindColor(selectionColor);
        if(id == UNKNOWN_COLOR)
        {
            Debug.LogWarningFormat("Color {0} not found in object {1}", selectionColor, gameObject);
            return;
        }

        SetColor(id); 
    }

    int FindColor(Color toFind)
    {
        for (int i = 0; i < _colorsMaterials.Length; i++)
        {
            if (_colorsMaterials[i].color == toFind)
                return i;
        }

        return UNKNOWN_COLOR;
    }

    private void SetColor(int id)
    {
        if (_colorsMaterials.Length == 0)
        {
            Debug.LogError("No colors specified to select from");
            return;
        }
        var renderer = gameObject.GetComponent<MeshRenderer>();
        renderer.material = _colorsMaterials[id];
    }

    private void SetRandomTransform()
    {
        float rx = UnityEngine.Random.Range(MIN_X, MAX_X);
        float ry = UnityEngine.Random.Range(MIN_Y, MAX_Y);
        float rz = UnityEngine.Random.Range(MIN_Z, MAX_Z);

        gameObject.transform.position = new Vector3(rx, ry, rz);
    }
}
