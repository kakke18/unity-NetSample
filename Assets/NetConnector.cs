using System.Collections;
using System.Net;
using UnityEngine;

public class NetConnector : MonoBehaviour {
	private string myIP = "";
	private string servIP = "";
	private bool isConnected = false;

	// Use this for initialization
	void Start () {
		string hostname = Dns.GetHostName();

		IPAddress[] adrList = Dns.GetHostAddresses(hostname);
		foreach (IPAddress address in adrList) {
			myIP = address.ToString();
			servIP= myIP;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI () {
		if (!isConnected) {
			if (GUI.Button(new Rect(10, 10, 200, 30), "ゲームサーバーになる")) {

			}

			servIP = GUI.TextField(new Rect(10, 50, 200, 30), servIP);

			if (GUI.Button(new Rect(10, 80, 200, 30), "上のゲームサーバーに接続")) {
				if (Network.InitializeServer(20, 25000, false) == NetworkConnectionError.NoError) {
					procConnect();
				}
				else {
					Debug.Log("ゲームサーバー初期化エラー");
				}
			}
		}

	}

	private void procConnect() {
		isConnected = true;
	}
}
