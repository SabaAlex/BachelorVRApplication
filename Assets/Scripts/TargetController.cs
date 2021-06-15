using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TargetController : MonoBehaviour
{
    public GameObject SpawningArea;

    float timer = 0.0f;

    int seconds;
    int countedSeconds;
    int lastTimer;

    bool isRest = false;

    public Vector3 SizeCoefficients;

    // Start is called before the first frame update
    void Start()
    {
        if (EnvironmentVariablesClass.IsCalibrationMode)
            NetworkController.StartCalibration();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        lastTimer = seconds;
        seconds = (int)(timer % 60);

        if (EnvironmentVariablesClass.IsCalibrationMode)
            CalibrationMode();
        else
            GameMode();
    }

    void CalibrationMode()
    {
        if (seconds < 120)
        {
            if(lastTimer != seconds)
            {
                if (seconds % EnvironmentVariablesClass.TimeCoefficient == 0)
                {
                    SpawnNextTarget();
                }
            }
            if (seconds % 5 == 0)
            {
                MixAppVariables();
                ChangeSpawnArea();
            }
        }
        else
        {
            NetworkController.EndCalibration();
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }

    void MixAppVariables()
    {
        EnvironmentVariablesClass.LastDistanceCoefficient = Random.Range(1.5f, 0.5f);
        EnvironmentVariablesClass.SizeCoefficient = Random.Range(2f, 0.75f);
        EnvironmentVariablesClass.TimeCoefficient = Random.Range(1, 3);
        EnvironmentVariablesClass.ClosenessCoefficient = Random.Range(1.5f, 0.5f);
    }

    void GameMode()
    {
        if (seconds < 120 * 10)
        {
            ///a second passed
            if (lastTimer != seconds)
            {
                ++countedSeconds;
                if (seconds % EnvironmentVariablesClass.TimeCoefficient == 0)
                {
                    SpawnNextTarget();
                }

                if (isRest)
                {
                    if (countedSeconds > 20)
                    {
                        isRest = !isRest;
                        countedSeconds = 0;
                        ChangeToIntense();
                    }
                }
                else
                {
                    if (countedSeconds > 40)
                    {
                        isRest = !isRest;
                        countedSeconds = 0;
                        ChangeToRest();
                    }
                }
            }
        }
        else
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }

    private void ChangeToRest()
    {
        EnvironmentVariablesClass.LastDistanceCoefficient = RestParameters.LastDistanceCoefficient;
        EnvironmentVariablesClass.SizeCoefficient = RestParameters.SizeCoefficient;
        EnvironmentVariablesClass.TimeCoefficient = RestParameters.TimeCoefficient;
        EnvironmentVariablesClass.ClosenessCoefficient = RestParameters.ClosenessCoefficient;
    }

    private void ChangeToIntense()
    {
        EnvironmentVariablesClass.LastDistanceCoefficient = IntenseParameters.LastDistanceCoefficient;
        EnvironmentVariablesClass.SizeCoefficient = IntenseParameters.SizeCoefficient;
        EnvironmentVariablesClass.TimeCoefficient = IntenseParameters.TimeCoefficient;
        EnvironmentVariablesClass.ClosenessCoefficient = IntenseParameters.ClosenessCoefficient;
    }

    private void ChangeSpawnArea()
    {
        SpawningArea.GetComponent<SpawnObject>().UpdateCoefficients();
    }

    private void SpawnNextTarget()
    {
        SpawningArea.GetComponent<SpawnObject>().SpawnTarget();
    }
}
