# Pattern-avoiders-detection
Implementation of the algorithm https://arxiv.org/abs/1509.08216 for finding pattern avoiders and counting pattern occurrences in permutations.

## Format vstupu:
`C` `n` `threds` `Subor`

`C` - typ vypoctup bud `PPAA` - uchovavnie i podoby permutacii alebo `PPAP`- pocita len pocet avoiderov.
`n` - maximalna dlzka hladanych permutacii
`threads` - pocet vlakien pouzitych na vypocet
`Subor` - meno subor odkial sa maju brat vstupne data.

## Format vstupneho suboru:
Na kazdom riadku je len jedna permutacia, kde napr. permutacia `312` je zapisana v tvare:
`2-0-1`

## Funkcie jednotlivych projektov:

### `ArrayExtensions`:
Obsahuje jeden súbor, ktorý obsahuje statickú triedu, 
ktorá obsahuje extension metódy pre pole `T[]`, kde `T` je genericky parameter.

### `BoolExtensions`:
Analogicky obsahuje rozširujúce metódy pre typ `bool`.

### `ByteExtensions`:
Analogicky obsahuje rozširujúce metódy pre typ `byte`.

### `CommandAccept`:
Príjma vstup po tom čo ho spracuje a príjme `InputHandler` z projektu `InputHandling`.
Rozhoduje o tom čo sa budu počítatať. Presúva dáta CommandHandler-u, konkretne `CommandHandlerPatternAvoidancePPA` z
projektu `CommandHandlers`. Teda je to medzivrstva medzi triedami z `InputHandler` a `CommandHandler`.

### `CommandHandlers`:
Spracovava vstup, ktory dostane od triedy z projektu`CommandAccept`. Napr. nacita permutacie zo suboru a ulozi ich. Vytvori
objekt pre ukladanie spocitanych dat (typu `ResultPPA`) a spusti samotne pocitanie PPA.
 
### `ExtensionMaps`:
V danom projekte su triedy, interface, ktore sluzia na vytvaranie a reprezentovanie `extension map` popisanych v clanku.
Na internu reprezentaciu pouziva triedy z projektu `NumericalSequences`.

### `ExtensionMapsComputation`:
`ExMapComputationUnsorted` sluzi na spocitanie `extension map` novej permutacie z `extension maps` inych permutacii, tak
ako je to popisane v clanku.
`MinimumLettersChecked` sluzi na spocitanie v nejakom zmysle minimalneho poctu extension map, ktore musia byt vzane 
do uvahy pri pocitani extension map novej premutacie.

### `GeneralInterfaces`:
Obsahuje interface, ktoere su pouzivane v celom solution-e, nepatraja k implementovanym interfac-om a su pomerne obecne.

### `InputHandling`:
Jeho triedy sluzia na prvotnu interakciu so vstupnymi argumentami. Skontroluju ich spravnost, rozumne ich ulozia a odovzdaju ich instancii triedy z projektu `CommandAccept`, ked to bude potrebne.

### `IntExtensions`:
Obsahuje rozsirujuce metody pre typ `int`.

### `LogicInterfaces`:
Obsahuje interfac-y, ktore maju "logicku povahu".

### `NumberOperationsDefaults`:
Obsahuje triedu, ktora vracia triedy, ktore pocituju popcount a ctz na postupnosti cisel typu `ulong`, tj. na
postupnostiach 64 bitovych cisel. Kde ctz vracia postupne pocet 0 pred 1. 1, 2. 1,... Kde pocet 0 pred i. 1 je "realny"
pocet 0 pred i. 1 a este aj pocet 1 pred i. 1.

### `NumberOperationsImplementations`:
Obsahuje implementacie interfac-ov, ktore podporuje pocitatnie popcount a ctz, tak ako je to popisane v predchadzajucom
odseku.

### `NumberOperationsInterfaces`:
Obsahuje interfac-y, ktore su implementovane triedami v projekte `NumberOperationsImplementations`.

### `NumericalSequences`:
Obsahuje triedy, ktore sluzia na reprezentovanie postupnosti cisel (postupnosti pismen, kde pismena su cisla), zaroven
na nich podporuje robit rozne operacie. Zaroven su pritomne interfac-y a ich implementacie, ktore sluzia na vytvaranie danych
tried.

### `NumericalSequencesConstruction`:
Obsahuje dalsie interfac-y a ich implementacie, ktore sluzia na vytvorenie tried z projektu `NumericalSequences`.

### `PatternAvoidersPPAComputation`:
Obsahuje interfac-y a ich implementacie, ktore pocitaju PPA. Su tam triedy, ktore zabezpecuju podporu i palarelneho
vypoctu a vypoctov, kedy chceme len počet avoider-ov danej velkosti a nie to ako vyzeraju.

### `PatternNode`:
Obsahuje interfac-y a ich implementacie, ktore reprezentuju pomyselny uzol pri vypocte PPA, kde dany uzol obsahuje permutaciu,
jej inverziu, resp. cast jej inverzie, jej `extension map` atd. Spominane data ma ulozene v instancii triedy z projektu
`PermutationContainers`.

### `Patterns`:
Obsahuje triedy a interfac-y, ktore sluzia na reprezentaciu a vytvaranie vzorov. Kde jedna z tried sluzi presne 
na reprezentaciu permutacii, dalsia zase na reprezentaciu "ciastocnych inverznych funkcii". 
Taketo permutacie a "ciastocne inverzne funkcie" su potom zabalene napr. v instancii 
triedy z projektu `PermutationContainers`.
Na internu reprezentaciu pouziva triedy z projektu `NumericalSequences`.


### `PermutationContainers`:
Obsahuje triedy, ktore zabaluju permutaciu, jej "ciastocnu inverznu funkciu" a napr. jej `extension map`.
Zabezpecuje napr. vytvaranie novych permutacii z permutacie, ktoru obsahuje na zaklade jej `extension map`. Teda ze vytvori
nove permutacie vlozenim noveho najvyssieho cisla na pozicie kam mu to dovoluje `extension map`. 
Nasledne spocita "ciastocnu inverznu funckciu", `extension map` novej permutacie a zabali to do novej instancie triedy
z `PermutationContainers`.

### `PermutationPatterns`:
Spusta samotny beh programu. Vytvori potrebne triedy pre nacitanie dat a spustenie vypoctu.

### `PermutationCollections`:
Obsahuje implementacie kolekcii na uchovanie permutacii.

### `PermutationSuccessorsComputation`:
Pocita "naslednikov" danej permutacie podla jej `extension map`.

### `Result`:
Obsahuje triedy na reprezentovanie ulozenych dat z vypoctu PPA.

### `ResultWriter`:
Obsahuje triedu implementujucu kod na vypisovanie ulozenych dat z tried v projekte `Result`.

## Dodatocne komentare:

### Teda priebeh vypoctu je takyto:
`PermutationPatterns` -> `InputHandling` -> `CommandAccept` -> 
`CommandHandlers` -> `PatternAvoidersPPAComputation` -> `ResultWriter`

`ExtensionMaps`, `Patterns` interne vyuzivaju `NumericalSequences`.
`PermutationContainers` zabaluje permutaciu, "jej ciastocnu inverziu", `extension map`.
`PatternNode` zabaluje `PermutationContainers`.


Pod "ciastocna inverzna funkcia" je mysleny taky vzor
z projektu `Patterns`, ktory uchovava pozicie najvyssich k
pismen (cisel) z nejakej, z kontextu jasnej, permutacie.

