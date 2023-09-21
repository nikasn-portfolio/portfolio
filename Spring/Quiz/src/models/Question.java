package models;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class Question {

    private UUID id;
    private String content;
    private UUID quizId;
    private UUID difficultyRankId;
    private UUID topicId;
    private List<Response> questionResponses = new ArrayList<>();

    public Question(UUID id, String content, UUID quizId, UUID difficultyRankId, UUID topicId) {
        this.id = id;
        this.content = content;
        this.quizId = quizId;
        this.difficultyRankId = difficultyRankId;
        this.topicId = topicId;
    }

}
