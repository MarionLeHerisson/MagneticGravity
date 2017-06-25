UPDATE block_level SET is_deleted = 1
WHERE level_id=39 AND
NOT EXISTS
(SELECT 1
 FROM
   (SELECT 1
    FROM block_level
    WHERE block_id = 3 AND posx = 5 AND posy = 0 AND level_id = 39)
);

INSERT INTO block_level(block_id, posx, posy, level_id)
VALUES (3, 5, 0, 39)
WHERE NOT EXISTS
(SELECT 1 FROM block_level WHERE block_id=3 AND posx=5 AND posy=0 AND level_id=39);