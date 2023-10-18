using UnityEngine;

public sealed class Melee3D : Melee
{
    private void OnTriggerEnter(Collider other) => Collision(other.gameObject);
    private void OnCollisionEnter(Collision collision) => Collision(collision.gameObject);
}
