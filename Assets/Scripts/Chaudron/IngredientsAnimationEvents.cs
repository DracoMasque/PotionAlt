using UnityEngine;

public class IngredientsAnimationEvents : MonoBehaviour
{
    void DisableRigidBodyComponents()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    void EnableRigidBodyComponents()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
