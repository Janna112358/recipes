\c postgres
drop database recipes;
create database recipes;
\c recipes

-- TYPES --
CREATE TYPE "measure_type" AS ENUM (
  'unspecified',
  'small_amount',
  'quantity',
  'range'
);

CREATE TYPE "effort_score" AS ENUM (
  'minimal',
  'low',
  'medium',
  'high',
  'extreme'
);

-- SCHEMAS -- 
CREATE SCHEMA "general";
CREATE SCHEMA "recipes";

-- TABLES -- 
CREATE TABLE "units" (
  "tag" varchar(255) PRIMARY KEY,
  "name" varchar(255) NOT NULL
);

CREATE TABLE "measures" (
  "id" int PRIMARY KEY,
  "type" measure_type NOT NULL,
  "text" varchar(255),
  "amount" float,
  "lower" float,
  "upper" float,
  "unit_tag" varchar(255),
  "is_approximate" boolean NOT NULL
);

CREATE TABLE "simple_ingredients" (
  "id" int PRIMARY KEY,
  "ingredient_option" int NOT NULL,
  "food" varchar(255) NOT NULL,
  "measure_id" int,
  "prep" varchar(255)
);

CREATE TABLE "ingredients" (
  "id" int PRIMARY KEY,
  "recipe_id" int,
  "primary_ingredient" int,
  "is_optional" bool NOT NULL
);

CREATE TABLE "ingredient_notes" (
  "id" varchar(255) PRIMARY KEY,
  "text" varchar(255) NOT NULL,
  "ingredient_id" int
);

CREATE TABLE "recipes" (
  "id" int PRIMARY KEY,
  "name" varchar(255) NOT NULL,
  "post_url" varchar(255),
  "post_title" varchar(255),
  "effort" effort_score
);

ALTER TABLE "measures" ADD FOREIGN KEY ("unit_tag") REFERENCES "units" ("tag");

ALTER TABLE "simple_ingredients" ADD FOREIGN KEY ("ingredient_option") REFERENCES "ingredients" ("id");

ALTER TABLE "simple_ingredients" ADD FOREIGN KEY ("measure_id") REFERENCES "measures" ("id");

ALTER TABLE "ingredients" ADD FOREIGN KEY ("recipe_id") REFERENCES "recipes" ("id");

ALTER TABLE "simple_ingredients" ADD FOREIGN KEY ("id") REFERENCES "ingredients" ("primary_ingredient");

ALTER TABLE "ingredient_notes" ADD FOREIGN KEY ("ingredient_id") REFERENCES "ingredients" ("id");
