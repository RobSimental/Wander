package networking.response;

// Other Imports
import metadata.Constants;
import utility.GamePacket;

/**
 * The ResponseLogin class contains information about the authentication
 * process.
 */
public class ResponseRegistration extends GameResponse {

    private short status;
    private String username;
    public ResponseRegistration() {
        responseCode = Constants.SMSG_REG;
    }

    @Override
    public byte[] constructResponseInBytes() {
        GamePacket packet = new GamePacket(responseCode);
        packet.addShort16(status);
        packet.addString(username);
        return packet.getBytes();
    }

    public void setStatus(short status) {
        this.status = status;
    }

    public void setUsername(String username) {
        this.username = username;
    }

}