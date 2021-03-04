using UnityEngine;

using System;

public class ResponseSpawnEventArgs : ExtendedEventArgs {

	public short status { get; set; }
	public int id { get; set; }
	
	public ResponseSpawnEventArgs() {
		event_id = Constants.SMSG_SPAWN;
	}
}

public class ResponseSpawn : NetworkResponse {
	
	private short status;
    private int id;
	public ResponseSpawn() {

	}
	
	public override void parse() {
		status = DataReader.ReadShort(dataStream);
		if (status == 0) {
			id = DataReader.ReadInt(dataStream);
		}
	}
	
	public override ExtendedEventArgs process() {
		ResponseSpawnEventArgs args = null;
		if (status == 0) {
			args = new ResponseSpawnEventArgs();
			args.status = status;
			args.id = id;
		}
		return args;
	}
}