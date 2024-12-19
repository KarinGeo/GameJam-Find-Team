using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;
using Unity.VisualScripting;

public partial class GameplayManager
{
    private class WaveManager
    {
        private List<WaveSO> _waves;
        private List<List<Vector3>> _paths;
        protected MonoBehaviour s_CoroutineRunner;
        private List<Enemy> _enemys;

        private bool _isSpawn;

        public WaveManager()
        { 

        }

        public void SetCoroutineRunner(MonoBehaviour obj)
        {
            this.s_CoroutineRunner = obj;
        }

        public void Set(List<List<Vector3>> paths, LevelSO data)
        {
            this._isSpawn = false;
            this._paths = paths;
            this._waves = data.Waves;
            this._enemys = new List<Enemy>();


            GameEvents.StartSpawnEnemy += GameEvents_StartSpawnEnemy;
        }

        public void ResetSelf()
        {
  
            this._isSpawn = false;
            this._paths = null;
            this._waves = null;
            this._enemys = new List<Enemy>();
            GameEvents.StartSpawnEnemy -= GameEvents_StartSpawnEnemy;
        }


        //Ð´³ÉÊÂ¼þ
        IEnumerator EnemySpawn()
        {
            foreach (WaveSO wave in _waves)
            {
                for (int i = 0; i < wave.Count; i++)
                {
                    Vector3 spawnPoint = _paths[wave.PathIndex][0] - new Vector3(0, 0, 1);
                    GameObject obj = Object.Instantiate(wave.Enemy.EnemyPrefab, spawnPoint, Quaternion.identity);
                    Enemy enemy = obj.AddComponent<Enemy>();
                    _enemys.Add(enemy);
                    enemy.Init(_paths[wave.PathIndex], wave.Enemy, wave.EnemyLevel);
                    if (i != wave.Count - 1)
                    {
                        yield return new WaitForSeconds(wave.Frequency);
                    }
                }
                yield return new WaitForSeconds(wave.NextWaveSpan);
            }

            while (!AllEnemyDie())
            {
                yield return null;
            }

            GameEvents.GameWin?.Invoke();
        }

        public void GameEvents_StartSpawnEnemy()
        {
            if (!_isSpawn)
            {
                s_CoroutineRunner.StartCoroutine(EnemySpawn());
                _isSpawn = true;
            }
        }

        public bool AllEnemyDie()
        {
            foreach (Enemy enmey in _enemys)
            {
                if (enmey.IsDie == false)
                {
                    return false;
                }
            }

            return true;
        }

    }

}
