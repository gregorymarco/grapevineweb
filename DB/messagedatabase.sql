/*POSTGRES 3.5*/

/*Messages table: contains data about messages that are posted*/
CREATE TABLE Messages (
    ID SERIAL NOT NULL,
    MessageContent varchar(1000),
    Lat decimal(11,8),
    Long decimal(11,8),
    heure TIMESTAMP,
    PRIMARY KEY(ID)
)
/*Votes table: contains vote data for messages*/
/*
CREATE TABLE Votes (
    ID FOREIGN KEY of Messages,
    ups decimal(10),
    downs decimal(10)
)
*/