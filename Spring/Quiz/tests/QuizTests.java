import models.Question;
import models.Response;
import org.junit.Assert;
import org.junit.Test;

import java.util.*;

public class QuizTests {
    @Test
    public void questionMethods() {
        // Initialize database H2
        H2DatabaseInit.initDatabase();

        // Initialize dao
        DaoQuestion dao = new DaoQuestion();

        // Create question with multiple responses
        List<Response> responses = new ArrayList<>();
        Response response1 = new Response(UUID.randomUUID(), null, "20", false);
        Response response2 = new Response(UUID.randomUUID(), null, "23", false);
        Response response3 = new Response(UUID.randomUUID(), null, "25", false);
        Response response4 = new Response(UUID.randomUUID(), null, "22", true);

        responses.add(response1);
        responses.add(response2);
        responses.add(response3);
        responses.add(response4);

        Question question = new Question(UUID.randomUUID(), "How old am I?", UUID.fromString("560ed22d-bcd9-41ad-9a49-3c2c92c9561f"), UUID.fromString("c0b0d0e0-75c6-4d41-bb1a-e793856d02dd"), UUID.fromString("921abccd-75c6-4d41-bb1a-e793856d02dd"), responses);

        // Save question with multiple responses
        Question savedQuestion = dao.addQuestionWithResponses(question);

        List<String> allQuestions = dao.getAllQuestions();
        System.out.println(allQuestions);
        Assert.assertTrue(allQuestions.contains("How old am I?"));

        // Changing question content and creating new responses
        savedQuestion.setContent("How old my girlfriend is?");

        List<Response> responses1 = new ArrayList<>();

        responses1.add(new Response(UUID.fromString("ce695276-a845-412d-88ab-14e0afe506d4"), null, "20", true));
        responses1.add(new Response(UUID.randomUUID(), null, "23", false));
        responses1.add(new Response(UUID.randomUUID(), null, "19", false));
        responses1.add(new Response(UUID.randomUUID(), null, "1", false));
        responses1.add(new Response(UUID.fromString("b9f88501-1495-4646-b87e-a1943852e54b"), null, "69", true));

        savedQuestion.setQuestionResponses(responses1);

        // Updating question with new content and new responses
        Question updatedQuestion = dao.updateQuestion(savedQuestion);

        allQuestions = dao.getAllQuestions();
        System.out.println(allQuestions);
        Assert.assertTrue(allQuestions.contains("How old my girlfriend is?"));

        Assert.assertEquals(savedQuestion.getId(), updatedQuestion.getId());
        Assert.assertTrue(updatedQuestion.getQuestionResponses().contains(new Response(UUID.fromString("b9f88501-1495-4646-b87e-a1943852e54b"), updatedQuestion.getId(), "69", true)));
        Assert.assertTrue(updatedQuestion.getQuestionResponses().contains(new Response(UUID.fromString("ce695276-a845-412d-88ab-14e0afe506d4"), updatedQuestion.getId(), "20", true)));

        // Search questions by topic ID
        List<Question> questions = dao.searchQuestionsByTopic(UUID.fromString("921abccd-75c6-4d41-bb1a-e793856d02dd"));

        System.out.println(questions);

        Assert.assertFalse(questions.isEmpty());

        // Delete question by ID
        dao.deleteQuestion(updatedQuestion);
        allQuestions = dao.getAllQuestions();

        Assert.assertTrue(allQuestions.isEmpty());
    }
}
