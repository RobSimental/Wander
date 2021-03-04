using UnityEngine;
using System;

public class ResponseMoveEventArgs : ExtendedEventArgs {

    public string username { get; set; }
    public string locationX { get; set; }
	public string locationY { get; set; }
	public ResponseMoveEventArgs() {
		event_id = Constants.SMSG_MOVE;
	}

}

public class ResponseMovement : NetworkResponse {

    private string username;
    private string locationX;
	private string locationY;
    

	public ResponseMovement() {
	}

	public override void parse() {
        username = DataReader.ReadString(dataStream);
        locationX = DataReader.ReadString(dataStream);
		locationY = DataReader.ReadString(dataStream);
	}

	public override ExtendedEventArgs process() {
		ResponseMoveEventArgs args = null;
		args = new ResponseMoveEventArgs();
		args.username = username;
		args.locationX = locationX;
		args.locationY = locationY;
		return args;
	}

}