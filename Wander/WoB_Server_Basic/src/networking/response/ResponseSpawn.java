package networking.response;

// Other Imports
import metadata.Constants;
import utility.GamePacket;

/**
 * The ResponseLogin class contains information about the authentication
 * process.
 */
public class ResponseSpawn extends GameResponse {

    private short status;
    private int id;
    public ResponseSpawn() {
        responseCode = Constants.SMSG_SPAWN;
    }

    @Override
    public byte[] constructResponseInBytes() {
        GamePacket packet = new GamePacket(responseCode);
        packet.addShort16(status);
        packet.addInt32(id);
        return packet.getBytes();
    }

    public void setStatus(short status) {
        this.status = status;
    }

    public void setId(int id) {
        this.id = id;
    }
    

}