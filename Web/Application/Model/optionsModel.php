<?php
/**
 * Created by PhpStorm.
 * User: Marion
 * Date: 18/05/2017
 * Time: 17:18
 */


require_once(BASE_PATH . 'Application/Model/defaultModel.php');

class OptionsModel extends DefaultModel
{
    protected $_name = 'options';

    public function getAllOptions() {
        $bdd = $this->connectDb();

        $query = $bdd->prepare("SELECT opt.id AS opt_id, opt.opt_name, opt.json_label, opt_type, opt_step, opt_desc, opt.min_val, opt.max_val, opt.block_type_id 
, bl.id, bl.ref
FROM " . $this->_name . " AS opt
JOIN blocks AS bl ON bl.id = opt.block_type_id;");
        $query->execute();

        return $query;
    }
}