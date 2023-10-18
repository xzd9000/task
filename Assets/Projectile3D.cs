using UnityEngine;

public sealed class Projectile3D : Projectile
{
    private void OnTriggerEnter(Collider other) => Collision(other.gameObject);
    private void OnCollisionEnter(Collision collision) => Collision(collision.gameObject);
}
