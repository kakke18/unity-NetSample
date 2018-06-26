using System.Collections;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

public class Client : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string ipOrHost = "localhost";
		int port = 10000;

		TcpClient tcp = new TcpClient(ipOrHost, port);
		Debug.Log("success");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
