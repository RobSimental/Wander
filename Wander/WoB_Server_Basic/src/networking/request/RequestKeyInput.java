package networking.request;

// Java Imports
import java.io.IOException;

import core.GameClient;
import core.GameServer;
// Other Imports
import networking.response.ResponseKeyInput;
import utility.DataReader;
import utility.Log;


/**
 * The RequestHeartbeat class is mainly used to release all pending responses
 * the client. Also used to keep the connection alive.
 */
public class RequestKeyInput extends GameRequest {

    public enum Keys{
        None,
        up,
        down,
        left,
        right
    }

    private int id;

    private int key;

    private float x;

    public float xposition = 0f;

    private ResponseKeyInput responseKeyInput;
    public RequestKeyInput() {
        responses.add(responseKeyInput = new ResponseKeyInput());
    }

    @Override
    public void parse() throws IOException {
        id = DataReader.readInt(dataInput);
        key = DataReader.readInt(dataInput);
    }

    @Override
    public void doBusiness() throws Exception {
        // if(key == Keys.None.ordinal()){
        //     return;
        // } 
        // if(key == Keys.left.ordinal()){
        //     xposition -= 0.1;
        // }
        // if(key == Keys.right.ordinal()){
        //     xposition += 0.1;
        // }
        responseKeyInput.setId(id);
        responseKeyInput.setX(x);
        for(GameClient player : GameServer.getInstance().getActiveThreads().values()){
            if(player != this.client){
                player.addResponseForUpdate(responseKeyInput);
            }
        }
    }

}
