CREATE DATABASE contact_db;

\c contact_db;

CREATE TABLE contacts
(
    id SERIAL PRIMARY KEY,
    company TEXT,
    name TEXT,
    surname TEXT,
    contact_info JSONB
);

CREATE DATABASE report_db;

\c report_db;

CREATE TABLE reports
(
    id SERIAL PRIMARY KEY,
    report_status BOOLEAN,
    requested_at TIMESTAMP,
    report_contents JSONB
);