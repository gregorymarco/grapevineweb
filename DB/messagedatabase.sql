/*POSTGRES 3.5*/
CREATE TABLE Messages (
    ID SERIAL NOT NULL,
    MessageContent varchar(1000),
    Lat decimal(10),
    Long decimal(10),
    PRIMARY KEY(ID)
)