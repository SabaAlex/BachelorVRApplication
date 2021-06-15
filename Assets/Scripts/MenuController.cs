using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class MenuController : MonoBehaviour
    {
        private void Start()
        {
            WebSocketController.Initialize();
            NetworkController.ConnectToServer();
        }
        public void ExitApplication()
        {
            NetworkController.DisconnectFromServer();
            Application.Quit();
        }

        public void StartCalibration()
        {
            MixAppVariables();
            EnvironmentVariablesClass.IsCalibrationMode = true;
            SceneManager.LoadScene("Level", LoadSceneMode.Single);
        }

        public async void StartGame()
        {
            EnvironmentVariablesClass.IsCalibrationMode = IntenseParameters.ParametersSet = RestParameters.ParametersSet = false;

            NetworkController.Init();

            while (EnvironmentVariablesClass.ParametersConfigured == false) ;

            NetworkController.SetParameters();

            /// wait for params to be set
            while (IntenseParameters.ParametersSet == false || RestParameters.ParametersSet == false) ;


            SceneManager.LoadScene("Level", LoadSceneMode.Single);
        }

        void MixAppVariables()
        {
            EnvironmentVariablesClass.LastDistanceCoefficient = Random.value;
            EnvironmentVariablesClass.SizeCoefficient = Random.value;
            EnvironmentVariablesClass.TimeCoefficient = Random.Range(1, 3);
            EnvironmentVariablesClass.ClosenessCoefficient = Random.value;
        }
    }
}
