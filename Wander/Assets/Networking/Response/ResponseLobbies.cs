using UnityEngine;

using System;

public class ResponseLobbiesEventArgs : ExtendedEventArgs {

	public short status { get; set; }
	public string json { get; set; }
	public ResponseLobbiesEventArgs() {
		event_id = Constants.SMSG_LOBBY;
	}
}

public class ResponseLobbies : NetworkResponse {
	
	private short status;
	private string json;

	public ResponseLobbies() {

	}
	
	public override void parse() {
		status = DataReader.ReadShort(dataStream);
		if (status == 0) {
			json = DataReader.ReadString(dataStream);
		}
	}
	
	public override ExtendedEventArgs process() {
		ResponseLobbiesEventArgs args = null;
		if(status == 0){
			args = new ResponseLobbiesEventArgs();
			args.status = status;
			args.json = json;
		} 
		return args;
	}
}