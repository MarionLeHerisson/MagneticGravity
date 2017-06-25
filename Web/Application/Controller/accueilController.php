<?php

class accueilController {

    public function indexAction() {

        require_once(BASE_PATH . 'Application/View/header.php');
        require_once(BASE_PATH . 'Application/View/nav.php');
        require_once(BASE_PATH . 'Application/View/home.php');
        require_once(BASE_PATH . 'Application/View/footer.php');
    }
}