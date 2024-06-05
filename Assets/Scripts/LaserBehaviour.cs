using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{

    float _moveSpeed = 3f; // Speed at which the laser moves
    float _maxDistance = 35f; // Maximum distance the laser will travel

    float _distanceTraveled = 0f;

    bool _isCrossedTheDistance = false;

    void Update()
    {
        // Calculate how far the laser has moved
        float distanceThisFrame = GetLaserSpeed() * Time.deltaTime;
        _distanceTraveled += distanceThisFrame;

        // Move the laser in its local forward direction
        transform.Translate(Vector3.right * distanceThisFrame, Space.Self);

        // Check if the laser has traveled the maximum distance
        if (_distanceTraveled >= _maxDistance)
        {
            _isCrossedTheDistance = true;
            // Destroy the laser or disable it as per your requirements
            Destroy(gameObject);
        }
    }

    public void SetLaserSpeed(float speed)
    {
        _moveSpeed = speed;
    }

    public float GetLaserSpeed()
    {
        return _moveSpeed;
    }

    public bool IsCrossedTheDistance()
    {
        return _isCrossedTheDistance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.Destroy();
            GameManager.Instance.isGameOver = true;
        }
    }

}