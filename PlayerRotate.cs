using UnityEngine;
using UnityEngine.UI;

public class PlayerRotate : MonoBehaviour
{
    [Header("PlayerRotate Properties")]
    [SerializeField] private Transform _mainCamera;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationLimit;
    public GameObject mouseSensitivity;

    protected float vertRot;


    public virtual void Rotate()
    {
        vertRot -= GetVerticalValue();
        vertRot = vertRot <= -_rotationLimit ? -_rotationLimit :
                  vertRot >= _rotationLimit ? _rotationLimit :
                  vertRot;

        RotateVertical();
        RotateHorizontal();
    }

    protected float GetVerticalValue() => Input.GetAxis("Mouse Y") * mouseSensitivity.GetComponent<Slider>().value * 200f * Time.deltaTime;
    protected float GetHorizontalValue() => Input.GetAxis("Mouse X") * mouseSensitivity.GetComponent<Slider>().value * 200f * Time.deltaTime;
    protected virtual void RotateVertical() => _mainCamera.localRotation = Quaternion.Euler(vertRot, 0f, 0f);
    protected virtual void RotateHorizontal() => transform.Rotate(Vector3.up * GetHorizontalValue());
}
