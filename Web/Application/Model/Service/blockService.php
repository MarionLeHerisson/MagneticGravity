<?php
/**
 * Created by PhpStorm.
 * User: Marion
 * Date: 09/04/2017
 * Time: 15:09
 */

class BlockService {

    /**
     * Create PDO statement to insert blocks linked to a level
     * DOES NOT SUPPORT JSON DATA, to use only with js editor
     * @param int $levelId
     * @param array $blocks
     */
    public function createInsertBlockStatement($levelId, $blocks) {

        /** @var BlocksModel $blocksManager */
        require_once(BASE_PATH . 'Application/Model/blocksModel.php');
        $blocksManager = new BlocksModel();

        /** @var BlockLevelModel $blockLevelManager */
        require_once(BASE_PATH . 'Application/Model/blockLevelModel.php');
        $blockLevelManager = new BlockLevelModel();

        /** @var OptionsModel $optionsManager */
        require_once(BASE_PATH . 'Application/Model/optionsModel.php');
        $optionsManager = new OptionsModel();

        /** @var BlockOptionModel $blockOptionManager */
        require_once(BASE_PATH . 'Application/Model/blockOptionModel.php');
        $blockOptionManager = new BlockOptionModel();

        $allBlocks = $blocksManager->getAllBlocks();
        $allOptions = $optionsManager->getAllOptions()->fetchAll();

        $adapter = [];
        foreach ($allBlocks as $oneBlock) {
            $adapter[$oneBlock['ref']] = $oneBlock['id'];
        }

        $optAdapter = [];
        foreach ($allOptions as $option) {
            $optAdapter[$option['opt_name']] = $option['opt_id'];
        }

        foreach ($blocks as $block) {

            $blockLevelId = $blockLevelManager->insertBlock($adapter[$block['type']], $levelId, $block['x'], $block['y']);
            unset($block['type']);
            unset($block['x']);
            unset($block['y']);

            if(is_array($block)) {
                foreach ($block as $prop => $value) {
                    $blockOptionManager->insertOption($optAdapter[$prop], $blockLevelId, $value);
                }
            }
        }
    }

    /**
     * insert blocks linked to a level and their options
     * DOES NOT SUPPORT JS DATA, to use only with json data
     * @param int $levelId
     * @param array $data
     */
    public function insertBlocksAndOptions($levelId, $data) {

        /** @var BlocksModel $blocksManager */
        require_once(BASE_PATH . 'Application/Model/blocksModel.php');
        $blocksManager = new BlocksModel();

        /** @var OptionsModel $optionsManager */
        require_once(BASE_PATH . 'Application/Model/optionsModel.php');
        $optionsManager = new OptionsModel();

        /** @var BlockLevelModel $blockLevelManager */
        require_once(BASE_PATH . 'Application/Model/blockLevelModel.php');
        $blockLevelManager = new BlockLevelModel();

        /** @var BlockOptionModel $blockOptionManager */
        require_once(BASE_PATH . 'Application/Model/blockOptionModel.php');
        $blockOptionManager = new BlockOptionModel();

        $allBlocks = $blocksManager->getAllBlocks();
        $allOptions = $optionsManager->getAllOptions()->fetchAll();

        // todo : class with methods to get adapters
        $typeAdapter = [];
        foreach ($allBlocks as $oneBlock) {
            $typeAdapter[$oneBlock['json_label']] = $oneBlock['id'];
        }

        $optAdapter = [];
        foreach ($allOptions as $option) {
            $optAdapter[$option['json_label']] = $option['opt_id'];
        }

        foreach($data as $type => $blocks) {    // for each group of blocks
            if(is_array($blocks) && !empty($blocks)) {
                foreach($blocks as $block) {        // for each block of a group

                    $blockLevelId = $blockLevelManager->insertBlock($typeAdapter[$type], $levelId, $block['x'], $block['y']);

                    if(in_array($typeAdapter[$type], [2, 4, 11, 12])) { // if this block can have options

                        foreach ($allOptions as $option) {
                            if(array_key_exists($option['json_label'], $block)) {   // if the block has this option


                                if($block[$option['json_label']] == true && is_bool($block[$option['json_label']])) {
                                    $block[$option['json_label']] = 'true';
                                } elseif ($block[$option['json_label']] == false) {
                                    $block[$option['json_label']] = 'false';
                                }

                                $blockOptionManager->insertOption($optAdapter[$option['json_label']], $blockLevelId, $block[$option['json_label']]);
                            }
                        }
                    }
                }
            }
        }
    }
}