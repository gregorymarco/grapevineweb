/*POSTGRES 3.5*/
CREATE TABLE Messages (
    ID SERIAL NOT NULL,
    MessageContent varchar(1000),
    Lat decimal(11,8),
    Long decimal(11,8),
    heure TIMESTAMP,
    PRIMARY KEY(ID)
)