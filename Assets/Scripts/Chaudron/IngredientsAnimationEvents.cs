using UnityEngine;

public class IngredientsAnimationEvents : MonoBehaviour
{
    private void Start()
    {
        Appear();
    }
    private void DisableRigidBodyComponents()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void EnableRigidBodyComponents()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    void Appear()
    {
        GetComponent<Animation>().Play("AddIngredient");
    }
}
