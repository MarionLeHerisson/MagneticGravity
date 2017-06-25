<?php
/**
 * Created by PhpStorm.
 * User: Marion
 * Date: 21/01/2017
 * Time: 18:50
 */

require_once('const.php');
require_once('Application/View/header.php');
?>

    <div class="cover-container cover-index">
        <div class="inner cover">
            <br><br><br>
            <h1 class="cover-heading">Oups !</h1>
            <p class="lead">Il semblerait que le Javascript soit désactivé sur votre navigateur.</p>
            <br><br><br>
        </div>
    </div>

    <div class="container">
    <h3>Pour continuer vers notre site, activez le Javascript en suivant les étapes suivantes :</h3>
    <br><br>
    <p>Dans le menu en haut à droite de votre navigateur, cliquez sur <b>Paramètres</b>.<br>
        En bas de la page qui s'ouvre, cliquez sur <b>Afficher les paramètres avancés</b>.<br>
        Dans <b>Confidentialité</b>, cliquez sur <b>Paramètres de contenu</b>.<br><br>
        Dans la partie Javascript, cohez <b>Autoriser tous les sites à exécuter JavaScript (recommandé)</b>.<br>
        Si vous ne souhaitez autoriser que notre site, cliquez sur <b>Gérer les exceptions</b>,
        collez <input class="input-sm" type="text" value="<?php echo BASE_URL; ?>"> et veillez à ce que l'action
        sélectionnée soit <b>"autoriser"</b> !<br><br>
        Vous pouvez maintenant naviguer sur notre site !<br></p>

    <a href="accueil"><input type="button" class="btn btn-primary btn-group-sm" value="Réessayer"></a>

<?php
require_once('Application/View/footer.php');
?>