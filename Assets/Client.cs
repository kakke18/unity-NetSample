using System.Collections;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Client : MonoBehaviour {
	// Use this for initialization
	void Start () {
		string ipOrHost = "localhost";
		int port = 10000;

		//Create TcpClient and connect server
		TcpClient tcp = new TcpClient(ipOrHost, port);

		//Get NetworkStream
		NetworkStream ns = tcp.GetStream();

		//Set timeout
		ns.ReadTimeout = 10000;
		ns.WriteTimeout = 10000;

		//Send message
		Encoding enc = Encoding.UTF8;
		string textField = "test";
		byte[] sendBytes = enc.GetBytes(textField + '\n');
		ns.Write(sendBytes, 0, sendBytes.Length);
		Debug.Log(textField);
		
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

		//Close
		ns.Close();
		tcp.Close();
		Debug.Log("切断しました");
	}
	
	// Update is called once per frame
	void Update () {

	}
}
