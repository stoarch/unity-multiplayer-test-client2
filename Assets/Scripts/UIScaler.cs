using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScaler : MonoBehaviour {

    Image _image;
    RawImage _rawImage;

    Rect _oldImageRect;
    Rect _oldRawImageRect;

    [SerializeField]
    float _originalScale = 0.1f;

	void Start () {
        _image = gameObject.GetComponent<Image>();
        _rawImage = gameObject.GetComponent<RawImage>();

        if (_image != null)
            _oldImageRect = _image.rectTransform.rect;

        if (_rawImage != null)
            _oldRawImageRect = _rawImage.rectTransform.rect;
	}

    public void SetScale(float newScale)
    {
        if(_image != null)
        {
            _image.rectTransform.sizeDelta = new Vector2(_oldImageRect.width * newScale/_originalScale, _oldImageRect.height * newScale/_originalScale);
        }

        if(_rawImage != null)
        {
            _rawImage.rectTransform.sizeDelta = new Vector2(_oldRawImageRect.width * newScale/_originalScale, _oldRawImageRect.height * newScale/_originalScale);
        }


    }
	
}
