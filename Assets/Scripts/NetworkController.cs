using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    public static class NetworkController
    {
        static readonly HttpClient client = new HttpClient();

        #region WebSocket Region
        public static void ConnectToServer()
        {
            WebSocketController.Connect();
        }

        public static void DisconnectFromServer()
        {
            WebSocketController.Disconnect();
            WebSocketController.Close();
        }

        public static void SetParameters()
        {
            WebSocketController.GetMaxVariables();
            WebSocketController.GetMinVariables();
        }
        #endregion

        #region HTTP Region
        public static void StartCalibration()
        {
            SendStartCalibration();
        }

        public static void EndCalibration()
        {
            SendEndCalibration();
        }

        public static void Init()
        {
            SendInit();
        }

        static async Task SendStartCalibration()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://192.168.0.101:8080/start_calibration");

                response.EnsureSuccessStatusCode();

                var code = response.StatusCode;

                if (code != System.Net.HttpStatusCode.OK)
                    throw new HttpRequestException("Request went bad");
            }
            catch (HttpRequestException e)
            {
                Debug.Log(e.Message);
            }

        }

        static async Task SendEndCalibration()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://192.168.0.101:8080/end_calibration");

                response.EnsureSuccessStatusCode();

                var code = response.StatusCode;

                if (code != System.Net.HttpStatusCode.OK)
                    throw new HttpRequestException("Request went bad");
            }
            catch (HttpRequestException e)
            {
                Debug.Log(e.Message);
            }
        }

        static async Task SendInit()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://192.168.0.101:8080/init");

                response.EnsureSuccessStatusCode();

                var code = response.StatusCode;

                if (code != System.Net.HttpStatusCode.OK)
                    throw new HttpRequestException("Request went bad");
            }
            catch (HttpRequestException e)
            {
                Debug.Log(e.Message);
            }
        }

        #endregion
    }
}
