<?php
/**
 * Created by PhpStorm.
 * User: Marion
 * Date: 27/02/2017
 * Time: 13:54
 */

require_once(BASE_PATH . 'library/strings.php');
require_once(BASE_PATH . 'library/webservice.php');


class AjaxEditeur {

    /**
     * Save level data and all level blocks
     * @param $data
     */
    public function saveLevel($data) {
        // MANAGERS
        /** @var LevelModel $levelManager */
        require_once(BASE_PATH . 'Application/Model/levelModel.php');
        $levelManager = new LevelModel();

        /** @var BlockService $blockService */
        require_once(BASE_PATH . 'Application/Model/Service/blockService.php');
        $blockService = new BlockService();

        if($levelManager->existLevel(Strings::sanitizeString($data['title']), 'title')) {
            die(json_encode([
                'stat'  	=> 'ko',
                'msg'	    => 'Ce nom existe déjà...'
            ]));
        }

        $code = webservice::generateCode();

        // insert level informations and get level id
        $levelId = $levelManager->insertLevel($data['title'], $code);

        // insert blocs informations
        $blockService->createInsertBlockStatement($levelId, $data['blocks']);

        die(json_encode([
            'stat'  	=> 'ok',
            'code'  	=> $code,
            'msg'	    => 'Niveau enregistré avec succès !'
        ]));
    }
}