create table Applications (
	Id INT NOT NULL AUTO_INCREMENT,
    Name VARCHAR(200) NOT NULL,
    Token VARCHAR(300) NOT NULL,
    PRIMARY KEY (Id)
);

create table Logs (
	Id VARCHAR(40) NOT NULL,
    ApplicationId INT NOT NULL REFERENCES Applications(Id),
    Title VARCHAR(200) NOT NULL,
    LogType VARCHAR(200) NOT NULL,
    LogDate datetime NOT NULL,
    Body text NOT NULL,
    PRIMARY KEY (Id)
)