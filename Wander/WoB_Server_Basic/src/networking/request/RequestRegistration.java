package networking.request;

// Java Imports
import java.io.IOException;
// Other Imports
import metadata.Constants;
import networking.response.ResponseRegistration;
import utility.DataReader;
import utility.Log;
import java.sql.*;
/**
 * The RequestLogin class authenticates the user information to log in. Other
 * tasks as part of the login process lies here as well.
 */

public class RequestRegistration extends GameRequest {

    // Data
    private String version;
    private String username;
    private String password;
    private String confirmedPassword;
    // Responses
    private ResponseRegistration responseRegistration;

    

    public RequestRegistration() {
        responses.add(responseRegistration = new ResponseRegistration());
    }

    @Override
    public void parse() throws IOException {
        version = DataReader.readString(dataInput).trim();
        username = DataReader.readString(dataInput).trim();
        password = DataReader.readString(dataInput).trim();
        confirmedPassword = DataReader.readString(dataInput).trim();
    }

    @Override
    public void doBusiness() throws Exception {
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
        Log.printf("User '%s' is registering...", username);
        if (version.compareTo(Constants.CLIENT_VERSION) >= 0) {
            String query = "SELECT * FROM Players WHERE username = '" + username + "';";
            Statement st = connection.createStatement();
            ResultSet rs = st.executeQuery(query);
            if(!rs.next()){
                if (!username.isEmpty() && password.equals(confirmedPassword)) {
                    Log.printf("User '%s' entered passwd '%s' and confirmed = '%s'", username, password, confirmedPassword);
                    Log.printf("Password matches");
                    query = "INSERT INTO Players (username,password) values('" + username + "','" + password + "');";
                    st.executeUpdate(query);
                    connection.close();
                    responseRegistration.setStatus((short) 0); // Registration Success
                    responseRegistration.setUsername(username);
                    Log.printf("User Successfully Registered");
                } else {
                    Log.printf("Either empty username or password do not match;");
                    responseRegistration.setStatus((short) 1); // Either empty username or password do not match;
                    responseRegistration.setUsername(username);
                }
            } else {
                Log.printf("Username already registered.");
                responseRegistration.setStatus((short) 2); // Username Already Taken
                responseRegistration.setUsername(username);
            }
        } 
    }
}


