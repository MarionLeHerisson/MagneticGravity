<?php
/**
 * Created by PhpStorm.
 * User: Marion
 * Date: 27/02/2017
 * Time: 14:35
 */

require_once(BASE_PATH . 'Application/Model/defaultModel.php');

class BlockLevelModel extends DefaultModel {
    protected $_name = 'block_level';

    /**
     * Insert all blocks linked to a level from previously made statement
     * @param String $stmt
     * @return PDOStatement
     */
    public function insertBlocks($stmt) {
        $bdd = $this->connectDb();

        $query = $bdd->prepare($stmt);
        $query->execute();

        return $query;
    }

    /**
     * Get all level blocks from a level id
     * @param int $levelId
     * @return PDOStatement
     */
    public function getAllBlocks($levelId) {
        $bdd = $this->connectDb();

        $query = $bdd->prepare('SELECT bl.id AS bl_id, bl.block_id, bl.posx, bl.posy'
                                . ', bo.block_level_id, bo.opt_id, bo.opt_value'
                                . ', o.json_label, o.min_val'
                                . ' FROM ' . $this->_name . ' AS bl'
                                . ' LEFT JOIN block_option AS bo ON bo.block_level_id = bl.id'
                                . ' LEFT JOIN options AS o ON o.id = bo.opt_id'
                                . ' WHERE bl.level_id = ? AND bl.is_deleted = 0;');

        $query->execute([$levelId]);

        return $query;
    }

    /**
     * Delete all blocks linked to a level
     * @param int $levelId
     * @return PDOStatement
     */
    public function deleteAllLevelBlocks($levelId) {
        $bdd = $this->connectDb();

        $query = $bdd->prepare('UPDATE ' . $this->_name . ' SET is_deleted = 1 WHERE level_id = ? AND is_deleted = 0');

        $query->execute([$levelId]);

        return $query;
    }

    /**
     * Insert one block and return its id
     * @param int $type
     * @param int $levelId
     * @param int $posX
     * @param int $posY
     * @return string
     */
    public function insertBlock($type, $levelId, $posX, $posY) {
        $bdd = $this->connectDb();

        $query = $bdd->prepare('INSERT INTO ' . $this->_name . '(block_id, level_id, posx, posy) VALUES (?, ?, ?, ?);');

        $query->execute([$type, $levelId, $posX, $posY]);

        $query = $bdd->prepare("SELECT LAST_INSERT_ID();");
        $query->execute();

        $res = $query->fetchColumn();

        return $res;
    }
}