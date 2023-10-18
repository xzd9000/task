using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIHPBar : MonoBehaviour
{
    [SerializeField] Character _character;
    [SerializeField] RectTransform _hp;

    private void OnEnable()
    {
        _character.OnHit += UpdateBars;
        UpdateBars(_character.hp, 0f);
    }
    private void OnDisable() => _character.OnHit -= UpdateBars;

    private void UpdateBars(float hp, float damage) => _hp.localScale = new Vector3(hp / _character.maxHP, 1f, 1f);
}
