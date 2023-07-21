using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    private Slider _uiSlider;

    bool firstMove = false;

    public static event Action<float> OnPointerDrag;

    private void Start()
    {
        _uiSlider = GameObject.FindGameObjectWithTag("InputCanvas").GetComponentInChildren<Slider>();
        _uiSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    public void ResetFirstMove()
    {
        firstMove = false;
    }

    public void OnSliderValueChanged(float value)
    {
        if (!firstMove)
        {
            firstMove = true;
            UIBehaviour.Instance.Play();
        }

        OnPointerDrag?.Invoke(value);
    }
}
