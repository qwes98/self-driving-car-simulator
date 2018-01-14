using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class NetworkServerUI : MonoBehaviour {

	CrossPlatformInputManager.VirtualAxis m_HVAxis;
	CrossPlatformInputManager.VirtualAxis m_VVAxis;
	string horizontalAxisName = "Horizontal";
	string verticalAxisName = "Vertical";

	void OnGUI() {
		string ipaddress = Network.player.ipAddress;
		GUI.Box(new Rect(10, Screen.height - 60, 110, 50), ipaddress);
		GUI.Label(new Rect(25, Screen.height - 45, 110, 20), "Status: " + NetworkServer.active);
		GUI.Label(new Rect(26, Screen.height - 30, 110, 20), "Connected: " + NetworkServer.connections.Count);
	}
	// Use this for initialization
	void Start () {
		m_HVAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
		CrossPlatformInputManager.RegisterVirtualAxis(m_HVAxis);
		m_VVAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
		CrossPlatformInputManager.RegisterVirtualAxis(m_VVAxis);

		NetworkServer.Listen(25000);
		NetworkServer.RegisterHandler(888, ServerReceiveMessage);
	}
	
	private void ServerReceiveMessage(NetworkMessage message) {
		StringMessage msg = new StringMessage();
		msg.value = message.ReadMessage<StringMessage>().value;

		string[] deltas = msg.value.Split('|');
		m_HVAxis.Update(Convert.ToSingle(deltas[0]));
		m_VVAxis.Update(Convert.ToSingle(deltas[1]));
	}

	// Update is called once per frame
	void Update () {

	}
}
