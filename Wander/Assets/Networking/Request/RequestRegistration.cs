using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestRegistration : NetworkRequest
{
    // Start is called before the first frame update
    public RequestRegistration() {
		request_id = Constants.CMSG_REG;
	}

    public void send(string username, string password, string confirmedPassword) {
	    packet = new GamePacket(request_id);
		packet.addString(Constants.CLIENT_VERSION);
		packet.addString(username);
		packet.addString(password);
        packet.addString(confirmedPassword);
	}
}
