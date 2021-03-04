using UnityEngine;

using System;

public class RequestMovement : NetworkRequest {

	public RequestMovement() {
		request_id = Constants.CMSG_MOVE;
	}
	
	public void send(string username, string locationX, string locationY) {
	    packet = new GamePacket(request_id);
		packet.addString(username);
		packet.addString(locationX);
		packet.addString(locationY);
	}
}