using UnityEngine;

public sealed class Melee2D : Melee
{
    private void OnTriggerEnter2D(Collider2D other) => Collision(other.gameObject);
    private void OnCollisionEnter2D(Collision2D collision) => Collision(collision.gameObject);
}
