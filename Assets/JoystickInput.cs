using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class JoystickInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Canvas _canvas;
    private RectTransform _rect;

    [SerializeField] Vector3 _direction;
    [SerializeField] float _magnitude;
    [SerializeField] RectTransform _tap;

    private bool _tapExists;
    private int _pointerIndex = -1;
    private float _radius;
    private Vector3 _preNormalized;
    private Vector3 _touch;

    public Vector3 direction => _direction;
    public float magnitude => _magnitude;

    private void Awake()
    {
        _canvas = GetComponentInParent<Canvas>();
        _rect = GetComponent<RectTransform>();
        _tapExists = _tap != null;
    }

    // Update is called once per frame
    void Update()
    {
        if (_pointerIndex >= 0)
        {
            _touch = Input.GetTouch(_pointerIndex).position;

            _preNormalized = _touch - _rect.position;
            _radius = _rect.sizeDelta.x / 2f * _rect.localScale.x * _canvas.scaleFactor;
            _direction = Vector3.Normalize(_preNormalized);
            _magnitude = _preNormalized.sqrMagnitude / (_direction * _radius).sqrMagnitude;
            _magnitude = Mathf.Clamp01(_magnitude);

            if (_tapExists) _tap.localPosition = _direction * _radius * _magnitude / _rect.localScale.x;
        }
        else
        {
            _magnitude = 0f;
            _direction = Vector3.zero;
            if (_tapExists) _tap.localPosition = Vector3.zero;
        }
    }

    public void OnPointerDown(PointerEventData eventData) => _pointerIndex = eventData.pointerId;

    public void OnPointerUp(PointerEventData eventData) => _pointerIndex = -1;
}
