drop table if exists RESPONSE;
drop table if exists QUESTION;
drop table if exists QUIZ;
drop table if exists DIFFICULTY_RANK;
drop table if exists TOPIC;


create table QUIZ
(
    id UUID PRIMARY KEY,
    name varchar(255),
    primary key (id)
);

create table DIFFICULTY_RANK
(
    id UUID PRIMARY KEY,
    name varchar(255)

);

create table TOPIC
(
    id UUID PRIMARY KEY,
    name varchar(255)

);

create table QUESTION
(
    id UUID PRIMARY KEY,
    quiz_id  UUID,
    difficulty_rank_id UUID,
    topic_id UUID,
    content varchar(255),

    FOREIGN KEY (quiz_id) REFERENCES QUIZ(id),
    FOREIGN KEY (difficulty_rank_id) REFERENCES DIFFICULTY_RANK(id),
    FOREIGN KEY (topic_id) REFERENCES TOPIC(id)

);

create table RESPONSE
(
    id UUID PRIMARY KEY,
    question_id UUID,
    text varchar(255),
    is_correct boolean,
    FOREIGN KEY (question_id) REFERENCES QUESTION(id)
);

insert into QUIZ (id, name) values ('560ed22d-bcd9-41ad-9a49-3c2c92c9561f', 'How well do you know me?');

insert into TOPIC (id, name) values ('921abccd-75c6-4d41-bb1a-e793856d02dd', 'Biography');

insert into DIFFICULTY_RANK (id, name) values ('c0b0d0e0-75c6-4d41-bb1a-e793856d02dd', 'Easy');
insert into DIFFICULTY_RANK (id, name) values ('c0b0d0e0-75c6-4d41-bb1a-e793856d02de', 'Medium');
insert into DIFFICULTY_RANK (id, name) values ('c0b0d0e0-75c6-4d41-bb1a-e793856d02df', 'Hard');

