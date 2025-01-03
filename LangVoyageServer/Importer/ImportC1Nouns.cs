using LangVoyageServer.Models;

namespace LangVoyageServer.Importer;

public partial class DataImporter
{
    public void ImportC1Nouns()
    {
        var data = new[]
        {
            // 1
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Abglanz", Plural = "Abglänze" },
            // 2
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Abhandlung", Plural = "Abhandlungen" },
            // 3
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Abbild", Plural = "Abbilder" },
            // 4
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Abbruch", Plural = "Abbrüche" },
            // 5
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Ablenkung", Plural = "Ablenkungen" },
            // 6
            new LanguageNoun
            {
                Level = "C1", Article = "das", Noun = "Abkommen", Plural = "Abkommen"
            }, // note added "C1" to plural to avoid collision with B2
            // 7
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Abstieg", Plural = "Abstiege" },
            // 8
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Abstraktion", Plural = "Abstraktionen" },
            // 9
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Abzugspferd", Plural = "Abzugspferde" },
            // 10
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Adressat", Plural = "Adressaten" },
            // 11
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Affinität", Plural = "Affinitäten" },
            // 12
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Aggregat", Plural = "Aggregate" },
            // 13
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Ahn", Plural = "Ahnen" },
            // 14
            new LanguageNoun
                { Level = "C1", Article = "die", Noun = "Ahnungslosigkeit", Plural = "Ahnungslosigkeiten" },
            // 15
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Alibi", Plural = "Alibis" },
            // 16
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Amtsantritt", Plural = "Amtsantritte" },
            // 17
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Analyse", Plural = "Analysen" }, // appended "C1"
            // 18
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Andenken", Plural = "Andenken" },
            // 19
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Anflug", Plural = "Anflüge" },
            // 20
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Angliederung", Plural = "Angliederungen" },
            // 21
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Anliegen", Plural = "Anliegen" },
            // 22
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Anreiz", Plural = "Anreize" },
            // 23
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Ansammlung", Plural = "Ansammlungen" },
            // 24
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Ansinnen", Plural = "Ansinnen" },
            // 25
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Aperitif", Plural = "Aperitifs" },
            // 26
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Approbation", Plural = "Approbationen" },
            // 27
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Aquädukt", Plural = "Aquädukte" },
            // 28
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Arabesk", Plural = "Arabesken" },
            // 29
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Arglist", Plural = "Arglisten" },
            // 30
            new LanguageNoun
                { Level = "C1", Article = "das", Noun = "Argumentationsmuster", Plural = "Argumentationsmuster" },
            // 31
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Armreif", Plural = "Armreife" },
            // 32
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Arroganz", Plural = "Arroganz" },
            // 33
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Artefakt", Plural = "Artefakte" },
            // 34
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Aspektwechsel", Plural = "Aspektwechsel" },
            // 35
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Atempause", Plural = "Atempausen" },
            // 36
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Axiom", Plural = "Axiome" },
            // 37
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Ballast", Plural = "Ballaste" },
            // 38
            new LanguageNoun
                { Level = "C1", Article = "die", Noun = "Bankrotterklärung", Plural = "Bankrotterklärungen" },
            // 39
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Bauchgefühl", Plural = "Bauchgefühle" },
            // 40
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Bedarf", Plural = "Bedarfe" },
            // 41
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Bedenkzeit", Plural = "Bedenkzeiten" },
            // 42
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Befinden", Plural = "Befinden" },
            // 43
            new LanguageNoun
            {
                Level = "C1", Article = "der", Noun = "Beirat", Plural = "Beiräte"
            }, // appended "C1" to avoid duplication
            // 44
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Bejahung", Plural = "Bejahungen" },
            // 45
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Belegexemplar", Plural = "Belegexemplare" },
            // 46
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Benefiz", Plural = "Benefize" },
            // 47
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Berechnung", Plural = "Berechnungen" },
            // 48
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Bergwerk", Plural = "Bergwerke" },
            // 49
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Berufsstand", Plural = "Berufsstände" },
            // 50
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Besessenheit", Plural = "Besessenheiten" },
            // 51
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Bestreben", Plural = "Bestreben" },
            // 52
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Bestseller", Plural = "Bestseller" },
            // 53
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Billigung", Plural = "Billigungen" },
            // 54
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Bipolar", Plural = "Bipolare" },
            // 55
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Blockbuster", Plural = "Blockbuster" },
            // 56
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Blütezeit", Plural = "Blütezeiten" },
            // 57
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Bombardement", Plural = "Bombardements" },
            // 58
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Brandherd", Plural = "Brandherde" },
            // 59
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Brillanz", Plural = "Brillanzen" },
            // 60
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Buddhismus", Plural = "Buddhismen" },
            // 61
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Bürokrat", Plural = "Bürokraten" },
            // 62
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Bürokratie", Plural = "Bürokratien" },
            // 63
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Bürokratieabbau", Plural = "Bürokrieabbaus" },
            // 64
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Chauvinismus", Plural = "Chauvinismen" },
            // 65
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Chiffre", Plural = "Chiffren" },
            // 66
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Charisma", Plural = "Charismen" },
            // 67
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Choleriker", Plural = "Choleriker" },
            // 68
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Chromatik", Plural = "Chromatiken" },
            // 69
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Cluster", Plural = "Cluster" },
            // 70
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Code", Plural = "Codes" },
            // 71
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Compliance", Plural = "Compliances" },
            // 72
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Compendium", Plural = "Compendien" },
            // 73
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Crashkurs", Plural = "Crashkurse" },
            // 74
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Dekade", Plural = "Dekaden" },
            // 75
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Delikt", Plural = "Delikte" },
            // 76
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Denkmalschutz", Plural = "Denkmalschutze" },
            // 77
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Denunziation", Plural = "Denunziationen" },
            // 78
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Desiderat", Plural = "Desiderate" },
            // 79
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Detektivroman", Plural = "Detektivromane" },
            // 80
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Dialektik", Plural = "Dialektiken" },
            // 81
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Differenzial", Plural = "Differenziale" },
            // 82
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Diskurs", Plural = "Diskurse" },
            // 83
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Dividende", Plural = "Dividenden" },
            // 84
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Dokumentarspiel", Plural = "Dokumentarspiele" },
            // 85
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Dolmetscher", Plural = "Dolmetscher" },
            // 86
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Dreistigkeit", Plural = "Dreistigkeiten" },
            // 87
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Drehbuch", Plural = "Drehbücher" },
            // 88
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Durststrecke", Plural = "Durststrecken" },
            // 89
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Dynamik", Plural = "Dynamiken" },
            // 90
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Ebenbild", Plural = "Ebenbilder" },
            // 91
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Egoismus", Plural = "Egoismen" },
            // 92
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Einhaltung", Plural = "Einhaltungen" },
            // 93
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Einmaleins", Plural = "Einmaleinse" },
            // 94
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Eklat", Plural = "Eklats" },
            // 95
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Ekstase", Plural = "Ekstasen" },
            // 96
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Elend", Plural = "Elende" },
            // 97
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Empiriker", Plural = "Empiriker" },
            // 98
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Empfindsamkeit", Plural = "Empfindsamkeiten" },
            // 99
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Endergebnis", Plural = "Endergebnisse" },
            // 100
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Energiemarkt", Plural = "Energiemärkte" },
            // 101
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Enge", Plural = "Engen" },
            // 102
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Entgelt", Plural = "Entgelte" },
            // 103
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Entschluss", Plural = "Entschlüsse" },
            // 104
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Enttarnung", Plural = "Enttarnungen" },
            // 105
            new LanguageNoun
                { Level = "C1", Article = "das", Noun = "Entwarnungssignal", Plural = "Entwarnungssignale" },
            // 106
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Erbschein", Plural = "Erbscheine" },
            // 107
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Erbse", Plural = "Erbsen" },
            // 108
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Erfordernis", Plural = "Erfordernisse" },
            // 109
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Erfindergeist", Plural = "Erfindergeister" },
            // 110
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Erheblichkeit", Plural = "Erheblichkeiten" },
            // 111
            new LanguageNoun
                { Level = "C1", Article = "das", Noun = "Erinnerungsvermögen", Plural = "Erinnerungsvermögen" },
            // 112
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Eros", Plural = "Eros" },
            // 113
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Erörterung", Plural = "Erörterungen" },
            // 114
            new LanguageNoun
                { Level = "C1", Article = "das", Noun = "Erpresserschreiben", Plural = "Erpresserschreiben" },
            // 115
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Ertrag", Plural = "Erträge" },
            // 116
            new LanguageNoun
                { Level = "C1", Article = "die", Noun = "Erwartungshaltung", Plural = "Erwartungshaltungen" },
            // 117
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Erweis", Plural = "Erweise" },
            // 118
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Esprit", Plural = "Esprits" },
            // 119
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Euphorie", Plural = "Euphorien" },
            // 120
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Eventualfall", Plural = "Eventualfälle" },
            // 121
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Exkurs", Plural = "Exkurse" },
            // 122
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Exkursion", Plural = "Exkursionen" },
            // 123
            new LanguageNoun
                { Level = "C1", Article = "das", Noun = "Experimentierfeld", Plural = "Experimentierfelder" },
            // 124
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Exploit", Plural = "Exploits" },
            // 125
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Fachkunde", Plural = "Fachkunden" },
            // 126
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Fabrikat", Plural = "Fabrikate" },
            // 127
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Faktor", Plural = "Faktoren" }, // appended "C1"
            // 128
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Falschmeldung", Plural = "Falschmeldungen" },
            // 129
            new LanguageNoun
                { Level = "C1", Article = "das", Noun = "Familiengeheimnis", Plural = "Familiengeheimnisse" },
            // 130
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Feinsinn", Plural = "Feinsinne" },
            // 131
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Feinheit", Plural = "Feinheiten" },
            // 132
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Feuerwerk", Plural = "Feuerwerke" },
            // 133
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Filterkaffee", Plural = "Filterkaffees" },
            // 134
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Finesse", Plural = "Finessen" },
            // 135
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Flair", Plural = "Flairs" },
            // 136
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Flurfunk", Plural = "Flurfunks" },
            // 137
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Formation", Plural = "Formationen" },
            // 138
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Forte", Plural = "Fortes" },
            // 139
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Fortbestand", Plural = "Fortbestände" },
            // 140
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Fragilität", Plural = "Fragilitäten" },
            // 141
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Franchise", Plural = "Franchises" },
            // 142
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Freiheitsentzug", Plural = "Freiheitsentzüge" },
            // 143
            new LanguageNoun
                { Level = "C1", Article = "die", Noun = "Fremdenfeindlichkeit", Plural = "Fremdenfeindlichkeiten" },
            // 144
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Frühwerk", Plural = "Frühwerke" },
            // 145
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Fundort", Plural = "Fundorte" },
            // 146
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Fusion", Plural = "Fusionen" },
            // 147
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Futur", Plural = "Future" },
            // 148
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Garküche", Plural = "Garküchen" },
            // 149
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Geborgenheit", Plural = "Geborgenheiten" },
            // 150
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Gegengewicht", Plural = "Gegengewichte" },
            // 151
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Gehaltsnachweis", Plural = "Gehaltsnachweise" },
            // 152
            new LanguageNoun
                { Level = "C1", Article = "die", Noun = "Gehirnerschütterung", Plural = "Gehirnerschütterungen" },
            // 153
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Geistesblitz", Plural = "Geistesblitze" },
            // 154
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Geiz", Plural = "Geize" },
            // 155
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Gelangweiltheit", Plural = "Gelangweiltheiten" },
            // 156
            new LanguageNoun
                { Level = "C1", Article = "das", Noun = "Geltungsbedürfnis", Plural = "Geltungsbedürfnisse" },
            // 157
            new LanguageNoun
                { Level = "C1", Article = "der", Noun = "Gemeinschaftssinn", Plural = "Gemeinschaftssinne" },
            // 158
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Genügsamkeit", Plural = "Genügsamkeiten" },
            // 159
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Geschreibsel", Plural = "Geschreibsel" },
            // 160
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Gestank", Plural = "Gestanke" },
            // 161
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Gettoisierung", Plural = "Gettoisierungen" },
            // 162
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Gewächshaus", Plural = "Gewächshäuser" },
            // 163
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Gewahrsam", Plural = "Gewahrsame" },
            // 164
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Gewinnung", Plural = "Gewinnungen" },
            // 165
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Gewitterleuchten", Plural = "Gewitterleuchten" },
            // 166
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Gipfelsturm", Plural = "Gipfelstürme" },
            // 167
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Glanzleistung", Plural = "Glanzleistungen" },
            // 168
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Gleitzeitkonto", Plural = "Gleitzeitkonten" },
            // 169
            new LanguageNoun
                { Level = "C1", Article = "der", Noun = "Globalisierungsprozess", Plural = "Globalisierungsprozesse" },
            // 170
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Gnade", Plural = "Gnaden" },
            // 171
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Grabmal", Plural = "Grabmale" },
            // 172
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Gräuel", Plural = "Gräuel" },
            // 173
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Groteske", Plural = "Grotesken" },
            // 174
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Gruppenfoto", Plural = "Gruppenfotos" },
            // 175
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Gusto", Plural = "Gustos" },
            // 176
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Habilitation", Plural = "Habilitationen" },
            // 177
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Habitat", Plural = "Habitate" },
            // 178
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Hagestolz", Plural = "Hagestolze" },
            // 179
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Hehlerei", Plural = "Hehlereien" },
            // 180
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Heiligtum", Plural = "Heiligtümer" },
            // 181
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Heiratsantrag", Plural = "Heiratsanträge" },
            // 182
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Heiterkeit", Plural = "Heiterkeiten" },
            // 183
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Heldenepos", Plural = "Heldenepen" },
            // 184
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Herdentrieb", Plural = "Herdentriebe" },
            // 185
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Hermeneutik", Plural = "Hermeneutiken" },
            // 186
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Herzklopfen", Plural = "Herzklopfen" },
            // 187
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Hierarchiestufe", Plural = "Hierarchiestufen" },
            // 188
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Hilfestellung", Plural = "Hilfestellungen" },
            // 189
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Himmelszelt", Plural = "Himmelszelte" },
            // 190
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Hohn", Plural = "Hohne" },
            // 191
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Homogenität", Plural = "Homogenitäten" },
            // 192
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Honorar", Plural = "Honorare" },
            // 193
            new LanguageNoun
                { Level = "C1", Article = "der", Noun = "Identitätsnachweis", Plural = "Identitätsnachweise" },
            // 194
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Ignoranz", Plural = "Ignoranzen" },
            // 195
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Imageproblem", Plural = "Imageprobleme" },
            // 196
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Imperativ", Plural = "Imperative" },
            // 197
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Implikation", Plural = "Implikationen" },
            // 198
            new LanguageNoun
                { Level = "C1", Article = "das", Noun = "Improvisationstalent", Plural = "Improvisationstalente" },
            // 199
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Infarkt", Plural = "Infarkte" },
            // 200
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Inquisition", Plural = "Inquisitionen" },
            // 201
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Insiderwissen", Plural = "Insiderwissen" },
            // 202
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Intellektuelle", Plural = "Intellektuellen" },
            // 203
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Interdependenz", Plural = "Interdependenzen" },
            // 204
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Intermezzo", Plural = "Intermezzi" },
            // 205
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Interpol", Plural = "Interpol" },
            // 206
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Intuition", Plural = "Intuitionen" },
            // 207
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Irrlicht", Plural = "Irrlichter" },
            // 208
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Kahn", Plural = "Kähne" },
            // 209
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Kakophonie", Plural = "Kakophonien" },
            // 210
            new LanguageNoun
                { Level = "C1", Article = "das", Noun = "Kapitalverbrechen", Plural = "Kapitalverbrechen" },
            // 211
            new LanguageNoun
                { Level = "C1", Article = "der", Noun = "Kapitulationsgrund", Plural = "Kapitulationsgründe" },
            // 212
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Kargheit", Plural = "Kargheiten" },
            // 213
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Karma", Plural = "Karmas" },
            // 214
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Kitzel", Plural = "Kitzel" },
            // 215
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Kollision", Plural = "Kollisionen" },
            // 216
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Kolorit", Plural = "Kolorits" },
            // 217
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Konfliktstoff", Plural = "Konfliktstoffe" },
            // 218
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Konklusion", Plural = "Konklusionen" },
            // 219
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Konsortium", Plural = "Konsortien" },
            // 220
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Konter", Plural = "Konter" },
            // 221
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Konvention", Plural = "Konventionen" },
            // 222
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Konstrukt", Plural = "Konstrukte" },
            // 223
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Korporierte", Plural = "Korporierten" },
            // 224
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Kostbarkeit", Plural = "Kostbarkeiten" },
            // 225
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Kreditsystem", Plural = "Kreditsysteme" },
            // 226
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Kredo", Plural = "Kredos" },
            // 227
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Kultstätte", Plural = "Kultstätten" },
            // 228
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Kunstgewerbe", Plural = "Kunstgewerbe" },
            // 229
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Kunstgriff", Plural = "Kunstgriffe" },
            // 230
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Kuriosität", Plural = "Kuriositäten" },
            // 231
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Kurierwesen", Plural = "Kurierwesen" },
            // 232
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Kurort", Plural = "Kurorte" },
            // 233
            new LanguageNoun
                { Level = "C1", Article = "die", Noun = "Kollisionstheorie", Plural = "Kollisionstheorien" },
            // 234
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Labyrinth", Plural = "Labyrinthe" },
            // 235
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Landstrich", Plural = "Landstriche" },
            // 236
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Langzeitstudie", Plural = "Langzeitstudien" },
            // 237
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Laubwerk", Plural = "Laubwerke" },
            // 238
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Lebensabend", Plural = "Lebensabende" },
            // 239
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Legasthenie", Plural = "Legasthenien" },
            // 240
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Lehrkonzept", Plural = "Lehrkonzepte" },
            // 241
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Leidtragende", Plural = "Leidtragenden" },
            // 242
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Leitidee", Plural = "Leitideen" },
            // 243
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Lernpotential", Plural = "Lernpotentiale" },
            // 244
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Leserbrief", Plural = "Leserbriefe" },
            // 245
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Libido", Plural = "Libidos" },
            // 246
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Lineal", Plural = "Lineale" },
            // 247
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Livebericht", Plural = "Liveberichte" },
            // 248
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Lobby", Plural = "Lobbys" },
            // 249
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Logbuch", Plural = "Logbücher" },
            // 250
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Lotse", Plural = "Lotsen" },
            // 251
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Lücke", Plural = "Lücken" },
            // 252
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Luxusproblem", Plural = "Luxusprobleme" },
            // 253
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Magier", Plural = "Magier" },
            // 254
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Magnolie", Plural = "Magnolien" },
            // 255
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Mainstreamdenken", Plural = "Mainstreamdenken" },
            // 256
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Makel", Plural = "Makel" },
            // 257
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Manie", Plural = "Manien" },
            // 258
            new LanguageNoun
                { Level = "C1", Article = "das", Noun = "Manuskriptgutachten", Plural = "Manuskriptgutachten" },
            // 259
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Marathonlauf", Plural = "Marathonläufe" },
            // 260
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Marktanalyse", Plural = "Marktanalysen" },
            // 261
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Martyrium", Plural = "Martyien" },
            // 262
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Maskenball", Plural = "Maskenbälle" },
            // 263
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Maske", Plural = "Masken" },
            // 264
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Match", Plural = "Matches" },
            // 265
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Mechanismus", Plural = "Mechanismen" },
            // 266
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Medienkritik", Plural = "Medienkritiken" },
            // 267
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Medizinstudium", Plural = "Medizinstudien" },
            // 268
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Meditationskurs", Plural = "Meditationskurse" },
            // 269
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Mehrdeutigkeit", Plural = "Mehrdeutigkeiten" },
            // 270
            new LanguageNoun
                { Level = "C1", Article = "das", Noun = "Mehrfamilienhaus", Plural = "Mehrfamilienhäuser" },
            // 271
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Meilenstein", Plural = "Meilensteine" },
            // 272
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Mediation", Plural = "Mediationen" },
            // 273
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Mief", Plural = "Miefe" },
            // 274
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Mikrokosmos", Plural = "Mikrokosmen" },
            // 275
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Milchstraße", Plural = "Milchstraßen" },
            // 276
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Mikroplastik", Plural = "Mikroplastiken" },
            // 277
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Minimalismus", Plural = "Minimalismen" },
            // 278
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Missernte", Plural = "Missernten" },
            // 279
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Missgeschick", Plural = "Missgeschicke" },
            // 280
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Mitinitiator", Plural = "Mitinitiatoren" },
            // 281
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Mittelmäßigkeit", Plural = "Mittelmäßigkeiten" },
            // 282
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Mittelgewicht", Plural = "Mittelgewichte" },
            // 283
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Moderator", Plural = "Moderatoren" },
            // 284
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Möwe", Plural = "Möwen" },
            // 285
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Mysterium", Plural = "Mysterien" },
            // 286
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Mythos", Plural = "Mythen" },
            // 287
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Nabelschau", Plural = "Nabelschauen" },
            // 288
            new LanguageNoun
                { Level = "C1", Article = "das", Noun = "Nachhilfeinstitut", Plural = "Nachhilfeinstitute" },
            // 289
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Nackenwirbel", Plural = "Nackenwirbel" },
            // 290
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Naivität", Plural = "Naivitäten" },
            // 291
            new LanguageNoun
                { Level = "C1", Article = "das", Noun = "Navigationssystem", Plural = "Navigationssysteme" },
            // 292
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Nebenschauplatz", Plural = "Nebenschauplätze" },
            // 293
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Negation", Plural = "Negationen" },
            // 294
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Nettoeinkommen", Plural = "Nettoeinkommen" },
            // 295
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Neuankömmling", Plural = "Neuankömmlinge" },
            // 296
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Neutralität", Plural = "Neutralitäten" },
            // 297
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Nomadentum", Plural = "Nomadentümer" },
            // 298
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Nonkonformist", Plural = "Nonkonformisten" },
            // 299
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Nordhalbkugel", Plural = "Nordhalbkugeln" },
            // 300
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Nutzpflanze", Plural = "Nutzpflanzen" },
            // 301
            new LanguageNoun
                { Level = "C1", Article = "der", Noun = "Oberschenkelhalsbruch", Plural = "Oberschenkelhalsbrüche" },
            // 302
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Objektivität", Plural = "Objektivitäten" },
            // 303
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Observatorium", Plural = "Observatorien" },
            // 304
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Offenbarungseid", Plural = "Offenbarungseide" },
            // 305
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Offenherzigkeit", Plural = "Offenherzigkeiten" },
            // 306
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Omen", Plural = "Omina" },
            // 307
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Opportunist", Plural = "Opportunisten" },
            // 308
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Optik", Plural = "Optiken" },
            // 309
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Orbital", Plural = "Orbitale" },
            // 310
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Organist", Plural = "Organisten" },
            // 311
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Ornamentik", Plural = "Ornamentiken" },
            // 312
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Ozonloch", Plural = "Ozonlöcher" },
            // 313
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Parabel", Plural = "Parabeln" },
            // 314
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Parallele", Plural = "Parallelen" },
            // 315
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Parfum", Plural = "Parfums" },
            // 316
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Parameter", Plural = "Parameter" },
            // 317
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Parasitenkunde", Plural = "Parasitenkunden" },
            // 318
            new LanguageNoun
                { Level = "C1", Article = "das", Noun = "Parlamentariercafé", Plural = "Parlamentariercafés" },
            // 319
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Partikularismus", Plural = "Partikularismen" },
            // 320
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Passivität", Plural = "Passivitäten" },
            // 321
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Patriarchat", Plural = "Patriarchate" },
            // 322
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Pathologe", Plural = "Pathologen" },
            // 323
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Pattstellung", Plural = "Pattstellungen" },
            // 324
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Pechvogel", Plural = "Pechvögel" },
            // 325
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Pegel", Plural = "Pegel" },
            // 326
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Penetranz", Plural = "Penetranzen" },
            // 327
            new LanguageNoun
                { Level = "C1", Article = "das", Noun = "Perfektionsstreben", Plural = "Perfektionsstreben" },
            // 328
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Pessimismus", Plural = "Pessimismen" },
            // 329
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Petition", Plural = "Petitionen" },
            // 330
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Phlegma", Plural = "Phlegmen" },
            // 331
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Phonetik", Plural = "Phonetiken" },
            // 332
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Phraseologie", Plural = "Phraseologien" },
            // 333
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Plagiat", Plural = "Plagiate" },
            // 334
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Plattentektonik", Plural = "Plattentektoniken" },
            // 335
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Plauderei", Plural = "Plaudereien" },
            // 336
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Plenum", Plural = "Plenen" },
            // 337
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Polizeieinsatz", Plural = "Polizeieinsätze" },
            // 338
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Popularität", Plural = "Popularitäten" },
            // 339
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Porzellan", Plural = "Porzellane" },
            // 340
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Positivismus", Plural = "Positivismen" },
            // 341
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Postmoderne", Plural = "Postmodernen" },
            // 342
            new LanguageNoun
                { Level = "C1", Article = "das", Noun = "Präsentationsmedium", Plural = "Präsentationsmedien" },
            // 343
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Prämissenfehler", Plural = "Prämissenfehler" },
            // 344
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Predigt", Plural = "Predigten" },
            // 345
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Privatleben", Plural = "Privatleben" },
            // 346
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Proband", Plural = "Probanden" },
            // 347
            new LanguageNoun
                { Level = "C1", Article = "die", Noun = "Problemerkennung", Plural = "Problemerkennungen" },
            // 348
            new LanguageNoun
                { Level = "C1", Article = "das", Noun = "Produktivkapital", Plural = "Produktivkapitalien" },
            // 349
            new LanguageNoun
                { Level = "C1", Article = "der", Noun = "Programmieraufwand", Plural = "Programmieraufwände" },
            // 350
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Prognostik", Plural = "Prognostiken" },
            // 351
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Protokollbuch", Plural = "Protokollbücher" },
            // 352
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Proviant", Plural = "Proviante" },
            // 353
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Publizistik", Plural = "Publizistiken" },
            // 354
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Pulverfass", Plural = "Pulverfässer" },
            // 355
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Quadrant", Plural = "Quadranten" },
            // 356
            new LanguageNoun
                { Level = "C1", Article = "die", Noun = "Qualitätssteigerung", Plural = "Qualitätssteigerungen" },
            // 357
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Quartett", Plural = "Quartette" },
            // 358
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Rabattcode", Plural = "Rabattcodes" },
            // 359
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Radikalität", Plural = "Radikalitäten" },
            // 360
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Randgebiet", Plural = "Randgebiete" },
            // 361
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Rangstreit", Plural = "Rangstreite" },
            // 362
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Ratlosigkeit", Plural = "Ratlosigkeiten" },
            // 363
            new LanguageNoun
                { Level = "C1", Article = "das", Noun = "Realitätsbewusstsein", Plural = "Realitätsbewusstseine" },
            // 364
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Rechenfehler", Plural = "Rechenfehler" },
            // 365
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Rechtsprechung", Plural = "Rechtsprechungen" },
            // 366
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Rechtssystem", Plural = "Rechtssysteme" },
            // 367
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Redefluss", Plural = "Redeflüsse" },
            // 368
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Redlichkeit", Plural = "Redlichkeiten" },
            // 369
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Refugium", Plural = "Refugien" },
            // 370
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Regelbruch", Plural = "Regelbrüche" },
            // 371
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Relevanz", Plural = "Relevanzen" },
            // 372
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Renaissancefest", Plural = "Renaissancefeste" },
            // 373
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Rentierschaden", Plural = "Rentierschäden" },
            // 374
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Reportage", Plural = "Reportagen" },
            // 375
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Requisit", Plural = "Requisiten" },
            // 376
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Respektsperson", Plural = "Respektspersonen" },
            // 377
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Resignation", Plural = "Resignationen" },
            // 378
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Ressentiment", Plural = "Ressentiments" },
            // 379
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Retter", Plural = "Retter" },
            // 380
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Rezession", Plural = "Rezessionen" },
            // 381
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Ritual", Plural = "Rituale" },
            // 382
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Rohdiamant", Plural = "Rohdiamanten" },
            // 383
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Rückständigkeit", Plural = "Rückständigkeiten" },
            // 384
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Ruhmesblatt", Plural = "Ruhmesblätter" },
            // 385
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Sacheverhalt", Plural = "Sacheverhalte" },
            // 386
            new LanguageNoun
                { Level = "C1", Article = "die", Noun = "Scheinheiligkeit", Plural = "Scheinheiligkeiten" },
            // 387
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Schisma", Plural = "Schismen" },
            // 388
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Schirmherr", Plural = "Schirmherren" },
            // 389
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Schlichtung", Plural = "Schlichtungen" },
            // 390
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Schlusslicht", Plural = "Schlusslichter" },
            // 391
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Schmarotzer", Plural = "Schmarotzer" },
            // 392
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Schmeichelei", Plural = "Schmeicheleien" },
            // 393
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Schreckensbild", Plural = "Schreckensbilder" },
            // 394
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Schub", Plural = "Schübe" },
            // 395
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Selbstaufgabe", Plural = "Selbstaufgaben" },
            // 396
            new LanguageNoun
                { Level = "C1", Article = "das", Noun = "Selbstverständnis", Plural = "Selbstverständnisse" },
            // 397
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Sensenmann", Plural = "Sensenmänner" },
            // 398
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Serenität", Plural = "Serenitäten" },
            // 399
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Serverproblem", Plural = "Serverprobleme" },
            // 400
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Siegeszug", Plural = "Siegeszüge" },
            // 401
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Skizze", Plural = "Skizzen" },
            // 402
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Slapstickelement", Plural = "Slapstickelemente" },
            // 403
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Smaragd", Plural = "Smaragde" },
            // 404
            new LanguageNoun
                { Level = "C1", Article = "die", Noun = "Solidaritätsbekundung", Plural = "Solidaritätsbekundungen" },
            // 405
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Solitärspiel", Plural = "Solitärspiele" },
            // 406
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Spickzettel", Plural = "Spickzettel" },
            // 407
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Spitzenleistung", Plural = "Spitzenleistungen" },
            // 408
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Sprachreservoir", Plural = "Sprachreservoire" },
            // 409
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Spuk", Plural = "Spuke" },
            // 410
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Stagnation", Plural = "Stagnationen" },
            // 411
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Standardwerk", Plural = "Standardwerke" },
            // 412
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Starrsinn", Plural = "Starrsinne" },
            // 413
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Statik", Plural = "Statiken" },
            // 414
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Statussymbol", Plural = "Statussymbole" },
            // 415
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Stellvertreter", Plural = "Stellvertreter" },
            // 416
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Stigmatisierung", Plural = "Stigmatisierungen" },
            // 417
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Stilmittel", Plural = "Stilmittel" },
            // 418
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Stoßseufzer", Plural = "Stoßseufzer" },
            // 419
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Streitfrage", Plural = "Streitfragen" },
            // 420
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Strohfeuer", Plural = "Strohfeuer" },
            // 421
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Strohhalm", Plural = "Strohhalme" },
            // 422
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Subkultur", Plural = "Subkulturen" },
            // 423
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Suchraster", Plural = "Suchraster" },
            // 424
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Suff", Plural = "Suffe" },
            // 425
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Symbiose", Plural = "Symbiosen" },
            // 426
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Syndikat", Plural = "Syndikate" },
            // 427
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Tadel", Plural = "Tadel" },
            // 428
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Talfahrt", Plural = "Talfahrten" },
            // 429
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Tandem", Plural = "Tandems" },
            // 430
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Tanzsaal", Plural = "Tanzsäle" },
            // 431
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Tapferkeit", Plural = "Tapferkeiten" },
            // 432
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Taufschein", Plural = "Taufscheine" },
            // 433
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Tausendsassa", Plural = "Tausendsassas" },
            // 434
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Teestube", Plural = "Teestuben" },
            // 435
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Temporalsystem", Plural = "Temporalsysteme" },
            // 436
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Theaterregisseur", Plural = "Theaterregisseure" },
            // 437
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Therapeutik", Plural = "Therapeutiken" },
            // 438
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Tierwohl", Plural = "Tierwohle" },
            // 439
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Tintenkiller", Plural = "Tintenkiller" },
            // 440
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Tirade", Plural = "Tiraden" },
            // 441
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Toleranzedikt", Plural = "Toleranzedikts" },
            // 442
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Torso", Plural = "Torsi" },
            // 443
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Trance", Plural = "Trancen" },
            // 444
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Treibhausgas", Plural = "Treibhausgase" },
            // 445
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Troll", Plural = "Trolle" },
            // 446
            new LanguageNoun
                { Level = "C1", Article = "die", Noun = "Tugendhaftigkeit", Plural = "Tugendhaftigkeiten" },
            // 447
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Ultimatum", Plural = "Ultimaten" },
            // 448
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Umschwung", Plural = "Umschwünge" },
            // 449
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Umwälzung", Plural = "Umwälzungen" },
            // 450
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Unbehagen", Plural = "Unbehagen" },
            // 451
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Unfug", Plural = "Unfuge" },
            // 452
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Unfruchtbarkeit", Plural = "Unfruchtbarkeiten" },
            // 453
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Unikat", Plural = "Unikate" },
            // 454
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Unmut", Plural = "Unmute" },
            // 455
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Unpässlichkeit", Plural = "Unpässlichkeiten" },
            // 456
            new LanguageNoun
                { Level = "C1", Article = "das", Noun = "Unterscheidungsmerkmal", Plural = "Unterscheidungsmerkmale" },
            // 457
            new LanguageNoun
                { Level = "C1", Article = "der", Noun = "Unterschiedspunkt", Plural = "Unterschiedspunkte" },
            // 458
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Unversehrtheit", Plural = "Unversehrtheiten" },
            // 459
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Uralterbe", Plural = "Uralterbe" },
            // 460
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Veranlass", Plural = "Veranlasse" },
            // 461
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Verbindlichkeit", Plural = "Verbindlichkeiten" },
            // 462
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Verdauungssystem", Plural = "Verdauungssysteme" },
            // 463
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Verdruss", Plural = "Verdrusse" },
            // 464
            new LanguageNoun
            {
                Level = "C1", Article = "die", Noun = "Vergangenheitserzählung", Plural = "Vergangenheitserzählungen"
            },
            // 465
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Verhaltensmuster", Plural = "Verhaltensmuster" },
            // 466
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Verkehrsverbund", Plural = "Verkehrsverbünde" },
            // 467
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Verlegerin", Plural = "Verlegerinnen" },
            // 468
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Verlustgeschäft", Plural = "Verlustgeschäfte" },
            // 469
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Vermarkter", Plural = "Vermarkter" },
            // 470
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Vermisstensuche", Plural = "Vermisstensuchen" },
            // 471
            new LanguageNoun
            {
                Level = "C1", Article = "das", Noun = "Vernehmlassungsverfahren", Plural = "Vernehmlassungsverfahren"
            },
            // 472
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Versager", Plural = "Versager" },
            // 473
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Versachlichung", Plural = "Versachlichungen" },
            // 474
            new LanguageNoun
                { Level = "C1", Article = "das", Noun = "Verteidigungsbündnis", Plural = "Verteidigungsbündnisse" },
            // 475
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Verunglückte", Plural = "Verunglückten" },
            // 476
            new LanguageNoun
                { Level = "C1", Article = "die", Noun = "Verzweiflungstat", Plural = "Verzweiflungstaten" },
            // 477
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Veto", Plural = "Vetos" },
            // 478
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Vexierspiegel", Plural = "Vexierspiegel" },
            // 479
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Vielfalt", Plural = "Vielfalten" },
            // 480
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Virusprogramm", Plural = "Virusprogramme" },
            // 481
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Volltreffer", Plural = "Volltreffer" },
            // 482
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Vorahnung", Plural = "Vorahnungen" },
            // 483
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Vordringen", Plural = "Vordringen" },
            // 484
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Vorhalt", Plural = "Vorhalte" },
            // 485
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Wachsamkeit", Plural = "Wachsamkeiten" },
            // 486
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Wagnis", Plural = "Wagnisse" },
            // 487
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Wahrer", Plural = "Wahrer" },
            // 488
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Wahnvorstellung", Plural = "Wahnvorstellungen" },
            // 489
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Wartesignal", Plural = "Wartesignale" },
            // 490
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Wechselbalg", Plural = "Wechselbälge" },
            // 491
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Widerlegung", Plural = "Widerlegungen" },
            // 492
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Wimmelbild", Plural = "Wimmelbilder" },
            // 493
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Windhauch", Plural = "Windhauche" },
            // 494
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Wogen", Plural = "Wogen" },
            // 495
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Wortgefecht", Plural = "Wortgefechte" },
            // 496
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Wucher", Plural = "Wucher" },
            // 497
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Wucht", Plural = "Wuchten" },
            // 498
            new LanguageNoun { Level = "C1", Article = "das", Noun = "Zähneknirschen", Plural = "Zähneknirschen" },
            // 499
            new LanguageNoun { Level = "C1", Article = "der", Noun = "Zank", Plural = "Zänke" },
            // 500
            new LanguageNoun { Level = "C1", Article = "die", Noun = "Zäsur", Plural = "Zäsuren" },
        };

        Service.UpdateNounsAsync(data);
    }
}