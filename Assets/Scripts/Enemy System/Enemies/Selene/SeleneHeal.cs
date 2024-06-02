using UnityEngine;

public class SeleneHeal : MonoBehaviour
{

    Transform selene;

    void Start()
    {
        selene = transform.parent;
        transform.position = selene.position;
    }

    void Update()
    {
        if (selene != null)
        {
            transform.position = selene.position;
        }
        else
        {
            return;
        }
    }

    public void EndAnimation()
    {
        Destroy(gameObject);
    }
}
