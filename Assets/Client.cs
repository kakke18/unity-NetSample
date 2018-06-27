using System.Collections;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

public class Client : MonoBehaviour {
	//Global variable
	TcpClient tcp;
	NetworkStream ns;
	MemoryStream ms;
	Encoding enc = Encoding.UTF8;
	bool connectFlag = false;
	bool receiveFlag = false;
	string text = "";
	string recMsg = "";

	// Use this for initialization
	void Start () {
		/*
		//Set timeout
		ns.ReadTimeout = 10000;
		ns.WriteTimeout = 10000;
		*/
	}
	
	// Update is called once per frame
	void Update () {
		receiveMessage();
	}

	void OnGUI () {
		//Set label style
		GUIStyle labelStyle = new GUIStyle();
		GUIStyleState styleState = new GUIStyleState();
		styleState.textColor = Color.black;
		labelStyle.normal = styleState;

		if (!connectFlag) {
			if (GUI.Button(new Rect(10, 30, 200, 30), "接続")) {
				connectServer();
			}
		}
		else {
			if (GUI.Button(new Rect(10, 30, 200, 30), "切断")) {
				disconnectServer();
			}

			text = GUI.TextField(new Rect(10, 70, 200, 30), text);
			
			if (GUI.Button(new Rect(10, 110, 200, 30), "送信") && text != "") {
				sendMessage(text);
			}
		}

		if (recMsg != "") {
			GUI.Label(new Rect(10, 150, 200, 30), recMsg, labelStyle);
		}
	}

	void connectServer () {
		string ipOrHost = "localhost";
		int port = 10000;
		connectFlag = true;

		Debug.Log("connect");
		//Create TcpClient and connect server
		tcp = new TcpClient(ipOrHost, port);
		//Get NetworkStream
		ns = tcp.GetStream();
	}

	void disconnectServer () {
		connectFlag = false;
		ns.Close();
		tcp.Close();
		Debug.Log("disconnect");
	}

	void sendMessage (string msg) {
		byte[] sendBytes = enc.GetBytes(msg + '\n');
		ns.Write(sendBytes, 0, sendBytes.Length);
		Debug.Log("send:" + msg);	
	}

	void receiveMessage () {
		byte[] recBytes = new byte[256];
		int recSize = 0;

		if (connectFlag) {
			//Receive message
			while (ns.DataAvailable) {
				ms = new MemoryStream();
				recSize = ns.Read(recBytes, 0, recBytes.Length);

				if (recSize == 0) {
					Debug.Log("サーバが切断しました");
					break;
				}

				ms.Write(recBytes, 0, recSize);

				if (recBytes[recSize - 1] == '\n') {
					receiveFlag = true;
					break;
				}
			}
			if (receiveFlag) {
				//Covert the received data to a character string
				recMsg = enc.GetString(ms.GetBuffer(), 0, (int)ms.Length);
				ms.Close();
				//Delete '\n'
				recMsg = recMsg.TrimEnd('\n');
				Debug.Log("receive:" + recMsg);
				receiveFlag = false;
			}
		}
	}
}
