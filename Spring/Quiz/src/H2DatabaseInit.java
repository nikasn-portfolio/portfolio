import java.io.BufferedReader;
import java.io.FileReader;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.sql.Statement;

public class H2DatabaseInit {
    static final String JDBC_DRIVER = "org.h2.Driver";


    public static void initDatabase() {
        Connection conn = null;
        Statement stmt = null;

        try {
            Class.forName(JDBC_DRIVER);

            System.out.println("Connecting to the database...");
            conn = DriverManager.getConnection("jdbc:h2:~/test", "sa", "");

            BufferedReader reader = new BufferedReader(new FileReader("src/schema.sql"));
            String line;
            StringBuilder sqlStatements = new StringBuilder();

            while ((line = reader.readLine()) != null) {
                sqlStatements.append(line).append(" ");
            }

            stmt = conn.createStatement();
            stmt.executeUpdate(sqlStatements.toString());

            stmt.close();
            conn.close();
            reader.close();
        } catch (SQLException se) {
            se.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            try {
                stmt.close();
            } catch (SQLException e) {
                throw new RuntimeException(e);
            }
            try {
                conn.close();
            } catch (SQLException e) {
                throw new RuntimeException(e);
            }
        }
        System.out.println("Database has been started!");
    }
}
