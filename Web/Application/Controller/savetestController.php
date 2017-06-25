<?php
/**
 * Created by PhpStorm.
 * User: huma
 * Date: 06/04/17
 * Time: 17:17
 */

class savetestController {

    public function indexAction() {

        echo '<form action="savelevel" method="post">
                <textarea name="json"></textarea>
                <button type="submit">Submit json data</button>
            </form>';

    }
}