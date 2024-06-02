using UnityEngine;

public class ABreezeBeingToldBehaviour : MonoBehaviour
{
    [SerializeField] private Transform windwheel;

    void Update()
    {
        windwheel.transform.Rotate(0, 0, -10);
    }
}