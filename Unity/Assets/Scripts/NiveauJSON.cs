using UnityEngine;
using System.Collections;
using System;

public class NiveauJSON : MonoBehaviour {

    struct VecInt2D {
        public int x;
        public int y;
        public bool valide;
    };

    private const float largeurBloc = 3.0f;
    private const float hauteurBloc = 1.5f;
    private const float largeurBloc05 = 1.5f;
    private const float hauteurBloc05 = 0.75f;

    [SerializeField]
    private ControlesPerso controlesperso;

    [SerializeField]
    public BlockStart blocStart;

    [SerializeField]
    public BlockEnd blocEnd;

    [SerializeField]
    public GameObject[] blocsSimples;

    [SerializeField]
    public BlockMort[] blocsTraps;

    [SerializeField]
    public BlockMagnetic[] blocsMagnetics;

    [SerializeField]
    public BlockReverse[] blocsRunReverses;

    [SerializeField]
    public BlockPolarityReverse[] blocsPolarityReverses;

    [SerializeField]
    public BlockStop[] blocsStops;

    [SerializeField]
    public Checkpoint[] blocsCheckpoints;

    [SerializeField]
    public Collectibles[] blocsCollectibles;

    [SerializeField]
    public GameObject[] blocsTurrets;

    [SerializeField]
    public Ralenti[] blocsRalenti;

    [Serializable]
    public class Bloc {
        public int x;
        public int y;
    }

    [Serializable]
    public class BlocRalenti : Bloc {
        public float duree;
    }

    [Serializable]
    public class BlocTourelle : Bloc {
        public int ballsByShot;
        public float waitBalls;
        public float waitShoot;
    }

    [Serializable]
    public class BlocMagnetique : Bloc {
        public bool StrongIntensity;
        public bool NegativePolarity;
    }

    [Serializable]
    public class Niveau {
        public int id;
        public string mac;
        public string code;
        public int author;
        public string title;
        public float score;
        public float hardness;
        public int finished;
        public Bloc[] start;
        public Bloc[] end;
        public Bloc[] simple;
        public BlocMagnetique[] magnetic;
        public Bloc[] trap;
        public Bloc[] runReverse;
        public Bloc[] polarityReverse;
        public Bloc[] stop;
        public Bloc[] checkpoint;
        public Bloc[] collectible;
        public BlocTourelle[] turret;
        public BlocRalenti[] ralenti;
        public string img;
    }

    public string titre = "";
    public string code = "";

    public string GetTitre() {
        return titre;
    }

    private Niveau NiveauDefaut() {
        var niveau = new Niveau();
        niveau.id = 0;
        niveau.code = "";
        niveau.mac = "";
        niveau.author = 0;
        niveau.title = "Niveau par défaut";
        niveau.start = new Bloc[1];
        niveau.start[0] = new Bloc();
        niveau.start[0].x = 0;
        niveau.start[0].y = 0;
        niveau.end = new Bloc[1];
        niveau.end[0] = new Bloc();
        niveau.end[0].x = 5;
        niveau.end[0].y = 0;
        return niveau;
    }

    private Niveau ParseJSON(string json) {
        
        if (json != "") {
            try {
                Niveau niveau = JsonUtility.FromJson<Niveau>(json);
                return niveau;
            }
            catch (Exception e) {
                Debug.Log(e);
            }
        }
        Niveau niveauDefaut = NiveauDefaut();
        return niveauDefaut;
    }

