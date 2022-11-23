CREATE DATABASE DapperJoins;
GO

USE DapperJoins;
GO

CREATE TABLE Pessoas
(
    IdPessoa INT IDENTITY,
    Nome VARCHAR(100) NOT NULL
);
GO

CREATE INDEX IDX_IdPessoa
ON Pessoas(IdPessoa);
GO

CREATE TABLE Telefones
(
    IdTelefone INT IDENTITY,
    TelefoneTexto VARCHAR(11) NOT NULL,
    IdPessoa INT NOT NULL,
    Ativo BIT DEFAULT 1 NOT NULL
);
GO

CREATE INDEX IDX_Telefones
ON Telefones(IdTelefone);
GO

CREATE TABLE Detalhes
(
    IdDetalhe INT IDENTITY,
    DetalheTexto VARCHAR(300),
    IdPessoa INT NOT NULL
);
GO

CREATE INDEX IDX_Detalhes
ON Detalhes(IdDetalhe);
GO

-- Popula Pessoas
INSERT INTO Pessoas (Nome)
VALUES ('Brittni Moncreiff'),
       ('Doug Klaiser'),
       ('Dorey Jiggens'),
       ('Kort Zanioletti'),
       ('Aldrich Marsay'),
       ('Raddie Gaynesford'),
       ('Magdalene Bastistini'),
       ('Barton Leinweber'),
       ('Jenilee Leborgne'),
       ('Korie Spurritt');
GO

-- Popula Telefones
INSERT INTO Telefones (TelefoneTexto, IdPessoa, Ativo)
VALUES ('5625112535', 1, 0),
       ('5902933598', 2, 1),
       ('2394320319', 3, 1),
       ('6129235384', 4, 0),
       ('4186432175', 5, 0),
       ('3862263691', 6, 1),
       ('4267144846', 7, 1),
       ('6025668630', 8, 0),
       ('1461362473', 9, 1),
       ('9764155948', 10, 0);
GO

-- Popula Detalhes
INSERT INTO Detalhes (DetalheTexto, IdPessoa)
VALUES ('transition dot-com applications', 1),
       ('evolve dynamic e-services', 2),
       ('optimize dynamic paradigms', 3),
       ('benchmark user-centric e-business', 4),
       ('iterate global experiences', 5),
       ('whiteboard efficient e-commerce', 6),
       ('incentivize plug-and-play relationships', 7),
       ('exploit revolutionary applications', 8),
       ('redefine sexy technologies', 9),
       ('deliver end-to-end infomediaries', 10);
GO

-- Adiciona as constraints após inserção
ALTER TABLE Pessoas
ADD CONSTRAINT PK_Pessoas PRIMARY KEY(IdPessoa)
GO

ALTER TABLE Telefones
ADD CONSTRAINT PK_Telefones PRIMARY KEY (IdTelefone),
    CONSTRAINT FK_PessoasTelefones FOREIGN KEY (IdPessoa)
    REFERENCES Pessoas(IdPessoa);
GO

ALTER TABLE DETALHES
ADD CONSTRAINT PK_Detalhes PRIMARY KEY (IdDetalhe),
    CONSTRAINT FK_PessoasDetalhes FOREIGN KEY (IdPessoa)
    REFERENCES Pessoas (IdPessoa);
GO