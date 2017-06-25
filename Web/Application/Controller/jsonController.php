<?php
/**
 * Created by PhpStorm.
 * User: Marion
 * Date: 03/04/2017
 * Time: 13:41
 */

class jsonController {
    /**
     * Shows error message
     * @param String $msg
     */
    public function handleError($msg) {
        $error['error'] = $msg;
        die(json_encode($error));
    }

    /**
     * Converts Std Object to array
     * @param $stdObject
     * @return mixed
     */
    public function stdObjectToArray($stdObject) {
        return json_decode(json_encode($stdObject), true);
    }
}