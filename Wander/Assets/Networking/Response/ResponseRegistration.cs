using UnityEngine;

using System;

public class ResponseRegistrationEventArgs : ExtendedEventArgs {

	public short status { get; set; }
	public int user_id { get; set; }
	public string username { get; set; }
	
	public ResponseRegistrationEventArgs() {
		event_id = Constants.SMSG_REG;
	}
}

public class ResponseRegistration : NetworkResponse {
	
	private short status;
	private string username;

	public ResponseRegistration() {
	}
	
	public override void parse() {
		status = DataReader.ReadShort(dataStream);
		if (status == 0) {
			username = DataReader.ReadString(dataStream);
		}
	}
	
	public override ExtendedEventArgs process() {
		ResponseRegistrationEventArgs args = null;
		if(status == 0){
			args = new ResponseRegistrationEventArgs();
			args.status = status;
			args.username = username;
		} else {
			args = new ResponseRegistrationEventArgs();
			args.status = status;
		}
		return args;
	}
}