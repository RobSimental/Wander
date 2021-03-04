using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestLobbies : NetworkRequest
{
    public RequestLobbies() {
		request_id = Constants.CMSG_LOBBY;
	}
    public void send() {
	    packet = new GamePacket(request_id);
	}
}
