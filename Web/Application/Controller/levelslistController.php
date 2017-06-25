<?php
/**
 * Created by PhpStorm.
 * User: Marion
 * Date: 19/03/2017
 * Time: 21:53
 */

class levelslistController {

    public function indexAction() {

        require_once(BASE_PATH . 'Application/Model/levelModel.php');
        $levelManager = new LevelModel();

        if (isset($_GET['mac']) && $_GET['mac'] != '') {

            require_once(BASE_PATH . 'library/strings.php');
            $mac = Strings::sanitizeString($_GET['mac']);

            $allLevels = $levelManager->getAllLevelsFromMac($mac);
        }
        else {

            $allLevels = $levelManager->getAllLevels();
        }

        $levels = [];
        while (is_array($level = $allLevels->fetch(PDO::FETCH_ASSOC))) {
            $levels[] = $level;
        }

        die(json_encode(['levels' => $levels]));
    }
}