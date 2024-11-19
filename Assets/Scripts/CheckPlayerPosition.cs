using UnityEngine;

public static class CheckPlayerPosition
{
    private static LayerMask lm = LayerMask.GetMask("Default");

    // Added "layerMask" as the parameter name for LayerMask
    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 playerDirection)
    {
        if (rigidbody.isKinematic)
        {
            return false;
        }

        float radius = 0.16f;
        float distance = 0.06f;  // Changed to positive to cast in the direction specified

        // Pass in the layerMask here
        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, playerDirection.normalized, distance);
        return hit.collider != null && hit.rigidbody != rigidbody; // Return true if the raycast hit an object and that object is not the same as the rigidbody itself.
    }

    // This method performs a Dot product test to determine if the other transform is in a specified direction.
    public static bool DotTest(this Transform transform, Transform other, Vector2 testDirection)
    {
        Vector2 playerDirection = other.position - transform.position;
        return Vector2.Dot(playerDirection.normalized, testDirection) > 0.5f;
    }
}
