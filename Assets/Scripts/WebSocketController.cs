using SocketIOClient;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Assets.Models;
using UnityEngine;
using System;

namespace Assets.Scripts
{
    public static class WebSocketController
    {
        static Client webSocket;

		public static void Close()
		{
			if (webSocket != null)
			{
				webSocket.Dispose(); // close & dispose of socket client
			}
		}

		public static void Initialize()
        {
			webSocket = new Client("http://192.168.0.101:8080/");

			webSocket.On("get parameters", (_) =>
			{
				AddParameters();
			});

			webSocket.On("configured parameters", (data) =>
			{
				try
				{
					var jsonData = data.Json.ToJsonString();
					JSONData dataParsed = JsonConvert.DeserializeObject<JSONData>(jsonData);

					if (dataParsed.data == "")
						throw new Exception("No Data");
					
					EnvironmentVariablesClass.SetListOfCoefficients(dataParsed.GetArrayData());
				}
				catch (Exception e)
				{
					Debug.Log(e.Message);
				}
			});

			webSocket.On("connection response", (data) => {
                try
                {
					var jsonData = data.Json.ToJsonString();
					JSONData dataParsed = JsonConvert.DeserializeObject<JSONData>(jsonData);

					if (dataParsed.data != "Connected")
						throw new Exception("Bad connection");
				}
                catch(Exception e)
                {
					Debug.Log(e.Message);
				}
				
			});

			webSocket.On("set max variables", (data) =>
			{
				try
				{
					var jsonData = data.Json.ToJsonString();
					JSONData dataParsed = JsonConvert.DeserializeObject<JSONData>(jsonData);

					if (dataParsed.data == "")
						throw new Exception("No Data");

					IntenseParameters.SetListOfCoefficients(dataParsed.GetArrayData());
				}
				catch (Exception e)
				{
					Debug.Log(e.Message);
				}
			});

			webSocket.On("set min variables", (data) =>
			{
				try
				{
					var jsonData = data.Json.ToJsonString();
					JSONData dataParsed = JsonConvert.DeserializeObject<JSONData>(jsonData);

					if (dataParsed.data == "")
						throw new Exception("No Data");

					RestParameters.SetListOfCoefficients(dataParsed.GetArrayData());
				}
				catch (Exception e)
				{
					Debug.Log(e.Message);
				}
			});

			webSocket.Connect();
		}

        #region Emiters

        public static void Connect()
        {
			webSocket.Emit("connect", "");
        }

		public static void Disconnect()
		{
			webSocket.Emit("disconnect", "");
		}

		public static void AddParameters()
        {
			string[] stringList = EnvironmentVariablesClass.GetListOfCoefficients();

			JSONData dataToSend = new JSONData()
			{
				data = JsonConvert.SerializeObject(stringList),
			};

			webSocket.Emit("add parameters", dataToSend);
        }

		public static void GetMinVariables()
        {
			string[] stringList = EnvironmentVariablesClass.GetListOfCoefficients();

			JSONData dataToSend = new JSONData()
			{
				data = JsonConvert.SerializeObject(stringList),
			};

			webSocket.Emit("get min variables", dataToSend);
        }

		public static void GetMaxVariables()
		{
			string[] stringList = EnvironmentVariablesClass.GetListOfCoefficients();

			JSONData dataToSend = new JSONData()
			{
				data = JsonConvert.SerializeObject(stringList),
			};

			webSocket.Emit("get max variables", dataToSend);
		}

		public static void GetParameters()
		{
			string[] stringList = EnvironmentVariablesClass.GetListOfCoefficients();

			JSONData dataToSend = new JSONData()
			{
				data = JsonConvert.SerializeObject(stringList),
			};

			webSocket.Emit("get parameters", dataToSend);
		}

        #endregion
    }
}
