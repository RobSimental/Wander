package networking.request;

// Java Imports
import java.io.IOException;

// Other Imports
import core.GameClient;
import core.GameServer;
import metadata.Constants;
import model.Player;
import networking.response.ResponseLogin;
import utility.DataReader;
import utility.Log;
import java.sql.*;
/**
 * The RequestLogin class authenticates the user information to log in. Other
 * tasks as part of the login process lies here as well.
 */

public class RequestLogin extends GameRequest {

    // Data
    private String version;
    private String username;
    private String password;
    // Responses
    private ResponseLogin responseLogin;

    public RequestLogin() {
        responses.add(responseLogin = new ResponseLogin());
    }

    @Override
    public void parse() throws IOException {
        version = DataReader.readString(dataInput).trim();
        username = DataReader.readString(dataInput).trim();
        password = DataReader.readString(dataInput).trim();
    }

    @Override
    public void doBusiness() throws Exception {
        Log.printf("User '%s' is connecting...", username);
        Player player = null;
        // Checks if the connecting client meets the minimum version required
        if (version.compareTo(Constants.CLIENT_VERSION) >= 0) {
            if (!username.isEmpty()) {
                // connect to db;
                try {
                    Class.forName("com.mysql.jdbc.Driver");
                } catch (ClassNotFoundException e) {
                    System.out.println("Where is your MySQL JDBC Driver?");
                    e.printStackTrace();
                    return;
                }
                Connection connection = null;
                try {
                    connection = DriverManager.getConnection("jdbc:mysql://" + "wanderdb.c4p7z07xl4sc.us-east-1.rds.amazonaws.com" + ":" + "3306" + "/" + "wander", "root", "awesomeganbold");
                } catch (SQLException e) {
                    System.out.println("Connection Failed!:\n" + e.getMessage());
                }
                if (connection != null) {
                    System.out.println("DB succesfully connected!");
                } else {
                    System.out.println("FAILURE! Failed to make connection!");
                }
                Log.printf("User '%s' entered passwd '%s'", username, password);
                String query = "SELECT * FROM Players WHERE username = '" + username + "'";
                Statement st = connection.createStatement();
                ResultSet rs = st.executeQuery(query);
                if(rs.next()){
                    if(username.equals(rs.getString(2)) && password.equals(rs.getString(3))){
                        Log.printf("-------------------");
                        player = new Player(rs.getInt(1), rs.getString(2), rs.getString(3));
                    } else {
                        player = null;
                    }
                } else {
                    Log.printf("Username Does not exist");
                }
                connection.close();
            }
            if (player == null) {
                responseLogin.setStatus((short) 1); // User info is incorrect
                Log.printf("User '%s' has failed to log in.", username);
            } else {
                player.setClient(client);
                if (client.getPlayer() == null || player.getID() != client.getPlayer().getID()) {
                    GameClient thread = GameServer.getInstance().getThreadByPlayerID(player.getID());
                    // If account is already in use, remove and disconnect the client
                    if (thread != null) {
                        responseLogin.setStatus((short) 2); // Account is in use
                        thread.removePlayerData();
                        thread.newSession();
                        Log.printf("User '%s' account is already in use.", username);
                    } else {
                        // Continue with the login process
                        GameServer.getInstance().setActivePlayer(player);
                        player.setClient(client);
                        // Pass Player reference into thread
                        client.setPlayer(player);
                        // Set response information
                        responseLogin.setStatus((short) 0); // Login is a success
                        responseLogin.setPlayer(player);
                        Log.printf("User '%s' has successfully logged in.", player.getUsername());
                    }
                }
            }
        } else {
            responseLogin.setStatus((short) 3); // Client version not compatible
            Log.printf("User '%s' has failed to log in. (v%s)", player.getUsername(), version);
        }
        Log.printf("'%s'", GameServer.getInstance().getActiveThreads().size());
    }



}
