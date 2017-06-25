<?php
/**
 * Created by PhpStorm.
 * User: Marion
 * Date: 22/04/2017
 * Time: 17:29
 */

require_once(BASE_PATH . 'library/strings.php');

class displayController {
    public function indexAction() {

        if(isset($_GET['code']) && $_GET['code'] != '') {

            require_once(BASE_PATH . 'Application/Model/Service/levelService.php');
            $levelService = new LevelService();

            $code = Strings::sanitizeString($_GET['code']);

            $level = $levelService->getLevel($code);

            $name     = $level['title'];
            $code     = $level['code'];
            $score    = $level['score'];
            $hardness = $level['hardness'];

            unset($level['id']);
            unset($level['title']);
            unset($level['code']);
            unset($level['score']);
            unset($level['hardness']);

            $htmlLevel = $levelService->htmlEncodeLevel($level);

            require_once(BASE_PATH . 'Application/View/header.php');
            require_once(BASE_PATH . 'Application/View/nav.php');
            require_once(BASE_PATH . 'Application/View/display.php');
            require_once(BASE_PATH . 'Application/View/footer.php');

        } else {

            require_once(BASE_PATH . 'Application/Controller/erreurController.php');
            $errorManager = new erreurController();

            $errorManager->indexAction();
        }
    }
}