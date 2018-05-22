using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSelection : MonoBehaviour {

    const string OBJECT_TAG = "object";
    const int MAX_DISTANCE = 20;

    [SerializeField]
    Image _uiCursor;
    [SerializeField]
    RawImage _uiCursorBackup;//if we have no ui cursor
    [SerializeField]
    float _detectionRadius = 1.0f;
    [SerializeField]
    ImageColorChanger _colorChanger;

    public float DetectionRadius { get { return _detectionRadius; } set { _detectionRadius = value; } }

	void Start ()
    {
	}

    private void OnEnable()
    {
        //Cursor.visible = false;
    }

    private void OnDisable()
    {
        //Cursor.visible = true;
    }

    void Update ()
    {
        var pos = Input.mousePosition;
        if(_uiCursor != null)
            _uiCursor.transform.position = pos;
        else if(_uiCursorBackup != null)
            _uiCursorBackup.transform.position = pos;

        //if (Input.GetMouseButtonDown(0))
        {
            pos.z = Camera.main.transform.position.z * -1;

            var newPos = Camera.main.ScreenToWorldPoint(pos);

            Ray ray = Camera.main.ScreenPointToRay(pos);
            var hits = Physics.SphereCastAll(ray, _detectionRadius, MAX_DISTANCE);

            if (hits.Length > 0)
            {
                foreach (var hit in hits)
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.tag == OBJECT_TAG)
                        {
                            ChangeColorFor(hit.collider.gameObject);
                        }
                    }
                }
            }
        }
	}

    private void ChangeColorFor(GameObject gameObject)
    {
        if(_colorChanger == null)
        {
            Debug.LogError("ImageColorChanger not set");
            return;
        }

        var objectStatus = gameObject.GetComponent<ObjectStatusManager>();
        if(objectStatus == null)
        {
            Debug.LogWarningFormat("Object {0} has no ObjectStatusManager", gameObject);
            return;
        }

        objectStatus.SetColor(_colorChanger.SelectionColor);
    }
}
