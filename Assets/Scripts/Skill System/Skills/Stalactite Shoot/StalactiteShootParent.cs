using UnityEngine;

public class StalactiteShootBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject[] children = new GameObject[2];

    private void Update()
    {
        if (children[0] == null && children[1] == null)
        {
            Destroy(gameObject);
        }
    }
}