using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollisionDetector2D : CollisionDetector 
{
    private void OnTriggerEnter2D(Collider2D other) => Collision(other.gameObject);
    private void OnCollisionEnter2D(Collision2D collision) => Collision(collision.gameObject);

    private void OnTriggerExit2D(Collider2D other) => ColliderLeft(other.gameObject);
    private void OnCollisionExit2D(Collision2D collision) => ColliderLeft(collision.gameObject);
}