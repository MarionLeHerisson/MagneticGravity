<?php
/**
 * Created by PhpStorm.
 * User: Marion
 * Date: 22/04/2017
 * Time: 23:55
 */

class LevelService {

    public function getLevel($code) {

        require_once(BASE_PATH . 'Application/Model/levelModel.php');
        $levelManager = new levelModel();

        require_once(BASE_PATH . 'Application/Model/blockLevelModel.php');
        $blockLevelManager = new BlockLevelModel();

        require_once(BASE_PATH . 'Application/Model/blocksModel.php');
        $blocksManager = new BlocksModel();

        $level = $levelManager->getLevelFromCode($code)->fetch(PDO::FETCH_ASSOC);

        if(empty($level)) {
            $this->handleError('Wrong code provided.');
        }

        // TODO : avoid repetition with getLevelController
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

        return $level;
    }


    /**
     * Create HTML level to display
     * @param array $level
     * @return String
     */
    public function htmlEncodeLevel($level) {

        require_once(BASE_PATH . 'Application/Model/blocksModel.php');
        $blocksManager = new BlocksModel();

        $allBlocks = $blocksManager->getAllBlocks()->fetchAll();
        $levelMatrix = [];
        $imax = 0;
        $jmin = 126;

        foreach($level as $blockType => $blockChildren) {
            foreach($allBlocks as $block) {
                if($blockType == $block['json_label']) {
                    foreach($blockChildren as $child) {
                        $x = $child['x'];
                        $y = $child['y'];
                        $levelMatrix[$x][$y] = $block['ref'];

                        if(array_key_exists('StrongIntensity', $child) && $child['StrongIntensity'] == 'true') {
                            if(array_key_exists('NegativePolarity', $child) && $child['NegativePolarity'] == 'true') {
                                $levelMatrix[$x][$y] .= '-';
                            } else {
                                $levelMatrix[$x][$y] .= '++';
                            }
                        }
                        if(array_key_exists('NegativePolarity', $child) && $child['NegativePolarity'] == 'true') {
                            $levelMatrix[$x][$y] .= '-';
                        }

                        if($x > $imax) {
                            $imax = $x;
                        }
                        if($y < $jmin) {
                            $jmin = $y;
                        }
                    }
                }
            }
        }

        $htmlLevel = '<table>';

        for($j = 126; $j >= $jmin; $j--) {
            $htmlLevel .= '<tr>';
            for($i = 0; $i < $imax + 1; $i++) {
                $htmlLevel .= '<td><div class="dropSquare" data-coord="'.$j.','.$i.'">';
                if(isset($levelMatrix[$i][$j])) {
                    $htmlLevel .= '<img src="Medias/Assets/' . $levelMatrix[$i][$j] . '.png" width="100" height="50">';
                }
                $htmlLevel .= '</div></td>';
            }
            $htmlLevel .= '</tr>';
        }

        $htmlLevel .= '</table>';

        return $htmlLevel;
    }

    /**
     * Create jpeg (or tiny html ?) thumbnail of a level
     * @param $level
     * @return Object|String
     */
    public function jpgLevelPreview($level) {

        return $level;
    }

    public function handleError($msg) {
        $error['error'] = $msg;
        die(json_encode($error));
    }
}
