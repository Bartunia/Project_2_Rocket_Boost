using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPos;
    Vector3 endPos;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float movementSpeed = 1f;
    float movementFactor;

    void Start()
    {
        startingPos = transform.position;
        endPos = startingPos + movementVector;
    }

    void Update()
    {
        movementFactor = Mathf.PingPong(Time.time * movementSpeed, 1f);
        transform.position = Vector3.Lerp(startingPos, endPos, movementFactor);
    }
}
