using UnityEngine;
using System.Collections;

using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

class TCPClientSingleton {
	private static TcpClient myTCPClient = null;
    public static TcpClient getTCPClient() {
        if (myTCPClient == null) {
            myTCPClient = new TcpClient(Constants.REMOTE_HOST, Constants.REMOTE_PORT);
        }
        return myTCPClient;
	}
}