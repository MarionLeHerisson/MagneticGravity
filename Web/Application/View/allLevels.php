<?php
/**
 * Created by PhpStorm.
 * User: Marion
 * Date: 13/06/2017
 * Time: 18:37
 */

?>

<div class="container">
    <div class="col-lg-2"></div>
    <div class="col-lg-8">
        <h1>Les niveaux de la communauté</h1>
        <div class="list-group">
            <?php echo $htmlList; ?>
        </div>
    </div>
    <div class="col-lg-2"></div>
</div>

<?php require_once ('Blocks/scrollArrow.php');