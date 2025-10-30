using UnityEngine;

public class Roller : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(100 * Time.deltaTime, 0, 0);
    }
}
