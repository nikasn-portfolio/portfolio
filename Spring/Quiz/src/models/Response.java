package models;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.UUID;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class Response {

    private UUID id;
    private UUID questionId;
    private String text;
    private Boolean isCorrect;
}
