using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSelection : MonoBehaviour {

    const string OBJECT_TAG = "object";

    [SerializeField]
    Image _uiCursor;
    [SerializeField]
    ImageColorChanger _colorChanger;

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
        _uiCursor.transform.position = pos;

        if (Input.GetMouseButtonDown(0))
        {
            pos.z = Camera.main.transform.position.z * -1;

            var newPos = Camera.main.ScreenToWorldPoint(pos);

            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit = new RaycastHit();
            Physics.Raycast(ray, out hit, 100);

            if (hit.collider != null)
            {
                if (hit.collider.tag == OBJECT_TAG)
                {
                    ChangeColorFor(hit.collider.gameObject);
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
