using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageColorChanger : MonoBehaviour {

    [SerializeField]
    Color _selectionColor = Color.blue;

    public Color SelectionColor { get { return _selectionColor; } }

    Image _image;



    private void Start()
    {
        _image = gameObject.GetComponent<Image>(); 
    }

    public void SetSelectionBlue()
    {
        _selectionColor = Color.blue;
        _image.color = _selectionColor;
    }

    public void SetSelectionRed()
    {
        _selectionColor = Color.red;
        _image.color = _selectionColor;
    }

    public void SetSelectionGreen()
    {
        _selectionColor = Color.green;
        _image.color = _selectionColor;
    }

    public void SetSelectionColor(float red, float green, float blue)
    {
        _selectionColor = new Color(red,green,blue);
    }
}
