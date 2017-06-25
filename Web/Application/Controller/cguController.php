<?php

class cguController {

    public function indexAction() {

        require_once(BASE_PATH . 'Application/View/header.php');
        require_once(BASE_PATH . 'Application/View/nav.php');
        require_once(BASE_PATH . 'Application/View/cgu.php');
        require_once(BASE_PATH . 'Application/View/footer.php');
    }
}