<?php
/**
 * Created by PhpStorm.
 * User: Marion
 * Date: 21/01/2017
 * Time: 18:42
 */

class contactController {

    public function indexAction() {

        require_once(BASE_PATH . 'Application/View/header.php');
        require_once(BASE_PATH . 'Application/View/nav.php');
        require_once(BASE_PATH . 'Application/View/contact.php');
        require_once(BASE_PATH . 'Application/View/footer.php');
    }
}