    public void VideNiveau() {

        // Désactive les blocs simples
        foreach (var bloc in blocsSimples) {
            bloc.transform.position = new Vector3(0f, 0f, 0f);
            bloc.SetActive(false);
        }

        // Désactive les blocs traps
        foreach (var bloc in blocsTraps) {
            bloc.transform.position = new Vector3(0f, 0f, 0f);
            bloc.gameObject.SetActive(false);
        }

        // Désactive les blocs magnétiques
        foreach (var bloc in blocsMagnetics) {
            bloc.transform.position = new Vector3(0f, 0f, 0f);
            bloc.gameObject.SetActive(false);
        }

        // Désactive les blocs run reverse
        foreach (var bloc in blocsRunReverses) {
            bloc.transform.position = new Vector3(0f, 0f, 0f);
            bloc.gameObject.SetActive(false);
        }

        // Désactive les blocs polarity reverse
        foreach (var bloc in blocsPolarityReverses) {
            bloc.transform.position = new Vector3(0f, 0f, 0f);
            bloc.gameObject.SetActive(false);
        }

        // Désactive les blocs stop
        foreach (var bloc in blocsStops) {
            bloc.transform.position = new Vector3(0f, 0f, 0f);
            bloc.gameObject.SetActive(false);
        }

        // Désactive les blocs checkpoint
        foreach (var bloc in blocsCheckpoints) {
            bloc.transform.position = new Vector3(0f, 0f, 0f);
            bloc.gameObject.SetActive(false);
        }

        // Désactive les blocs collectibles
        foreach (var bloc in blocsCollectibles) {
            bloc.transform.position = new Vector3(0f, 0f, 0f);
            bloc.gameObject.SetActive(false);
        }

        // Désactive les blocs tourelle
        foreach (var bloc in blocsTurrets) {
            bloc.transform.position = new Vector3(0f, 0f, 0f);
            bloc.gameObject.SetActive(false);
        }

        // Désactive les blocs ralentis
        foreach (var bloc in blocsRalenti) {
            bloc.transform.position = new Vector3(0f, 0f, 0f);
            bloc.gameObject.SetActive(false);
        }

    }

    // Positionne les blocs simples
    private void ConstruitBlocsSimples(Bloc[] JSONblocs) {

        if (JSONblocs == null) return;

        var nbBlocsMax = JSONblocs.Length;
        if (nbBlocsMax > blocsSimples.Length) nbBlocsMax = blocsSimples.Length;
        Bloc JSONbloc;
        GameObject bloc;

        for (var i = 0; i < nbBlocsMax; i++) {

            JSONbloc = JSONblocs[i];
            bloc = blocsSimples[i];

            bloc.transform.position = new Vector3(JSONbloc.x * largeurBloc, JSONbloc.y * hauteurBloc, 0f);
            bloc.SetActive(true);
        }

    }

    // Positionne les blocs traps
    private void ConstruitBlocsTraps(Bloc[] JSONblocs) {

        if (JSONblocs == null) return;

        var nbBlocsMax = JSONblocs.Length;
        if (nbBlocsMax > blocsTraps.Length) nbBlocsMax = blocsTraps.Length;
        Bloc JSONbloc;
        BlockMort bloc;

        for (var i = 0; i < nbBlocsMax; i++) {

            JSONbloc = JSONblocs[i];
            bloc = blocsTraps[i];

            bloc.transform.position = new Vector3(JSONbloc.x * largeurBloc, JSONbloc.y * hauteurBloc, 0f);
            bloc.gameObject.SetActive(true);
        }

    }

    // Positionne et paramètrise les blocs magnétiques
    private void ConstruitBlocsMagnetics(BlocMagnetique[] JSONblocs) {

        if (JSONblocs == null) return;

        var nbBlocsMax = JSONblocs.Length;
        if (nbBlocsMax > blocsTraps.Length) nbBlocsMax = blocsTraps.Length;
        BlocMagnetique JSONbloc;
        BlockMagnetic bloc;

        for (var i = 0; i < nbBlocsMax; i++) {

            JSONbloc = JSONblocs[i];
            bloc = blocsMagnetics[i];

            bloc.transform.position = new Vector3(JSONbloc.x * largeurBloc, JSONbloc.y * hauteurBloc, 0f);
            bloc.SetPolariteNegative(JSONbloc.NegativePolarity);
            bloc.SetIntensiteForte(JSONbloc.StrongIntensity);
            bloc.gameObject.SetActive(true);
            bloc.majPolariteText();
        }

    }

