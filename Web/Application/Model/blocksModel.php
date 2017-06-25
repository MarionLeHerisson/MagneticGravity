<?php

require_once('defaultModel.php');

class BlocksModel extends DefaultModel {

    protected $_name = 'blocks';

    /**
     * Get all available blocks
     *
     * @return PDOStatement
     */
    public function getAllBlocks() {

        $bdd = $this->connectDb();

        $query = $bdd->prepare("SELECT id, ref, json_label, french_name, french_desc, is_premium FROM " . $this->_name
                                . " WHERE is_deleted = 0;");
        $query->execute();

        return $query;
    }

}