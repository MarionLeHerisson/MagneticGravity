<?php
/**
 * Created by PhpStorm.
 * User: Marion
 * Date: 19/03/2017
 * Time: 21:54
 */

require_once(BASE_PATH . 'Application/Controller/jsonController.php');

class getlevelController extends jsonController {

    protected $error = [];

    public function indexAction() {

        if (isset($_GET['code']) && $_GET['code'] != '') {

            require_once(BASE_PATH . 'library/strings.php');
            $code = Strings::sanitizeString($_GET['code']);

            require_once(BASE_PATH . 'Application/Model/levelModel.php');
            $levelManager = new levelModel();

            require_once(BASE_PATH . 'Application/Model/blockLevelModel.php');
            $blockLevelManager = new BlockLevelModel();

            require_once(BASE_PATH . 'Application/Model/blocksModel.php');
            $blocksManager = new BlocksModel();

            $level       = $levelManager->getLevelFromCode($code)->fetch(PDO::FETCH_ASSOC);
            if (empty($level)) {
                $this->handleError('Wrong code provided.');
            }

            $levelId     = $level['id'];
            $levelBlocks = $blockLevelManager->getAllBlocks($levelId);
            $allBlocks   = $blocksManager->getAllBlocks()->fetchAll();
            
            // format all existing blocks into an array
            $allLevelBlocks = [];
            $i = 0;
            $j = 0;

            while (is_array($block = $levelBlocks->fetch(PDO::FETCH_ASSOC))) {

                if($i > 0 && $block['bl_id'] == $allLevelBlocks[$i - 1]['bl_id']) {

                    $i--;
                    $j++;
                    $allLevelBlocks[$i]['options'][$j] = [
                        'opt_id'     => $block['opt_id'],
                        'opt_value'  => $block['opt_value'],
                        'json_label' => $block['json_label'],
                        'min_val'    => $block['min_val']
                    ];
                } else {
                    $j = 0;
                    $allLevelBlocks[$i] = [
                        'bl_id'    => $block['bl_id'],
                        'block_id' => $block['block_id'],
                        'posx'     => $block['posx'],
                        'posy'     => $block['posy']
                    ];
                    $allLevelBlocks[$i]['options'][$j] = [
                        'opt_id'     => $block['opt_id'],
                        'opt_value'  => $block['opt_value'],
                        'json_label' => $block['json_label'],
                        'min_val'    => $block['min_val']
                    ];
                }

                $i++;
            }

            // format all level blocks into an array
            foreach ($allBlocks as $block) {
                foreach ($allLevelBlocks as $levelBlock) {
                    if ($block['id'] == $levelBlock['block_id']) {
                        $thisBlock = [
                            'x' => $levelBlock['posx'],
                            'y' => $levelBlock['posy']
                        ];

                        foreach ($levelBlock['options'] as $blOpt) {
                            if($blOpt['opt_id'] != '') {
                                $thisBlock[$blOpt['json_label']] = $blOpt['opt_value'];
                            }
                        }

                        $level[$block['json_label']][] = $thisBlock;
                    }
                }
            }

            echo json_encode($level);
            //print_r($level);

            die();

        } else {
            $this->handleError('No code provided.');
        }
    }
}