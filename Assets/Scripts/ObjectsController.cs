using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsController : MonoBehaviour {

    List<GameObject> _objects = new List<GameObject>();
    List<ObjectStatusManager> _statuses = new List<ObjectStatusManager>();

    [SerializeField]
    int _maxObjects = 500;
    [SerializeField]
    GameObject _objectTemplate;
    [SerializeField]
    Transform _root;

	void Start () {
        MakeObjects();	
	}

    public void MakeObjects()
    {
        if(_objectTemplate == null)
        {
            Debug.LogError("Object template is not set");
            return;
        }

        if(_root == null)
        {
            Debug.LogError("Root transform is not set");
            return;
        }

        for (int i = 0; i < _maxObjects; i++)
        {
            var obj = Instantiate(_objectTemplate);
            obj.transform.parent = _root;
            _objects.Add(obj);

            var statusManager = obj.GetComponent<ObjectStatusManager>();

            if(statusManager == null)
            {
                Debug.LogWarning("Object template does not contain object status manager");
                continue;
            }

            _statuses.Add(statusManager);
            statusManager.SetRandomValues();
        }
    }

    public void DeleteObjects()
    {
        _statuses.Clear();

        foreach (var item in _objects)
        {
            Destroy(item);
        }
        _objects.Clear();
    }

    // 1. Check all objects changes
    // 2. Propagate changes towards application controller
    // 3. Apply changes from server
    void Update () {
		
	}
}
