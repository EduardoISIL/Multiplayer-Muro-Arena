using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [Header("Values")]
    [SerializeField, Tooltip("How fast is going to move")] private float Intensity = 1;
    [SerializeField, Tooltip("value in squares in the scene")] private float Lenght = 3;
    private int direction = 1;
    enum PlatformState { IsHorinzontal, IsVertical, IsDiagonal1, IsDiagonal2 }
    [SerializeField] private PlatformState state = PlatformState.IsHorinzontal;

    private Vector2 pos;
    private Transform _transform;
    private float InitialX;
    private float InitialY;
    void Start()
    {
        _transform = GetComponent<Transform>();
        InitialX = _transform.position.x;
        InitialY = _transform.position.y;
    }
    void Update()
    {
        switch (state)
        {
            case PlatformState.IsHorinzontal:
                pos = _transform.position;
                pos.x = InitialX + (Mathf.PingPong(Time.time * Intensity, Lenght)) * direction;
                _transform.position = pos;
                break;

            case PlatformState.IsVertical:
                pos = _transform.position;
                pos.y = InitialY + (Mathf.PingPong(Time.time * Intensity, Lenght)) * direction;
                _transform.position = pos;
                break;

            case PlatformState.IsDiagonal1:
                pos = _transform.position;
                pos.y = InitialY + (Mathf.PingPong(Time.time * Intensity, Lenght)) * direction;
                pos.x = InitialX + (Mathf.PingPong(Time.time * Intensity, Lenght)) * direction;
                _transform.position = pos;
                break;

            case PlatformState.IsDiagonal2:
                pos = transform.position;
                pos.y = InitialY + (Mathf.PingPong(Time.time * Intensity, Lenght)) * direction;
                pos.x = InitialX + (Mathf.PingPong(Time.time * Intensity, Lenght)) * direction * -1;
                _transform.position = pos;
                break;
        }
    }
}
