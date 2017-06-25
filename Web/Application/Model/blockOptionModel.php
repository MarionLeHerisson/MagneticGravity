<?php
/**
 * Created by PhpStorm.
 * User: Marion
 * Date: 10/06/2017
 * Time: 19:41
 */

class BlockOptionModel extends DefaultModel {
    protected $_name = 'block_option';

    /**
     * @param int $optId
     * @param int $blockLvlId
     * @param int|float|string $val
     * @return PDOStatement
     */
    public function insertOption($optId, $blockLvlId, $val) {
        $db = $this->connectDb();
        $query = $db->prepare('INSERT INTO ' . $this->_name . '(opt_id, block_level_id, opt_value) VALUES(?, ?, ?);');
        $query->execute([$optId, $blockLvlId, $val]);
        return $query;
    }
}