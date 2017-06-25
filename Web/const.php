<?php

/* C O N F   L O C A L   W I N D O W S   M A R I O N */
if($_SERVER['HTTP_HOST'] == 'localhost') {
    ini_set("display_errors", 1);
    define('BASE_URL', 'http://localhost' . DIRECTORY_SEPARATOR . 'magneticgravity' . DIRECTORY_SEPARATOR . 'www' . DIRECTORY_SEPARATOR);
    define('BASE_PATH', 'C:' . DIRECTORY_SEPARATOR . 'wamp64' . DIRECTORY_SEPARATOR . 'www' . DIRECTORY_SEPARATOR .
        'magneticgravity' . DIRECTORY_SEPARATOR);
    define('HOSTNAME', 'localhost');
    define('DBNAME', 'magnetic_gravity');
    define('DBLOGIN', 'root');
    define('DBPWD', 'root');
    define('DEBUG', 1);
}

/* P R O D U C T I O N   E N V */
else {
    require_once('prod_const.php');
}