    // Positionne les blocs run reverse
    private void ConstruitBlocsRunReverses(Bloc[] JSONblocs) {

        if (JSONblocs == null) return;

        var nbBlocsMax = JSONblocs.Length;
        if (nbBlocsMax > blocsTraps.Length) nbBlocsMax = blocsTraps.Length;
        Bloc JSONbloc;
        BlockReverse bloc;

        for (var i = 0; i < nbBlocsMax; i++) {

            JSONbloc = JSONblocs[i];
            bloc = blocsRunReverses[i];

            bloc.transform.position = new Vector3(JSONbloc.x * largeurBloc, JSONbloc.y * hauteurBloc, 0f);
            bloc.gameObject.SetActive(true);
        }

    }

    // Positionne les blocs polarity reverse
    private void ConstruitBlocsPolarityReverses(Bloc[] JSONblocs) {

        if (JSONblocs == null) return;

        var nbBlocsMax = JSONblocs.Length;
        if (nbBlocsMax > blocsTraps.Length) nbBlocsMax = blocsTraps.Length;
        Bloc JSONbloc;
        BlockPolarityReverse bloc;

        for (var i = 0; i < nbBlocsMax; i++) {

            JSONbloc = JSONblocs[i];
            bloc = blocsPolarityReverses[i];

            bloc.transform.position = new Vector3(JSONbloc.x * largeurBloc, JSONbloc.y * hauteurBloc, 0f);
            bloc.gameObject.SetActive(true);
        }

    }

    // Positionne les blocs stop
    private void ConstruitBlocsStops(Bloc[] JSONblocs) {

        if (JSONblocs == null) return;

        var nbBlocsMax = JSONblocs.Length;
        if (nbBlocsMax > blocsTraps.Length) nbBlocsMax = blocsTraps.Length;
        Bloc JSONbloc;
        BlockStop bloc;

        for (var i = 0; i < nbBlocsMax; i++) {

            JSONbloc = JSONblocs[i];
            bloc = blocsStops[i];

            bloc.transform.position = new Vector3(JSONbloc.x * largeurBloc, JSONbloc.y * hauteurBloc, 0f);
            bloc.gameObject.SetActive(true);
        }

    }

    // Positionne les blocs stop
    private void ConstruitBlocsCheckpoints(Bloc[] JSONblocs) {

        if (JSONblocs == null) return;

        var nbBlocsMax = JSONblocs.Length;
        if (nbBlocsMax > blocsTraps.Length) nbBlocsMax = blocsTraps.Length;
        Bloc JSONbloc;
        Checkpoint bloc;

        for (var i = 0; i < nbBlocsMax; i++) {

            JSONbloc = JSONblocs[i];
            bloc = blocsCheckpoints[i];

            bloc.transform.position = new Vector3(JSONbloc.x * largeurBloc, JSONbloc.y * hauteurBloc, 0f);
            bloc.gameObject.SetActive(true);
        }

    }

    // Positionne le bloc start et le personnage dessus
    private void ConstruitStart(Bloc[] JSONblocs) {

        if (JSONblocs == null || JSONblocs.Length == 0) return;

        var pos = new Vector3();
        Bloc JSONbloc = JSONblocs[0];

        if (JSONbloc != null) {
            pos = new Vector3(JSONbloc.x * largeurBloc, JSONbloc.y * hauteurBloc, 0f);
        }

        blocStart.gameObject.transform.position = pos;
        blocStart.gameObject.SetActive(true);

        controlesperso.gameObject.transform.position = new Vector3(pos.x, pos.y + 2.0f, 0f);
        controlesperso.gameObject.SetActive(true);
    }

    // Positionne le bloc end
    private void ConstruitEnd(Bloc[] JSONblocs) {

        if (JSONblocs == null || JSONblocs.Length == 0) return;

        var pos = new Vector3(largeurBloc, 0f, 0f);
        Bloc JSONbloc = JSONblocs[0];

        if (JSONbloc != null) {
            pos = new Vector3(JSONbloc.x * largeurBloc, JSONbloc.y * hauteurBloc, 0f);
        }

        blocEnd.gameObject.transform.position = pos;
        blocEnd.gameObject.SetActive(true);
    }

