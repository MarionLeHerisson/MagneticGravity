<div id="wrapper" class="">
    <?php
        require_once('sidebar.php');
    ?>
    <!-- Page Content -->
    <div id="page-content-wrapper">
        <button type="button" class="hamburger is-closed" data-toggle="offcanvas">
            <span class="hamb-top"></span>
            <span class="hamb-middle"></span>
            <span class="hamb-bottom"></span>
        </button>

        <div class="container">
            <div class="row">
                <div class="col-lg-8 col-lg-offset-2">
                    <h1>Editeur de niveaux</h1>
                </div>
            </div>

            <button type="button" id="btn_test" class="btn btn-default">Tester ce niveau</button>
            <!-- todo <button type="button" id="btn_test" class="btn btn-default">Vider la grille</button> -->
            <div class="form-group">
                <label for="lvlNewName">Nom du niveau :</label>
                <input type="text" class="form-control" id="lvlNewName">
            </div>

            <div id="submitTest" class="none alert alert-dismissible fade in col-md-12" role="alert">
                <button type="button" class="close" onclick="closePopin()">
                    <span>×</span>
                </button>
                <p id="submitTestMsg"></p>
            </div>

            <p id="lvlCode"></p>
                <div id="grid"></div>
        </div>
    </div>

</div>
<script src="js/editor.js"></script>
<div id="hidden_trash" class="hidden"></div>
<?php require_once ('Blocks/scrollArrow.php');