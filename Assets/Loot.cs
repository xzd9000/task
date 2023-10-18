using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class Loot : MonoBehaviour
{
    [SerializeField] GameObject[] _drop = new GameObject[0];
    [SerializeField] int _dropCount;
    [SerializeField] bool random;

    private Character _character;

    private void Awake() => _character = GetComponent<Character>();

    private void OnEnable() => _character.OnDeath += Drop;
    private void OnDisable() => _character.OnDeath -= Drop;

    private void Drop()
    {
        for (int i = 0, d = random ? Random.Range(0, _drop.Length) : 0; 
                            random ? i < _dropCount                : i < _drop.Length;
                 i++,   d = random ? Random.Range(0, _drop.Length) : d++)
        
            Instantiate(_drop[d], transform.position, transform.rotation);        
    }
}
