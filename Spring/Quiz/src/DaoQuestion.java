import models.Question;
import models.Response;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

public class DaoQuestion {
    static final String DB_URL = "jdbc:h2:~/test";
    static final String USER = "sa";
    static final String PASS = "";

    public Question addQuestionWithResponses(Question question) {
        try (
                Connection conn = DriverManager.getConnection(DB_URL, USER, PASS);
                PreparedStatement pstmt = conn.prepareStatement("INSERT INTO QUESTION VALUES (?, ?, ?, ?, ?)")
        ) {
            pstmt.setString(1, question.getId().toString());
            pstmt.setString(2, question.getQuizId().toString());
            pstmt.setString(3, question.getDifficultyRankId().toString());
            pstmt.setString(4, question.getTopicId().toString());
            pstmt.setString(5, question.getContent());

            pstmt.executeUpdate();

            System.out.println("Inserted records into the table...");
        } catch (SQLException se) {
            se.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        System.out.println("Successful!");

        try (
                Connection conn = DriverManager.getConnection(DB_URL, USER, PASS);
                PreparedStatement pstmt = conn.prepareStatement("INSERT INTO RESPONSE VALUES (?, ?, ?, ?)")
        ) {
            for (Response response : question.getQuestionResponses()) {
                response.setQuestionId(question.getId());

                pstmt.setString(1, response.getId().toString());
                pstmt.setString(2, response.getQuestionId().toString());
                pstmt.setString(3, response.getText());
                pstmt.setBoolean(4, response.getIsCorrect());

                pstmt.addBatch();
            }

            int[] batchResult = pstmt.executeBatch();

            for (int result : batchResult) {
                if (result == PreparedStatement.EXECUTE_FAILED) {
                    throw new SQLException("Batch execution failed.");
                }
            }

        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
        return question;
    }

    public Question updateQuestion(Question question) {
        // Update question content
        try (
                Connection conn = DriverManager.getConnection(DB_URL, USER, PASS);
                PreparedStatement pstmt = conn.prepareStatement("UPDATE QUESTION SET content = ? WHERE id = ?")
        ) {

            pstmt.setString(1, question.getContent());
            pstmt.setString(2, question.getId().toString());

            pstmt.executeUpdate();

        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
        // Delete old Responses
        try (
                Connection conn = DriverManager.getConnection(DB_URL, USER, PASS);
                PreparedStatement pstmt = conn.prepareStatement("DELETE FROM RESPONSE WHERE question_id = ?")
        ) {

            pstmt.setString(1, question.getId().toString());

            pstmt.executeUpdate();

        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
        // Insert new Responses
        try (
                Connection conn = DriverManager.getConnection(DB_URL, USER, PASS);
                PreparedStatement pstmt = conn.prepareStatement("INSERT INTO RESPONSE VALUES (?, ?, ?, ?)")
        ) {
            for (Response response : question.getQuestionResponses()) {
                response.setQuestionId(question.getId());

                pstmt.setString(1, response.getId().toString());
                pstmt.setString(2, response.getQuestionId().toString());
                pstmt.setString(3, response.getText());
                pstmt.setBoolean(4, response.getIsCorrect());

                pstmt.addBatch();
            }

            int[] batchResult = pstmt.executeBatch();

            for (int result : batchResult) {
                if (result == PreparedStatement.EXECUTE_FAILED) {
                    throw new SQLException("Batch execution failed.");
                }
            }

        } catch (SQLException e) {
            throw new RuntimeException(e);
        }

        return question;
    }


    public List<String> getAllQuestions() {
        List<String> allQuestionsContent = new ArrayList<>();
        try (
                Connection conn = DriverManager.getConnection(DB_URL, USER, PASS);
                Statement stmt = conn.createStatement()
        ) {
            String sql = "SELECT content FROM QUESTION";
            ResultSet rs = stmt.executeQuery(sql);


            while (rs.next()) {
                String content = rs.getString("content");
                allQuestionsContent.add(content);
            }

            System.out.println("All records have been retrieved from the table...");
        } catch (SQLException se) {
            se.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        System.out.println("Successful!");
        return allQuestionsContent;
    }

    public void deleteQuestion(Question question) {
        try (
                Connection conn = DriverManager.getConnection(DB_URL, USER, PASS);
                PreparedStatement pstmt = conn.prepareStatement("DELETE FROM RESPONSE WHERE question_id = ?")
        ) {

            pstmt.setString(1, question.getId().toString());

            pstmt.executeUpdate();

        } catch (SQLException e) {
            throw new RuntimeException(e);
        }

        try (
                Connection conn = DriverManager.getConnection(DB_URL, USER, PASS);
                PreparedStatement pstmt = conn.prepareStatement("DELETE FROM QUESTION WHERE id = ?")
        ) {

            pstmt.setString(1, question.getId().toString());

            pstmt.executeUpdate();

        } catch (SQLException e) {
            throw new RuntimeException(e);
        }


        System.out.println("Question has been deleted!");
    }

    public List<Question> searchQuestionsByTopic(UUID topicId) {
        List<Question> questionList = new ArrayList<>();
        try (
                Connection conn = DriverManager.getConnection(DB_URL, USER, PASS);
                PreparedStatement pstmt = conn.prepareStatement("SELECT * FROM QUESTION WHERE topic_id = ?")
        ) {

            pstmt.setString(1, topicId.toString());

            ResultSet rs = pstmt.executeQuery();

            while (rs.next()) {
                Question question = new Question(UUID.fromString(rs.getString("id")), rs.getString("content"),
                        UUID.fromString(rs.getString("quiz_id")),
                        UUID.fromString(rs.getString("difficulty_rank_id")),
                        UUID.fromString(rs.getString("topic_id"))
                );


                try (
                        Connection conn1 = DriverManager.getConnection(DB_URL, USER, PASS);
                        PreparedStatement pstmt1 = conn1.prepareStatement("SELECT * FROM RESPONSE WHERE question_id = ?")) {
                    pstmt1.setString(1, question.getId().toString());

                    ResultSet rs1 = pstmt1.executeQuery();
                    List<Response> responses = new ArrayList<>();
                    while (rs1.next()) {
                        Response response = new Response(UUID.fromString(rs1.getString("id")),
                                UUID.fromString(rs1.getString("question_id")),
                                rs1.getString("text"),
                                rs1.getBoolean("is_correct"));
                        responses.add(response);
                    }
                    question.setQuestionResponses(responses);
                    questionList.add(question);

                } catch (SQLException e) {
                    throw new RuntimeException(e);
                }
            }

        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
        return questionList;
    }
}
