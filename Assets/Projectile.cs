using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] float _damage;
    [SerializeField] Vector3 _speed;
    [SerializeField] Space _space;
    [SerializeField] bool _destroyOnHit = true;

    private bool _active;

    private void Update()
    {
        if (_active) transform.Translate(_speed * Time.deltaTime, _space);      
    }

    public virtual void Attack(Character target)
    {
        Projectile proj = Instantiate(this, transform.position, transform.rotation);
        proj.gameObject.SetActive(true);
        RotateProjTowards(proj, target.transform.position);
        proj._active = true;
    }

    protected void RotateProjTowards(Projectile proj, Vector3 position)
    {
        Vector3 normalized = (position - transform.position).normalized;
        proj.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(normalized.y, normalized.x) * Mathf.Rad2Deg);
    }

    protected virtual void Collision(GameObject hit)
    {
        TryHit(hit);
        if (_destroyOnHit) Destroy(gameObject);
    }

    protected void TryHit(GameObject obj) { if (obj.TryGetComponent(out Character character)) character.Hit(_damage); }
}