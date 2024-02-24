CREATE TABLE Coffee_Beans (
  id INT IDENTITY PRIMARY KEY,
  name VARCHAR(50) NOT NULL,
  roast_level VARCHAR(20) NOT NULL,
  description VARCHAR(200),
  price DECIMAL(10,2) NOT NULL,
  country_of_origin VARCHAR(50)
);
CREATE TABLE Brewing_Methods (
  id INT IDENTITY PRIMARY KEY,
  name VARCHAR(50) NOT NULL,
  description VARCHAR(200)
);
CREATE TABLE Drinks (
  id INT IDENTITY PRIMARY KEY,
  name VARCHAR(50) NOT NULL,
  description VARCHAR(200),
  id_coffee_beans INT,
  id_brewing_method INT,
  price DECIMAL(10,2) NOT NULL,
  volume INT NOT NULL,
  FOREIGN KEY (id_coffee_beans) REFERENCES Coffee_Beans(id),
  FOREIGN KEY (id_brewing_method) REFERENCES Brewing_Methods(id)
);
CREATE TABLE Orders (
  id INT IDENTITY PRIMARY KEY,
  date_time DATETIME NOT NULL,
  id_drink INT,
  quantity INT NOT NULL,
  total DECIMAL(10,2) NOT NULL,
  FOREIGN KEY (id_drink) REFERENCES Drinks(id)
);
INSERT INTO Coffee_Beans (name, roast_level, description, price, country_of_origin)
VALUES
  ('Arabica Cattura', 'Medium', 'Balanced taste with fruity notes', 350, 'Brazil'),
  ('Robusta Vietnam', 'Dark', 'Strong and bitter coffee with chocolate aroma', 280, 'Vietnam'),
  ('Ethiopia Yirgacheffe', 'Light', 'Floral aroma with citrus notes', 420, 'Ethiopia'),
  ('Guatemala Antigua', 'Medium', 'Chocolate taste with nutty aroma', 380, 'Guatemala');
INSERT INTO Brewing_Methods (name, description)
VALUES
  ('Espresso', 'Classic way of making coffee'),
  ('Americano', 'Espresso diluted with water'),
  ('Cappuccino', 'Espresso with milk foam'),
  ('Latte', 'Espresso with hot milk');
INSERT INTO Drinks (name, description, id_coffee_beans, id_brewing_method, price, volume)
VALUES
  ('Espresso', 'Classic coffee', 1, 1, 150, 30),
  ('Americano', 'Espresso with water', 1, 2, 180, 120),
  ('Cappuccino', 'Coffee with milk foam', 2, 3, 220, 180),
  ('Latte', 'Coffee with milk', 3, 4, 200, 250);
INSERT INTO Orders (date_time, id_drink, quantity, total)
VALUES
  ('2024-02-14 12:00:00', 1, 2, 300),
  ('2024-02-14 14:30:00', 3, 1, 220),
  ('2024-02-14 17:00:00', 2, 1, 180);
