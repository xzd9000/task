using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollisionDetector3D : CollisionDetector 
{
    private void OnTriggerEnter(Collider other) => Collision(other.gameObject);
    private void OnCollisionEnter(Collision collision) => Collision(collision.gameObject);

    private void OnTriggerExit(Collider other) => ColliderLeft(other.gameObject);
    private void OnCollisionExit(Collision collision) => ColliderLeft(collision.gameObject);
}