CREATE TABLE level(
  id INT NOT NULL AUTO_INCREMENT,
  title VARCHAR(50) NOT NULL,
  mac VARCHAR(40) DEFAULT NULL,
  code VARCHAR(20) NOT NULL,
  score FLOAT DEFAULT NULL,
  hardness FLOAT DEFAULT 0,
  img BLOB,
  is_deleted INT NOT NULL DEFAULT 0,

  PRIMARY KEY (id)
);

CREATE TABLE blocks(
  id INT NOT NULL AUTO_INCREMENT,
  ref VARCHAR(4) NOT NULL UNIQUE,
  unity_name VARCHAR(50) NOT NULL,
  json_label VARCHAR(50) NOT NULL,
  french_name VARCHAR(50) NOT NULL,
  french_desc VARCHAR(255) NOT NULL,
  is_premium INT NOT NULL DEFAULT 0,
  is_deleted INT NOT NULL DEFAULT 0,

  PRIMARY KEY (id)
);

INSERT INTO blocks(ref, unity_name, json_label, french_name, french_desc) VALUES
  ('SMPL', 'Block_Simple',          'simple',           'Bloc simple',                    'Bloc n\'ayant aucun effet particulier.'),
  ('STRT', 'BlockStart',            'start',            'Bloc de départ',                 'Début du niveau.'),
  ('BEND', 'BlockEnd',              'end',              'Bloc d\'arrivée',                'Fin du niveau.'),
  ('MAGN', 'BlockMagnetic',         'magnetic',         'Bloc magnétique',                'Attire ou repousse Bernard en fonction de sa polarité.'),
  ('TRAP', 'BlockMort',             'trap',             'Bloc d\'échec',                  'Renvoie Bernard au début du niveau ou au dernier checkpoint.'),
  ('PLRV', 'BlockPolarityReverse',  'polarityReverse',  'Bloc d\'inversion de polarité',  'Inverse la polarité de Bernard lorsqu\'il marche dessus.'),
  ('RNRV', 'BlockReverse',          'runReverse',       'Bloc d\'inversion de sens',      'Bernard change de direction lorsqu\'il marche dessus.'),
  ('STOP', 'BlockStop',             'stop',             'Bloc de pause',                  'Bernard profite d\'une pause réparatrice le temps de réfléchir à la suite du niveau.'),
  ('CHPT', 'Checkpoint',            'checkpoint',       'Checkpoint',                     'Si Bernard échoue, il pourra reprendre le niveau ici.'),
  ('BCOL', 'Collectibles',          'collectible',      'Collectible',                    'Un bonus que Bernard peut ramasser.'),
  ('BTUR', 'Turret',                'turret',           'Tourelle',                       'Avec ses missiles aimantés, elle risque de poser de sérieux problèmes à Bernard...'),
  ('SLOW', 'SlowPill',              'ralenti',          'Pillule de ralenti',             'Un petit coup de pouce pour ralentir la vitesse de Bernard');


CREATE TABLE block_level(
  id INT NOT NULL AUTO_INCREMENT,
  block_id INT NOT NULL,
  level_id INT NOT NULL,
  posx INT NOT NULL,
  posy INT NOT NULL,
  is_deleted INT NOT NULL DEFAULT 0,

  PRIMARY KEY (id),
  FOREIGN KEY (block_id) REFERENCES blocks(id),
  FOREIGN KEY (level_id) REFERENCES level(id)
);


-- inserted : 16/05/2017

-- DROP TABLE block_option;
-- DROP TABLE options;

CREATE TABLE options(
  id INT NOT NULL AUTO_INCREMENT,
  opt_name VARCHAR(50) NOT NULL,
  json_label VARCHAR(50) NOT NULL,
  opt_type varchar(10) NOT NULL,
  opt_desc VARCHAR(50) NOT NULL,
  opt_step VARCHAR(5) DEFAULT NULL,
  min_val VARCHAR(50) DEFAULT NULL,
  max_val VARCHAR(50) DEFAULT NULL,
  block_type_id INT NOT NULL,

  PRIMARY KEY (id),
  FOREIGN KEY (block_type_id) REFERENCES blocks(id)
);

CREATE TABLE block_option(
  id INT NOT NULL AUTO_INCREMENT,
  opt_id INT NOT NULL,
  block_level_id INT NOT NULL,
  opt_value VARCHAR(50),

  PRIMARY KEY (id),
  FOREIGN KEY (opt_id) REFERENCES options(id),
  FOREIGN KEY (block_level_id) REFERENCES block_level(id)
);

INSERT INTO options(opt_name, json_label, opt_type, opt_desc, opt_step, min_val, max_val, block_type_id)
VALUES ('strong_intensity', 'StrongIntensity', 'checkbox',   'Intensité forte',         'NULL', 'false', 'true',  4),
  ('negative_polarity', 'NegativePolarity',    'checkbox',   'Polarité négative',       'NULL', 'false', 'true',  4),
  ('balls_by_shoot'   , 'ballsByShot',         'range',      'Balles par tir',          '1',    '1',     '3',     11),
  ('wait_balls'       , 'waitBalls',           'range',      'Temps entre les balles',  '0.1',  '0.2',   '2',     11),
  ('wait_shoot'       , 'waitShoot',           'range',      'Temps entre les tirs',    '0.1',  '0.5',   '5',     11),
  ('direction_right'  , 'directionRight',      'checkbox',   'Départ vers la droite',   'NULL', 'true',  'false', 2),
  ('duree'            , 'duree',               'range',      'Durée du ralentissement', '1',    '1',     '10',    12);

-- inserted : 22/05/2017



# todo:
# Grille max horizontale : 128 blocs
# verticale : 128 blocs aussi.
# Blocs max :
# start : 1
# end : 1
# polarity reverse : 16
# run reverse : 16
# checkpoint : 1
# stop : 16
# blocs de mort : 64
# magnetiques : 64
# blocs simples : 64
# tourelle : à définir