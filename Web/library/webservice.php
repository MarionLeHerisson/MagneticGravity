<?php
/**
 * Created by PhpStorm.
 * User: Marion
 * Date: 16/04/2017
 * Time: 18:44
 */

class webservice {
    /**
     * Generate a unique key (5 letters and 5 numbers)
     * @return string
     */
    public static function generateCode() {
        require_once(BASE_PATH . 'Application/Model/levelModel.php');
        $levelManager = new LevelModel();

        do {
            $letters = str_shuffle('ABCDEFGHIJKLMNOPQRSTUVWXYZ');
            $numbers= str_shuffle('0123456789');

            $code = str_shuffle(substr($letters, 0, 5) . substr($numbers, 0, 5));
        } while ($levelManager->getLevelFromCode($code) == null);

        return $code;
    }
}