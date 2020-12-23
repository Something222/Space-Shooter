using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] private int startingWave = 0;


    [SerializeField] bool looping = false;
    //Bug start happens before the starting wave is reinitialized

    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
    }

 private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex=0;waveIndex<waveConfigs.Count;++waveIndex)
        {
            yield return StartCoroutine(SpawnAllEnemiesInWave(waveConfigs[waveIndex]));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        
        for (int i = 0; i < waveConfig.NumberOfEnemies; i++)
        {
            waveConfig.EnemyPrefab.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            Instantiate(
               waveConfig.EnemyPrefab,
               waveConfig.GetWaypoints()[0].transform.position,
               Quaternion.identity);
            yield return new WaitForSeconds(waveConfig.TimeBetweenSpawns);
        }
        
    }
}
