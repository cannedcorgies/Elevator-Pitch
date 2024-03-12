using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ToggleSwitch : MonoBehaviour
{
    [SerializeField] RectTransform handleRectTransform;
    [SerializeField] Color bgActiveColor;
    // [SerializeField] Color handleActiveColor;
    Image bgImage;
    Color bgDefaultColor;
    Toggle toggle;
    Vector2 handlePosition;

    void Awake() {
        toggle = GetComponent<Toggle>();
        handlePosition = handleRectTransform.anchoredPosition;

        bgImage = handleRectTransform.parent.GetComponent<Image>();
        // handleImage = handleRectTransform.GetComponent<Image>();
        // Debug.Log(bgImage);

        bgDefaultColor = bgImage.color;
        // handleDefaultColor = handleImage.color;
        // Debug.Log(bgDefaultColor);

        toggle.onValueChanged.AddListener(OnSwitch);
        if (toggle.isOn) {
            OnSwitch(true);
        }
    }

    void OnSwitch(bool on) {
        // handleRectTransform.anchoredPosition = on ? handlePosition * -1 : handlePosition;
        handleRectTransform.DOAnchorPos(on ? handlePosition * -1 : handlePosition, .4f).SetEase(Ease.InOutBack);
        // bgImage.color = on ? bgActiveColor : bgDefaultColor;
        bgImage.DOColor(on ? bgActiveColor : bgDefaultColor, .6f);
        // handleImage.color = on ? handleActiveColor : handleDefaultColor;
        // Debug.Log(bgImage.color);
    }

    void OnDestroy() {
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}
