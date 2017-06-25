<nav class="navbar navbar-inverse navbar-fixed-top mg-nav">
    <div class="container">
        <div class="navbar-header">
            <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </button>
            <a class="navbar-brand" href="<?php echo BASE_URL ?>">Magnetic Gravity</a>
        </div>
        <div id="navbar" class="collapse navbar-collapse">
            <ul class="nav navbar-nav">
                <li <?php echo THISPAGE == '' ? 'class="active"' : '' ?>><a href="<?php echo BASE_URL ?>">Accueil</a></li>
                <li <?php echo THISPAGE == 'niveaux' ? 'class="active"' : '' ?>><a href="<?php echo BASE_URL ?>niveaux">Niveaux de la communauté</a></li>
                <li <?php echo THISPAGE == 'editeur' ? 'class="active"' : '' ?>><a href="<?php echo BASE_URL ?>editeur">Editeur de niveaux</a></li>
                <li <?php echo THISPAGE == 'contact' ? 'class="active"' : '' ?>><a href="<?php echo BASE_URL ?>contact">Contact</a></li>
            </ul>
        </div>
    </div>
</nav>
<?php echo THISPAGE == 'editeur' ? '<img id="clic" src="Medias/clic.png">' : ''; ?>