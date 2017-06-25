using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class Utils {

    // Web Service

    //public const string SERVEUR_WEB = "http://localhost/magneticgravity/";
    public const string SERVEUR_WEB = "http://marionhurteau.fr.nf/";

    public const string LIEN_SAUVEGARDE_NIVEAU = SERVEUR_WEB + "savelevel";
    public const string LIEN_TELECHARGE_NIVEAU = SERVEUR_WEB + "getlevel";
    public const string LIEN_LISTE_NIVEAUX = SERVEUR_WEB + "levelslist";
    public const string LIEN_AFFICHE_NIVEAU = SERVEUR_WEB + "display";

    // JSON

    public const int JSON_AUCUN = 0;
    public const int JSON_EDITOR = 1;
    public const int JSON_MES_NIVEAUX = 2;
    public const int JSON_TELECHARGE = 3;

    // Ralenti

    public const float graviteY = -20.0f;
    public static float facteurTemps = 1.0f;

    public static void AnnuleRalenti() {
        facteurTemps = 1.0f;
        //Physics2D.gravity = new Vector2(0f, graviteY);
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;
    }

    public static void SetRalenti(float facteurRalenti) {
        //facteurTemps = facteurRalenti;
        //Physics2D.gravity = new Vector2(0f, graviteY * facteurRalenti * facteurRalenti);
        Time.timeScale = facteurRalenti;
        Time.fixedDeltaTime = 0.02f * facteurRalenti;
    }


    // Sauvegarde

    [Serializable]
    public class MondeNiveau {
        public bool reussi = false;
        public int bonus = 0;
    }

    [Serializable]
    public class Partie {

        public MondeNiveau[,] mondes = new MondeNiveau[5,5];
        public int mondeActuel = 0;
        public int niveauActuel = 0;
        public int bonusActuel = 0;

        public Partie() {
            Initialise();
        }

        public void TermineNiveau(int monde, int niveau, int bonus) {

            mondes[monde, niveau].reussi = true;
            if (bonus > mondes[monde, niveau].bonus) mondes[monde, niveau].bonus = bonus;
        }

        public int GetDernierMonde() {

            int dernierMonde = 0;
            
            for (var i = 0; i < 5; i++) {
                for (var j = 0; j < 5; j++) {
                    dernierMonde = i;
                    if (mondes[i, j].reussi == false) return dernierMonde;
                }
            }

            return dernierMonde;
        }

        public int GetDernierNiveau() {

            int dernierNiveau = 0;

            for (var i = 0; i < 5; i++) {
                for (var j = 0; j < 5; j++) {
                    dernierNiveau = j;
                    if (mondes[i, j].reussi == false) return dernierNiveau;
                }
            }

            return dernierNiveau;
        }

        public void DebloqueTousLesNiveaux() {
            for (var i = 0; i < 5; i++) {
                for (var j = 0; j < 5; j++) {
                    mondes[i, j].reussi = true;
                }
            }
        }

        public void Initialise() {
            for (var i = 0; i < 5; i++) {
                for (var j = 0; j < 5; j++) {
                    mondes[i, j] = new MondeNiveau();
                }
            }
        }
    }

    public static Partie partie = new Partie();

    public static void SauvegarderPartie() {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MagneticGravity_sauvegarde.sav");
        bf.Serialize(file, partie);
        file.Close();
    }

    public static void ChargerPartie() {

        if (File.Exists(Application.persistentDataPath + "/MagneticGravity_sauvegarde.sav")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MagneticGravity_sauvegarde.sav", FileMode.Open);
            partie = (Partie)bf.Deserialize(file);
            file.Close();
        }
    }


    // Changement de niveau

    public static void LoadLevel(int monde = -1, int niveau = -1) {

        if (monde != -1) partie.mondeActuel = monde;
        if (niveau != -1) partie.niveauActuel = niveau;
        partie.bonusActuel = 0;

        SceneManager.LoadScene("World" + (partie.mondeActuel + 1) + "_Level" + (partie.niveauActuel + 1));
    }

    public static void LoadNextLevel() {

        partie.niveauActuel++;

        if (partie.niveauActuel > 4) {
            partie.niveauActuel = 0;
            partie.mondeActuel++;
        }

        if (partie.mondeActuel > 4) {
            partie.mondeActuel = 4;
        }

        LoadLevel();
    }

    public static void FinishLevel() {
        partie.TermineNiveau(partie.mondeActuel, partie.niveauActuel, partie.bonusActuel);
        SauvegarderPartie();
    }

    // Partage

    public const string PARTAGE_NOM = "Magnetic Gravity";

    public static void PartageTwitter(string code) {
        Application.OpenURL("http://twitter.com/intent/tweet" +
      "?text=" + WWW.EscapeURL(PARTAGE_NOM + "\nVenez voir le niveau que j'ai créé dans le jeu Magnetic Gravity !\n" + LIEN_AFFICHE_NIVEAU + "?code=" + code));
    }

    public static void PartageFacebook(string code) {
        // Facebook n'autorise plus les partages pré-remplis.
        Application.OpenURL("https://www.facebook.com/sharer/sharer.php?u=" + LIEN_AFFICHE_NIVEAU + "%3Fcode=" + code);
    }

    public static void PartageGooglePlus(string code) {
        Application.OpenURL("https://plus.google.com/share?url=" + LIEN_AFFICHE_NIVEAU + "%3Fcode=" + code +
      "&text=" + WWW.EscapeURL(PARTAGE_NOM + "\nVenez voir le niveau que j'ai créé dans le jeu Magnetic Gravity !\n"));
    }


    public static void PartageTwitter() {
        Application.OpenURL("http://twitter.com/intent/tweet" +
      "?text=" + WWW.EscapeURL(PARTAGE_NOM + "\nVenez essayer le magnifique jeu Magnetic Gravity !\n" + SERVEUR_WEB));
    }

    public static void PartageFacebook() {
        // Facebook n'autorise plus les partages pré-remplis.
        Application.OpenURL("https://www.facebook.com/sharer/sharer.php?u=" + SERVEUR_WEB);
    }

    public static void PartageGooglePlus() {
        Application.OpenURL("https://plus.google.com/share?url=" + SERVEUR_WEB +
      "&text=" + WWW.EscapeURL(PARTAGE_NOM + "\nVenez essayer le magnifique jeu Magnetic Gravity !\n"));
    }
    
}
