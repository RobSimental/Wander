package networking.response;

// Other Imports
import metadata.Constants;
import utility.GamePacket;

/**
 * The ResponseLogin class contains information about the authentication
 * process.
 */
public class ResponseKeyInput extends GameResponse {


    private int id;
    private float x;

    public ResponseKeyInput() {
        responseCode = Constants.SMSG_KEY;
    }

    @Override
    public byte[] constructResponseInBytes() {
        GamePacket packet = new GamePacket(responseCode);
        packet.addInt32(id);
        packet.addFloat(x);
        return packet.getBytes();
    }

    public void setId(int id) {
        this.id = id;
    }
    public void setX(float x){
        this.x = x;
    }

}