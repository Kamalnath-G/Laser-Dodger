using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _smoothSpeed = 10f;
    [SerializeField] Vector3 _positionOffset = new Vector3(0, 10f, -9f);
    [SerializeField] Vector3 _rotationOffset = new Vector3(50f, 0, 0);


    void LateUpdate()
    {
        UpdateCameraMovement();
    }

    void UpdateCameraMovement()
    {
        if (_target == null)
            return;

        Vector3 desiredPosition = _target.position + _positionOffset;
        Quaternion desiredRotation = Quaternion.Euler(_target.rotation.eulerAngles + _rotationOffset);


        transform.position = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, _smoothSpeed * Time.deltaTime);

        transform.LookAt(_target);
    }
    
}