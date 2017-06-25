<?php
/**
 * Created by PhpStorm.
 * User: Marion
 * Date: 27/02/2017
 * Time: 14:35
 */

require_once(BASE_PATH . 'Application/Model/defaultModel.php');

class LevelModel extends DefaultModel {
    protected $_name = 'level';

    /**
     * Get level from code
     * @param String $code
     * @return PDOStatement
     */
    public function getLevelFromCode($code) {
        $bdd = $this->connectDb();

        $query = $bdd->prepare("SELECT id, title, code, score, hardness, img FROM " . $this->_name .
                               " WHERE code = ? AND is_deleted = 0;");
        $query->execute([$code]);

        return $query;
    }

    /**
     * Get level from its name
     * @param $levelName
     * @return PDOStatement
     */
    public function getLevelFromName($levelName) {
        $bdd = $this->connectDb();

        $query = $bdd->prepare("SELECT id, title, code, score, hardness, img FROM " . $this->_name .
            " WHERE title = ? AND is_deleted = 0;");
        $query->execute([$levelName]);

        return $query;
    }

    /**
     * Create level with unique code and name
     * Return its id
     * @param String $title
     * @param String $code
     * @param String $mac
     * @param float $hardness
     * @param String $img
     * @return integer
     */
    public function insertLevel($title, $code, $mac = null, $hardness = 0.0, $img = null) {
        $bdd = $this->connectDb();

        $query = $bdd->prepare("INSERT INTO " . $this->_name . " (title, code, mac, hardness, img)" .
                               " VALUES (?, ?, ?, ?, ?);");

        $query->execute([$title, $code, $mac, $hardness, $img]);

        $query = $bdd->prepare("SELECT LAST_INSERT_ID();");
        $query->execute();

        $res = $query->fetchColumn();

        return $res;
    }

    /**
     * Returns true if level already exists
     * @param String $data
     * @param String $type
     * @return boolean
     */
    public function existLevel($data, $type) {
        if($type == 'title') {
            $lvl = $this->getLevelFromName($data)->fetch();
        }
        else if($type == 'code') {
            $lvl = $this->getLevelFromCode($data)->fetch();
        }
        return(!empty($lvl));
    }

    /**
     * Get all finished levels
     * @return PDOStatement
     */
    public function getAllLevels() {
        $bdd = $this->connectDb();

        $query = $bdd->prepare('SELECT code, title, score, hardness, img FROM level'
                                . ' WHERE is_deleted = 0 ORDER BY id DESC;');
        $query->execute();

        return $query;
    }

    /**
     * Get all levels for a specific mac address
     * @return PDOStatement
     */
    public function getAllLevelsFromMac($mac) {
        $bdd = $this->connectDb();

        $query = $bdd->prepare('SELECT code, title, score, hardness, img FROM level'
                                . ' WHERE is_deleted = 0 AND mac = \'' . $mac  . '\' ORDER BY id DESC;');
        $query->execute();

        return $query;
    }

    /**
     * Update score and thumbnail of a level
     * @param int $lvlId
     * @param string $title
     * @param int|string $score
     * @param String $img
     * @return PDOStatement
     */
    public function updateLevel($lvlId, $title, $score = null, $img) {
        $bdd = $this->connectDb();

        $query = $bdd->prepare('UPDATE level SET title = ?, score = ?, img = ? WHERE id = ?;');
        $query->execute([$title, $score, $img, $lvlId]);

        return $query;
    }
}