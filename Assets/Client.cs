using System.Collections;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Client : MonoBehaviour {
	//Global variable
	TcpClient tcp;
	NetworkStream ns;
	Encoding enc = Encoding.UTF8;
	bool connectFlag = false;
	string text = "";

	// Use this for initialization
	void Start () {
		/*
		//Set timeout
		ns.ReadTimeout = 10000;
		ns.WriteTimeout = 10000;

		//Receive message
		System.IO.MemoryStream ms = new System.IO.MemoryStream();
		byte[] resBytes = new byte[256];
		int resSize = 0;
		do {
			resSize = ns.Read(resBytes, 0, resBytes.Length);
			if (resSize == 0) {
				Debug.Log("サーバが切断しました");
				break;
			}
			ms.Write(resBytes, 0, resSize);
		} while (ns.DataAvailable || resBytes[resSize - 1] != '\n');
		//Covert the received data to a character string
		string resMsg = enc.GetString(ms.GetBuffer(), 0, (int)ms.Length);
		ms.Close();
		//Delete '\n'
		resMsg = resMsg.TrimEnd('\n');
		Debug.Log(resMsg);
		*/
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI () {
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
}
