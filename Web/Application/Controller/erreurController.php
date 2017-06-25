<?php

class erreurController {

    public function indexAction() {

        require_once(BASE_PATH . 'Application/View/header.php');
        require_once(BASE_PATH . 'Application/View/nav.php');
        require_once(BASE_PATH . 'Application/View/error.php');
        require_once(BASE_PATH . 'Application/View/footer.php');
    }
}