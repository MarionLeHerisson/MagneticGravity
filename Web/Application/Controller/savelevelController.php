<?php
/**
 * Created by PhpStorm.
 * User: Marion
 * Date: 19/03/2017
 * Time: 21:53
 */

require_once(BASE_PATH . 'Application/Controller/jsonController.php');
require_once(BASE_PATH . 'library/strings.php');
require_once(BASE_PATH . 'library/webservice.php');

class savelevelController extends jsonController {

    public function indexAction() {
        if(array_key_exists('json', $_POST) && isset($_POST['json']) && $_POST['json'] != '' && $_POST['json'] != null) {

            require_once(BASE_PATH . 'Application/Model/defaultModel.php');
            $defaultManager = new DefaultModel();

            require_once(BASE_PATH . 'Application/Model/levelModel.php');
            $levelManager = new LevelModel();

            require_once(BASE_PATH . 'Application/Model/blockLevelModel.php');
            $blockLevelManager = new BlockLevelModel();

            require_once(BASE_PATH . 'Application/Model/Service/blockService.php');
            $blockService = new BlockService();


            $data = json_decode($_POST['json'], true);

            $code = Strings::sanitizeString($data['code']);
            $title = Strings::sanitizeString($data['title']);

            // UPDATE LEVEL
            if($levelManager->existLevel($code, 'code')) {

                $level = $levelManager->getLevelFromCode($code)->fetch(PDO::FETCH_ASSOC);
                $lvlId = $level['id'];

                // update img / score / title
                $levelManager->updateLevel($lvlId, $title, $data['score'], $data['img']);

                $blockLevelManager->deleteAllLevelBlocks($lvlId);

                $uniqueCode = $code;

            }
            // CREATE LEVEL
            else {

                if($levelManager->existLevel($title, 'title')) {
                    $this->handleError('Name already exists.');
                }

                $uniqueCode = webservice::generateCode();

                $lvlId = $levelManager->insertLevel($data['title'], $uniqueCode, $data['mac'], $data['hardness'], $data['img']);

                // DEBUG START
                // $lvlId = 15;
                // $blockLevelManager->deleteAllLevelBlocks($lvlId);
                // DEBUG END
            }

            unset($data['id']);
            unset($data['mac']);
            unset($data['code']);
            unset($data['score']);
            unset($data['title']);
            unset($data['author']);
            unset($data['hardness']);
            unset($data['img']);

            $blockService->insertBlocksAndOptions($lvlId, $data);

            $msg['code'] = $uniqueCode;
            die(json_encode($msg));

        } else {
            $this->handleError('No json provided.');
        }
    }
}