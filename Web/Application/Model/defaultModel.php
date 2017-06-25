<?php

class DefaultModel {
    protected $coBdd = null;

    // connect to bdd
    public function connectDb() {
        if(isset($this->coBdd)) {
            return $this->coBdd;
        }
        else {
            try {
                $bdd = new PDO("mysql:host=" . HOSTNAME . ";dbname=" . DBNAME, DBLOGIN, DBPWD,
                    array (PDO::MYSQL_ATTR_INIT_COMMAND => 'SET NAMES \'UTF8\''));
                // set BDD content in UTF-8, adjust for your needs
            }
            catch(Exception $e) {
                die("erreur :".$e->getMessage());
            }
            $this->coBdd = $bdd;
            return $this->coBdd;
        }
    }
}