    // Positionne les blocs collectibles
    private void ConstruitBlocsCollectibles(Bloc[] JSONblocs) {

        if (JSONblocs == null) return;

        var nbBlocsMax = JSONblocs.Length;
        if (nbBlocsMax > blocsTraps.Length) nbBlocsMax = blocsTraps.Length;
        Bloc JSONbloc;
        Collectibles bloc;

        for (var i = 0; i < nbBlocsMax; i++) {

            JSONbloc = JSONblocs[i];
            bloc = blocsCollectibles[i];

            bloc.transform.position = new Vector3(JSONbloc.x * largeurBloc, JSONbloc.y * hauteurBloc, 0f);
            bloc.gameObject.SetActive(true);
        }
    }

    // Positionne les blocs tourelles
    private void ConstruitBlocsTourelles(BlocTourelle[] JSONblocs) {

        if (JSONblocs == null) return;

        var nbBlocsMax = JSONblocs.Length;
        if (nbBlocsMax > blocsTraps.Length) nbBlocsMax = blocsTraps.Length;
        BlocTourelle JSONbloc;
        GameObject bloc;

        for (var i = 0; i < nbBlocsMax; i++) {

            JSONbloc = JSONblocs[i];
            bloc = blocsTurrets[i];

            Tourelle_ShootSpeed tourelleShootSpeed = bloc.GetComponentInChildren<Tourelle_ShootSpeed>();
            tourelleShootSpeed.SetBallsByShot(JSONbloc.ballsByShot);
            tourelleShootSpeed.SetWaitBetweenBalls(JSONbloc.waitBalls);
            tourelleShootSpeed.SetWaitBetweenShoot(JSONbloc.waitShoot);

            bloc.transform.position = new Vector3(JSONbloc.x * largeurBloc, JSONbloc.y * hauteurBloc, 0f);
            bloc.gameObject.SetActive(true);
        }
    }

    // Positionne les pillules de ralenti
    private void ConstruitBlocsRalenti(BlocRalenti[] JSONblocs) {

        if (JSONblocs == null) return;

        var nbBlocsMax = JSONblocs.Length;
        if (nbBlocsMax > blocsRalenti.Length) nbBlocsMax = blocsRalenti.Length;
        BlocRalenti JSONbloc;
        Ralenti bloc;

        for (var i = 0; i < nbBlocsMax; i++) {

            JSONbloc = JSONblocs[i];
            bloc = blocsRalenti[i];

            bloc.SetDuree(JSONbloc.duree);

            bloc.transform.position = new Vector3(JSONbloc.x * largeurBloc, JSONbloc.y * hauteurBloc, 0f);
            bloc.gameObject.SetActive(true);
        }
    }


    // Positionne tous les blocs du JSON
    public void ConstruitNiveau(Niveau niveau) {

        ConstruitStart(niveau.start);
        ConstruitEnd(niveau.end);
        ConstruitBlocsSimples(niveau.simple);
        ConstruitBlocsTraps(niveau.trap);
        ConstruitBlocsMagnetics(niveau.magnetic);
        ConstruitBlocsRunReverses(niveau.runReverse);
        ConstruitBlocsPolarityReverses(niveau.polarityReverse);
        ConstruitBlocsStops(niveau.stop);
        ConstruitBlocsCheckpoints(niveau.checkpoint);
        ConstruitBlocsCollectibles(niveau.collectible);
        ConstruitBlocsTourelles(niveau.turret);
        ConstruitBlocsRalenti(niveau.ralenti);

        titre = niveau.title;
        code = niveau.code;
    }
    
    public void Charge(string jsonStr) {
        Niveau niveau = ParseJSON(jsonStr);
        VideNiveau();
        ConstruitNiveau(niveau);
    }


    // Convertit la position d'un bloc vers des coordonnées de grille

    private VecInt2D Vector3ToVecInt2D(Vector3 pos) {

        VecInt2D vecInt = new VecInt2D();

        int x = (int)((pos.x + largeurBloc05) / largeurBloc);
        int y = (int)((pos.y + hauteurBloc05) / hauteurBloc);

        vecInt.x = x;
        vecInt.y = y;

        if (x >= 0 && y >= 0
            && x < 128 && y < 128) {

            vecInt.valide = true;
        }
        else {
            vecInt.valide = false;
        }

        return vecInt;
    }

