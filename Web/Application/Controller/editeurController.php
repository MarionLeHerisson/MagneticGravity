<?php

class editeurController {

    public function indexAction() {

        // ajax
        if(isset($_POST['action']) && !empty($_POST['action'])) {
            $this->Ajax($_POST);
        }

        require_once(BASE_PATH . 'Application/View/header.php');
        require_once(BASE_PATH . 'Application/View/nav.php');
        require_once(BASE_PATH . 'Application/View/editor.php');
        require_once(BASE_PATH . 'Application/View/footer.php');
    }

    private function Ajax($post) {
        $action = $post['action'];
        $param = [];

        if(isset($post['param'])) {
            $param = $post['param'];
        }

        require_once('../Application/Model/Ajax/AjaxEditeur.php');
        $ajaxApi = new AjaxEditeur();

        require_once('../Application/Model/blocksModel.php');
        $blocksManager = new BlocksModel();

        require_once('../Application/Model/optionsModel.php');
        $optionsManager = new OptionsModel();

        switch($action) {
            case 'saveLevel' :
                $ajaxApi->saveLevel($param);
                break;
            case 'getAllBlocks' :
                die(json_encode($blocksManager->getAllBlocks()->fetchAll()));
            case 'getAllOptions' :
                die(json_encode($optionsManager->getAllOptions()->fetchAll()));
        }
    }

}
