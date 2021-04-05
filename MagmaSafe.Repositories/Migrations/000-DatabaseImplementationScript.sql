/*
+---------------------------+
| CRIAÇÃO DO BANCO DE DADOS |
+---------------------------+
*/
CREATE DATABASE magmaSafeDatabase;
USE magmaSafeDatabase;

/*
+---------------------------+
| CRIAÇÃO DA TABELA USER    |
+---------------------------+
*/
CREATE TABLE IF NOT EXISTS user (
    id						VARCHAR(40)   		NOT NULL PRIMARY KEY,
    name					VARCHAR(50)   		NOT NULL,
    email					VARCHAR(100) 	  	NOT NULL UNIQUE,
    password				VARCHAR(100)		NOT NULL,
    isAdmin					BOOLEAN				NOT NULL,
    isActive				BOOLEAN				NOT NULL
);


/*
+---------------------------+
| CRIAÇÃO DA TABELA SERVER  |
+---------------------------+
*/
CREATE TABLE IF NOT EXISTS server (
    id						VARCHAR(40)   		NOT NULL PRIMARY KEY,
    name					VARCHAR(50)   		NOT NULL,
	adminId					VARCHAR(40)			NOT NULL,
  	createdAt				DATETIME			NOT NULL,
  	updatedAt				DATETIME			NOT NULL,
  	CONSTRAINT			FK_adminId FOREIGN KEY (adminId) REFERENCES user(id)
);


/*
+-----------------------------+
| CRIAÇÃO DA TABELA SECRET    |
+-----------------------------+*/
CREATE TABLE IF NOT EXISTS secret (
    id					VARCHAR(40)   			NOT NULL PRIMARY KEY,
    name				VARCHAR(50)   			NOT NULL,
    magmaSecret			VARCHAR(100) 	  		NOT NULL UNIQUE,
  	userId 				VARCHAR(40) 			NOT NULL,
  	serverId 			VARCHAR(40) 			NOT NULL,
  	createdAt			DATETIME				NOT NULL,
  	updatedAt			DATETIME				NOT NULL,
    lastAccessedByUser	VARCHAR(40)				NOT NULL,
    lastAccessed		DATETIME				NOT NULL,
    
    
  	CONSTRAINT FK_userId FOREIGN KEY (userId) REFERENCES user(id),
  	CONSTRAINT FK_serverId FOREIGN KEY (serverId) REFERENCES server(id)
);


/*
+-------------------------------------+
| CRIAÇÃO DA TABELA SERVERSOFUSERS    |
+-------------------------------------+
*/
CREATE TABLE IF NOT EXISTS serversOfUsers (
  userId			VARCHAR(40)					NOT NULL,
  serverId			VARCHAR(40)					NOT NULL,
  CONSTRAINT FK_userIdServers FOREIGN KEY (userId) REFERENCES user(id),
  CONSTRAINT FK_serverIdServers FOREIGN KEY (serverId) REFERENCES server(id)
);
  