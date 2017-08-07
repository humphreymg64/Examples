# The following script generates a table 'movie' containing a list of movies with some
# general information such as the title, the MPAA film rating, its year of publication,
# and its run time

DROP TABLE IF EXISTS movie;
CREATE TABLE movie (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT, 
	title VARCHAR(80),
	studio VARCHAR(50),
	rating ENUM('G', 'PG', 'PG-13', 'R', 'NC-17', 'Not Rated'),
	pub_year YEAR(4),
	imdb_rating FLOAT(4.1),
	run_time INTEGER);

INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Lady and the Tramp II', 'Walt Disney', 'G', 2001, 5.9, 70);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Arthur and the Invisibles', 'The Weinstein Company', 'PG', 2007, 6, 94);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Don\'t Say a Word', 'Twentieth Century Fox', 'R', 2001, 6.3, 113);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Good Will Hunting', 'Miramax', 'R', 1998, 8.3, 126);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Noises Off', 'Touchstone', 'PG-13', 1992, 7.5, 89);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Amadeus', 'Warner Brothers', 'R', 1985, 8.3, 160);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Grand Canyon', 'Twentieth Century Fox', 'R', 1992, 6.9, 137);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Patch Adams', 'Universal', 'PG-13', 1998, 6.6, 116);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Men in Black II', 'Columbia Pictures', 'PG-13', 2002, 7.2, 88);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('300', 'Warner Brothers', 'R', 2007, 7.8, 116);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Braveheart', 'Paramount', 'R', 2000, 8.4, 177);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Meet the Robinsons', 'Walt Disney', 'G', 2007, 6.9, 95);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Surfs Up', 'Columbia Pictures', 'PG', 2007, 6.8, 85);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Its a Wonderful Life', 'Paramount', 'Not Rated', 1946, 8.7, 130);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('The Polar Express', 'Warner Brothers', 'G', 2004, 6.6, 100);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('The Incredibles', 'Disney-Pixar', 'PG', 2003, 8.0, 115);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Peter Pan', 'Walt Disney', 'G', 1953, 7.4, 77);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Monster House', 'Columbia Pictures', 'PG', 2006, 6.7, 91);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('The Shawshank Redemption', 'Castle Rock Entertainment', 'R', 1994, 9.2, 142);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('The Godfather', 'Paramount', 'R', 1972, 9.2, 175);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('The Godfather: Part II', 'Paramount', 'R', 1974, 9.0, 200);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('The Dark Knight', 'Warner Brothers', 'PG-13', 2008, 9.0, 152);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Pulp Fiction', 'Miramax', 'R', 1994, 8.9, 154);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Schindler\'s List', 'Universal Pictures', 'R', 1993, 8.9, 195);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('12 Angry Men', 'Orion-Nova Productions', 'Not Rated', 1957, 8.9, 96);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('The Good, the Bad and the Ugly', 'Produzioni Europee Associati', 'Not Rated', 1967, 8.9, 161);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('The Lord of the Rings: The Return of the King', 'New Line Cinema', 'PG-13', 2003, 8.9, 201);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Fight Club', 'Fox 2000 Pictures', 'R', 1999, 8.8, 139);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('The Lord of the Rings: The Fellowship of the Ring', 'New Line Cinema', 'PG-13', 2001, 8.8, 178);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Star Wars: Episode V - The Empire Strikes Back', 'Lucasfilm', 'PG', 1980, 8.7, 124);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Forrest Gump', 'Paramount', 'PG-13', 1994, 8.7, 142);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Inception', 'Warner Bros.', 'PG-13', 2010, 8.7, 148);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('One Flew Over the Cuckoo\'s Nest', 'Fantasy Films', 'R', 1975, 8.7, 133);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('The Lord of the Rings: The Two Towers', 'New Line Cinema', 'PG-13', 2002, 8.7, 179);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Goodfellas', 'Warner Bros.', 'R', 1990, 8.7, 146);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('The Matrix', 'Warner Bros.', 'R', 1999, 8.7, 136);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Star Wars: Episode IV - A New Hope', 'Lucasfilm', 'PG', 1977, 8.7, 121);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Seven Samurai', 'Toho Company', 'Not Rated', 1954, 8.7, 207);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('City of God', 'O2 Filmes', 'R', 2002, 8.6, 130);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Se7en', 'Cecchi Gori Pictures', 'R', 1995, 8.6, 127);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Interstellar', 'Paramount', 'PG-13', 2014, 8.6, 169);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('The Usual Suspects', 'PolyGram Filmed Entertainment', 'R', 1995, 8.6, 106);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('The Silence of the Lambs', 'Strong Heart//Demme Production', 'R', 1991, 8.6, 118);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Léon: The Professional', 'Gaumont', 'R', 1994, 8.6, 110);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Once Upon a Time in the West', 'Rafran Cinematografica', 'PG-13', 1968, 8.6, 165);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Life Is Beautiful', 'Melampo Cinematografica', 'PG-13', 1997, 8.6, 116);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Casablanca', 'Warner Bros.', 'PG', 1942, 8.5, 102);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('American History X', 'New Line Cinema', 'R', 1998, 8.5, 119);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Saving Private Ryan', 'DreamWorks SKG', 'R', 1998, 8.5, 169);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Raiders of the Lost Ark', 'Paramount Pictures', 'PG', 1981, 8.5, 115);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Spirited Away', 'Tokuma Shoten', 'PG', 2001, 8.5, 125);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Psycho', 'Shamley Productions', 'R', 1960, 8.5, 109);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Rear Window', 'Paramount Pictures', 'Not Rated', 1954, 8.5, 112);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Whiplash', 'Bold Films', 'R', 2014, 8.5, 107);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('The Intouchables', 'Quad Productions', 'R', 2011, 8.5, 112);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Modern Times', 'Charles Chaplin Productions', 'G', 1936, 8.5, 87);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Terminator 2: Judgment Day', 'Carolco Pictures', 'R', 1991, 8.5, 137);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('The Green Mile', 'Castle Rock Entertainment', 'R', 1999, 8.5, 189);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Memento', 'Newmarket Capital Group', 'R', 2000, 8.5, 113);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('The Pianist', 'R.P. Productions', 'R', 2002, 8.5, 150);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('The Departed', 'Warner Bros.', 'R', 2006, 8.5, 151);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Gladiator', 'DreamWorks SKG', 'R', 2000, 8.5, 155);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Apocalypse Now', 'Zoetrope Studios', 'R', 1979, 8.5, 153);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Sunset Blvd.', 'Paramount Pictures', 'Not Rated', 1950, 8.5, 110);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Dr. Strangelove', 'Columbia Pictures Corporation', 'PG', 1964, 8.5, 95);
INSERT INTO movie (title, studio, rating, pub_year, imdb_rating, run_time) VALUES ('Back to the Future', 'Universal Pictures', 'PG', 1985, 8.5, 116);