using UnityEngine;

public class PowerupSpawnerScript : MonoBehaviour
{
    [SerializeField] private RectTransform _spawnArea;
    [SerializeField] private float _spawnTime; 
    [SerializeField] PowerupScript[] _powerups;
    private float _counter;
    private void Update() {
        _counter += Time.deltaTime;
        if (_counter >= _spawnTime) {
            _counter -= _spawnTime;
            PowerupScript powerup = _powerups[Random.Range(0, _powerups.Length)];
            Vector3 spawnPos = _spawnArea.position + new Vector3(Random.Range(0, _spawnArea.localScale.x), Random.Range(0, _spawnArea.localScale.y));
            GameObject.Instantiate(powerup, spawnPos, Quaternion.identity);
        }
    }
}
