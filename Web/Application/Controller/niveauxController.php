<?php
/**
 * Created by PhpStorm.
 * User: Marion
 * Date: 13/06/2017
 * Time: 18:36
 */

class niveauxController {

    public function indexAction() {

        // managers
        require_once(BASE_PATH . 'Application/Model/levelModel.php');
        /** @var levelModel $levelManager */
        $levelManager = new levelModel();

        $allLevels = $levelManager->getAllLevels();

        $htmlList = '';
        while (is_array($level = $allLevels->fetch(PDO::FETCH_ASSOC))) {
            $htmlList .= '<a class="list-group-item" href="display?code=' . $level['code'] . '">';
            $htmlList .= '<h3>' . $level['title'] . ' <small>' . $level['code'] . '</small></h3>';

            if($level['img'] != null) {
                $htmlList .= '<img class="flright" src="data:image/png;base64,';
                $htmlList .= $level['img'];
                $htmlList .= '">';
            }

            $htmlList .= '</a>';
        }

        require_once(BASE_PATH . 'Application/View/header.php');
        require_once(BASE_PATH . 'Application/View/nav.php');
        require_once(BASE_PATH . 'Application/View/allLevels.php');
        require_once(BASE_PATH . 'Application/View/footer.php');
    }
}