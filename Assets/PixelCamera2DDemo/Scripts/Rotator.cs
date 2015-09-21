using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private float degreesPerSecond = 20f;

	private void Update()
    {
        transform.Rotate(Vector3.forward * degreesPerSecond * Time.deltaTime);
    }
}
