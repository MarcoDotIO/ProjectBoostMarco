using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This is a friendly object.");
                break;

            case "Enemy":
                Debug.Log("This is an enemy object.");
                break;

            case "Finish":
                Debug.Log("This is the finish object.");
                break;
        }
    }
}