    private Bloc PosToBlock(GameObject go) {

        Bloc bloc = new Bloc();
        VecInt2D pos = Vector3ToVecInt2D(go.transform.position);

        if (!pos.valide) return null;

        bloc.x = pos.x;
        bloc.y = pos.y;
        return bloc;
    }
    
    public string ExporteJSON(int[,] grilleObjets, string titre = "Sans titre", int auteur = 0, string code = "", string mac = "", string img = "") {

        string json = "test";

        Niveau niveauJSON = new Niveau();
        Bloc blocJSON;
        int nb = 0;
        int i = 0;

        niveauJSON.author = auteur;
        niveauJSON.title = titre;
        niveauJSON.mac = mac;
        niveauJSON.code = code;
        niveauJSON.finished = 1;
        niveauJSON.img = img;

        // Start

        blocJSON = PosToBlock(blocStart.gameObject);
        if (blocJSON != null) {
            niveauJSON.start = new Bloc[1];
            niveauJSON.start[0] = blocJSON;
        }

        // End

        blocJSON = PosToBlock(blocEnd.gameObject);
        if (blocJSON != null) {
            niveauJSON.end = new Bloc[1];
            niveauJSON.end[0] = blocJSON;
        }

        // Simple

        nb = 0; i = 0;
        foreach (var bloc in blocsSimples) { if (bloc.gameObject.activeSelf && PosToBlock(bloc.gameObject) != null) nb++; }
        niveauJSON.simple = new Bloc[nb];

        foreach (var bloc in blocsSimples) {
            if (bloc.gameObject.activeSelf) {
                blocJSON = PosToBlock(bloc.gameObject);
                if (blocJSON != null) niveauJSON.simple[i++] = blocJSON;
            }
        }

        // Magnétiques

        nb = 0; i = 0;
        foreach (var bloc in blocsMagnetics) { if (bloc.gameObject.activeSelf && PosToBlock(bloc.gameObject) != null) nb++; }
        niveauJSON.magnetic = new BlocMagnetique[nb];

        foreach (var bloc in blocsMagnetics) {
            if (bloc.gameObject.activeSelf) {
                blocJSON = PosToBlock(bloc.gameObject);
                if (blocJSON != null) {
                    // BlocMagnetique blocMagnetiqueJSON = (BlocMagnetique)blocJSON; // Cast marche pas
                    BlocMagnetique blocMagnetiqueJSON = new BlocMagnetique();
                    blocMagnetiqueJSON.x = blocJSON.x;
                    blocMagnetiqueJSON.y = blocJSON.y;
                    blocMagnetiqueJSON.NegativePolarity = bloc.GetPolariteNegative();
                    blocMagnetiqueJSON.StrongIntensity = bloc.GetIntensiteForte();
                    niveauJSON.magnetic[i++] = blocMagnetiqueJSON;
                }
            }
        }

        // Polarity reverse

        nb = 0; i = 0;
        foreach (var bloc in blocsPolarityReverses) { if (bloc.gameObject.activeSelf && PosToBlock(bloc.gameObject) != null) nb++; }
        niveauJSON.polarityReverse = new Bloc[nb];

        foreach (var bloc in blocsPolarityReverses) {
            if (bloc.gameObject.activeSelf) {
                blocJSON = PosToBlock(bloc.gameObject);
                if (blocJSON != null) niveauJSON.polarityReverse[i++] = blocJSON;
            }
        }

        // Run reverse

        nb = 0; i = 0;
        foreach (var bloc in blocsRunReverses) { if (bloc.gameObject.activeSelf && PosToBlock(bloc.gameObject) != null) nb++; }
        niveauJSON.runReverse = new Bloc[nb];

        foreach (var bloc in blocsRunReverses) {
            if (bloc.gameObject.activeSelf) {
                blocJSON = PosToBlock(bloc.gameObject);
                if (blocJSON != null) niveauJSON.runReverse[i++] = blocJSON;
            }
        }

        // Traps

        nb = 0; i = 0;
        foreach (var bloc in blocsTraps) { if (bloc.gameObject.activeSelf && PosToBlock(bloc.gameObject) != null) nb++; }
        niveauJSON.trap = new Bloc[nb];

        foreach (var bloc in blocsTraps) {
            if (bloc.gameObject.activeSelf) {
                blocJSON = PosToBlock(bloc.gameObject);
                if (blocJSON != null) niveauJSON.trap[i++] = blocJSON;
            }
        }

        // Stop

        nb = 0; i = 0;
        foreach (var bloc in blocsStops) { if (bloc.gameObject.activeSelf && PosToBlock(bloc.gameObject) != null) nb++; }
        niveauJSON.stop = new Bloc[nb];

        foreach (var bloc in blocsStops) {
            if (bloc.gameObject.activeSelf) {
                blocJSON = PosToBlock(bloc.gameObject);
                if (blocJSON != null) niveauJSON.stop[i++] = blocJSON;
            }
        }

        // Checkpoint

        nb = 0; i = 0;
        foreach (var bloc in blocsCheckpoints) { if (bloc.gameObject.activeSelf && PosToBlock(bloc.gameObject) != null) nb++; }
        niveauJSON.checkpoint = new Bloc[nb];

        foreach (var bloc in blocsCheckpoints) {
            if (bloc.gameObject.activeSelf) {
                blocJSON = PosToBlock(bloc.gameObject);
                if (blocJSON != null) niveauJSON.checkpoint[i++] = blocJSON;
            }
        }

        // Collectibles

        nb = 0; i = 0;
        foreach (var bloc in blocsCollectibles) { if (bloc.gameObject.activeSelf && PosToBlock(bloc.gameObject) != null) nb++; }
        niveauJSON.collectible = new Bloc[nb];

        foreach (var bloc in blocsCollectibles) {
            if (bloc.gameObject.activeSelf) {
                blocJSON = PosToBlock(bloc.gameObject);
                if (blocJSON != null) niveauJSON.collectible[i++] = blocJSON;
            }
        }

        // Ralentis

        nb = 0; i = 0;
        foreach (var bloc in blocsRalenti) { if (bloc.gameObject.activeSelf && PosToBlock(bloc.gameObject) != null) nb++; }
        niveauJSON.ralenti = new BlocRalenti[nb];

        foreach (var bloc in blocsRalenti) {
            if (bloc.gameObject.activeSelf) {
                blocJSON = PosToBlock(bloc.gameObject);
                if (blocJSON != null) {
                    BlocRalenti blocRalentiJSON = new BlocRalenti();
                    blocRalentiJSON.x = blocJSON.x;
                    blocRalentiJSON.y = blocJSON.y;
                    blocRalentiJSON.duree = bloc.GetDuree();
                    niveauJSON.ralenti[i++] = blocRalentiJSON;
                }
            }
        }


        // Tourelles

        nb = 0; i = 0;
        foreach (var bloc in blocsTurrets) { if (bloc.gameObject.activeSelf && PosToBlock(bloc.gameObject) != null) nb++; }
        niveauJSON.turret = new BlocTourelle[nb];

        foreach (var bloc in blocsTurrets) {
            if (bloc.gameObject.activeSelf) {
                blocJSON = PosToBlock(bloc.gameObject);
                if (blocJSON != null) {
                    BlocTourelle blocTourelleJSON = new BlocTourelle();
                    blocTourelleJSON.x = blocJSON.x;
                    blocTourelleJSON.y = blocJSON.y;
                    Tourelle_ShootSpeed tss = bloc.GetComponentInChildren<Tourelle_ShootSpeed>();
                    blocTourelleJSON.ballsByShot = tss.GetBallsByShot();
                    blocTourelleJSON.waitBalls = tss.GetWaitBetweenBalls();
                    blocTourelleJSON.waitShoot = tss.GetWaitBetweenShoot();
                    niveauJSON.turret[i++] = blocTourelleJSON;
                }
            }
        }




        // Conversion en JSON

        json = JsonUtility.ToJson(niveauJSON);

        return json;

    }

    // Use this for initialization
    void Start() {

    }

        // Update is called once per frame
    void Update () {
	
	}
}
