using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Melee : Projectile
{
    [SerializeField][Min(0.0001f)] float _deactivationDelay = 0.1f;

    public override void Attack(Character target)
    {
        RotateProjTowards(this, target.transform.position);
        gameObject.SetActive(true);
        StartCoroutine(IEAutoDeactivation());
    }
    protected override void Collision(GameObject hit)
    {
        TryHit(hit);
        gameObject.SetActive(false);
    }

    private IEnumerator IEAutoDeactivation()
    {
        yield return new WaitForSeconds(_deactivationDelay);
        gameObject.SetActive(false);
    }
}