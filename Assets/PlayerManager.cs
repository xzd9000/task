using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] JoystickInput _joystick;
    [SerializeField] ButtonsInput _buttons;
    [SerializeField] InventoryController _inventory;
    [SerializeField] SaveLoad _saveLoad;
    [SerializeField] RectTransform _deathScreen;
    [SerializeField] float _sceneReloadTime = 10f;

    private Scene _currentScene;

    private void Awake() => _currentScene = SceneManager.GetActiveScene();

    private void OnEnable()
    {
        _buttons.OnShootButtonTap += ShootButtonTap;
        _buttons.OnInventoryButtonTap += InventoryButtonTap;
        _player.OnDeath += PlayerDied;
    }
    private void OnDisable()
    {
        _buttons.OnShootButtonTap -= ShootButtonTap;
        _buttons.OnInventoryButtonTap -= InventoryButtonTap;
        _player.OnDeath -= PlayerDied;
    }

    private void Update()
    {
        if (_joystick.magnitude > 0f) _player.Move(_joystick.direction, _joystick.magnitude);

        if (Input.GetKey(KeyCode.Escape) && _inventory.gameObject.activeInHierarchy) _inventory.Hide(); 
    }

    private void ShootButtonTap() => _player.Attack();
    private void InventoryButtonTap() => _inventory.gameObject.SetActive(true);
    private void PlayerDied()
    {
        _saveLoad.save = false;
        _deathScreen.gameObject.SetActive(true);
        StartCoroutine(IEReload());
    }

    private IEnumerator IEReload()
    {
        yield return new WaitForSeconds(_sceneReloadTime);
        SceneManager.LoadScene(_currentScene.buildIndex, LoadSceneMode.Single);
    }
}
