DROP TABLE IF EXISTS savedgames_cells;
DROP TABLE IF EXISTS savedgames;
DROP TABLE IF EXISTS plants_users;
DROP TABLE IF EXISTS users;
DROP TABLE IF EXISTS plants;


CREATE TABLE plants (
	id_plant INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
	plant VARCHAR(24),
	time FLOAT,
	quantity INT,
	sell DECIMAL(9,2),
	buy DECIMAL(9,2),
	season INT
);

CREATE TABLE users (
	id_user INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
	user VARCHAR(12),
	password CHAR(32)
);

CREATE TABLE plants_users (
	id_plant_user INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
	id_plant INT UNSIGNED NOT NULL,
	id_user INT UNSIGNED NOT NULL,
	FOREIGN KEY (id_plant) REFERENCES plants (id_plant),
	FOREIGN KEY (id_user) REFERENCES users (id_user)
);

CREATE TABLE savedgames (
	id_savedgame INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
	time FLOAT,
	size INT,
	money DECIMAL(9,2),
	saved DATETIME,
	id_user INT UNSIGNED NOT NULL,
	FOREIGN KEY (id_user) REFERENCES users (id_user)
);

CREATE TABLE savedgames_cells (
	id_savedgame_cell INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
	x INT,
	y INT,
	time FLOAT,
	id_plant INT UNSIGNED NOT NULL,
	id_savedgame INT UNSIGNED NOT NULL,
	FOREIGN KEY (id_plant) REFERENCES plants (id_plant),
	FOREIGN KEY (id_savedgame) REFERENCES savedgames (id_savedgame)
);
