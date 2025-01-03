using LangVoyageServer.Models;

namespace LangVoyageServer.Importer;

public partial class DataImporter
{
    public void ImportB2Nouns()
    {
        var data = new[]
        {
            // 1
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Jurist", Plural = "Juristen" },
            // 2
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Anklage", Plural = "Anklagen" },
            // 3
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Gutachten", Plural = "Gutachten" },
            // 4
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Vortragende", Plural = "Vortragenden" },
            // 5
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Konferenz", Plural = "Konferenzen" },
            // 6
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Experiment", Plural = "Experimente" },
            // 7
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Wissenschaftler", Plural = "Wissenschaftler" },
            // 8
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Erfindung", Plural = "Erfindungen" },
            // 9
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Patent", Plural = "Patente" },
            // 10
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Ingenieur", Plural = "Ingenieure" },
            // 11
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Mechanik", Plural = "Mechaniken" },
            // 12
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Labor", Plural = "Labore" },
            // 13
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Einsatzbereich", Plural = "Einsatzbereiche" },
            // 14
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Strategie", Plural = "Strategien" },
            // 15
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Management", Plural = "Managements" },
            // 16
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Vorgesetzte", Plural = "Vorgesetzten" },
            // 17
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Ressource", Plural = "Ressourcen" },
            // 18
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Kapital", Plural = "Kapitalien" },
            // 19
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Geldgeber", Plural = "Geldgeber" },
            // 20
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Börse", Plural = "Börsen" },
            // 21
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Portfolio", Plural = "Portfolios" },
            // 22
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Redakteur", Plural = "Redakteure" },
            // 23
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Redaktion", Plural = "Redaktionen" },
            // 24
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Interview", Plural = "Interviews" },
            // 25
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Kolumnist", Plural = "Kolumnisten" },
            // 26
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Auflage", Plural = "Auflagen" },
            // 27
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Publikum", Plural = "Publika" },
            // 28
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Verlag", Plural = "Verlage" },
            // 29
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Leserschaft", Plural = "Leserschaften" },
            // 30
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Manuskript", Plural = "Manuskripte" },
            // 31
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Lektor", Plural = "Lektoren" },
            // 32
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Rezension", Plural = "Rezensionen" },
            // 33
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Event", Plural = "Events" },
            // 34
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Komplott", Plural = "Komplotte" },
            // 35
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Absprache", Plural = "Absprachen" },
            // 36
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Verfahren", Plural = "Verfahren" },
            // 37
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Kontrast", Plural = "Kontraste" },
            // 38
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Müdigkeit", Plural = "Müdigkeiten" },
            // 39
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Abenteuer", Plural = "Abenteuer" },
            // 40
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Gesichtspunkt", Plural = "Gesichtspunkte" },
            // 41
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Intention", Plural = "Intentionen" },
            // 42
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Auge", Plural = "Augen" },
            // 43
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Balkon", Plural = "Balkone" },
            // 44
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Batterie", Plural = "Batterien" },
            // 45
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Grundbedürfnis", Plural = "Grundbedürfnisse" },
            // 46
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Fachbegriff", Plural = "Fachbegriffe" },
            // 47
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Beratung", Plural = "Beratungen" },
            // 48
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Bild", Plural = "Bilder" },
            // 49
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Brandfall", Plural = "Brandfälle" },
            // 50
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Gelegenheit", Plural = "Gelegenheiten" },
            // 51
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Chaos", Plural = "Chaos" },
            // 52
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Charakterzug", Plural = "Charakterzüge" },
            // 53
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Erinnerung", Plural = "Erinnerungen" },
            // 54
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Erlebnis", Plural = "Erlebnisse" },
            // 55
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Fachmann", Plural = "Fachmänner" },
            // 56
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Kompetenz", Plural = "Kompetenzen" },
            // 57
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Gebirge", Plural = "Gebirge" },
            // 58
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Gedanke", Plural = "Gedanken" },
            // 59
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Erlaubnis", Plural = "Erlaubnisse" },
            // 60
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Gewissen", Plural = "Gewissen" },
            // 61
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Gipfel", Plural = "Gipfel" },
            // 62
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Gleichstellung", Plural = "Gleichstellungen" },
            // 63
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Glück", Plural = "Glücksmomente" },
            // 64
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Grundsatz", Plural = "Grundsätze" },
            // 65
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Einstellung", Plural = "Einstellungen" },
            // 66
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Handwerk", Plural = "Handwerke" },
            // 67
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Horizont", Plural = "Horizonte" },
            // 68
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Inspiration", Plural = "Inspirationen" },
            // 69
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Interesse", Plural = "Interessen" },
            // 70
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Arbeitsplatz", Plural = "Arbeitsplätze" },
            // 71
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Fachkompetenz", Plural = "Fachkompetenzen" },
            // 72
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Konzept", Plural = "Konzepte" },
            // 73
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Kredit", Plural = "Kredite" },
            // 74
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Kreativität", Plural = "Kreativitäten" },
            // 75
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Lebewesen", Plural = "Lebewesen" },
            // 76
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Arbeitslohn", Plural = "Arbeitslöhne" },
            // 77
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Überzeugung", Plural = "Überzeugungen" },
            // 78
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Motiv", Plural = "Motive" },
            // 79
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Nachteil", Plural = "Nachteile" },
            // 80
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Option", Plural = "Optionen" },
            // 81
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Projekt", Plural = "Projekte" },
            // 82
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Ratgeber", Plural = "Ratgeber" },
            // 83
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Reaktion", Plural = "Reaktionen" },
            // 84
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Rezept", Plural = "Rezepte" },
            // 85
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Sachschaden", Plural = "Sachschäden" },
            // 86
            new LanguageNoun
                { Level = "B2", Article = "die", Noun = "Eigenständigkeit", Plural = "Eigenständigkeiten" },
            // 87
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Signal", Plural = "Signale" },
            // 88
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Sinngehalt", Plural = "Sinngehalte" },
            // 89
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Sicherheit", Plural = "Sicherheiten" },
            // 90
            new LanguageNoun { Level = "B2", Article = "das", Noun = "System", Plural = "Systeme" },
            // 91
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Transport", Plural = "Transporte" },
            // 92
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Überzeugung", Plural = "Überzeugungen" },
            // 93
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Urteil", Plural = "Urteile" },
            // 94
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Vorgang", Plural = "Vorgänge" },
            // 95
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Voraussetzung", Plural = "Voraussetzungen" },
            // 96
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Vorurteil", Plural = "Vorurteile" },
            // 97
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Wertgegenstand", Plural = "Wertgegenstände" },
            // 98
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Wissenschaft", Plural = "Wissenschaften" },
            // 99
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Zeichen", Plural = "Zeichen" },
            // 100
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Zweck", Plural = "Zwecke" },
            // 101
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Abdruck", Plural = "Abdrücke" },
            // 102
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Abkürzung", Plural = "Abkürzungen" },
            // 103
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Abkommen", Plural = "Abkommen" },
            // 104
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Abschied", Plural = "Abschiede" },
            // 105
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Akzeptanz", Plural = "Akzeptanzen" },
            // 106
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Album", Plural = "Alben" },
            // 107
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Alltag", Plural = "Alltage" },
            // 108
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Anlage", Plural = "Anlagen" },
            // 109
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Anzeichen", Plural = "Anzeichen" },
            // 110
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Arbeiter", Plural = "Arbeiter" },
            // 111
            new LanguageNoun
                { Level = "B2", Article = "die", Noun = "Arbeitslosigkeit", Plural = "Arbeitslosigkeiten" },
            // 112
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Argument", Plural = "Argumente" },
            // 113
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Aufwand", Plural = "Aufwände" },
            // 114
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Aufmerksamkeit", Plural = "Aufmerksamkeiten" },
            // 115
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Ausmaß", Plural = "Ausmaße" },
            // 116
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Ausdruck", Plural = "Ausdrücke" },
            // 117
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Auswirkung", Plural = "Auswirkungen" },
            // 118
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Autohaus", Plural = "Autohäuser" },
            // 119
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Begleiter", Plural = "Begleiter" },
            // 120
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Begeisterung", Plural = "Begeisterungen" },
            // 121
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Beispiel", Plural = "Beispiele" },
            // 122
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Beleg", Plural = "Belege" },
            // 123
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Belohnung", Plural = "Belohnungen" },
            // 124
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Benehmen", Plural = "Benehmen" },
            // 125
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Fachbereich", Plural = "Fachbereiche" },
            // 126
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Berühmtheit", Plural = "Berühmtheiten" },
            // 127
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Bestehen", Plural = "Bestehen" },
            // 128
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Betrag", Plural = "Beträge" },
            // 129
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Bewegung", Plural = "Bewegungen" },
            // 130
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Bewusstsein", Plural = "Bewusstseinszustände" },
            // 131
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Bezug", Plural = "Bezüge" },
            // 132
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Bilanz", Plural = "Bilanzen" },
            // 133
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Blatt", Plural = "Blätter" },
            // 134
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Blick", Plural = "Blicke" },
            // 135
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Bühne", Plural = "Bühnen" },
            // 136
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Bündnis", Plural = "Bündnisse" },
            // 137
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Charme", Plural = "Charme" },
            // 138
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Debatte", Plural = "Debatten" },
            // 139
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Denken", Plural = "Denken" },
            // 140
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Dienst", Plural = "Dienste" },
            // 141
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Distanz", Plural = "Distanzen" },
            // 142
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Drama", Plural = "Dramen" },
            // 143
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Durchschnitt", Plural = "Durchschnitte" },
            // 144
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Effizienz", Plural = "Effizienzen" },
            // 145
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Eigentum", Plural = "Eigentümer" },
            // 146
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Eindruck", Plural = "Eindrücke" },
            // 147
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Einstellung", Plural = "Einstellungen" },
            // 148
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Element", Plural = "Elemente" },
            // 149
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Empfang", Plural = "Empfänge" },
            // 150
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Entwicklung", Plural = "Entwicklungen" },
            // 151
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Ereignis", Plural = "Ereignisse" },
            // 152
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Erfolg", Plural = "Erfolge" },
            // 153
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Erlaubnis", Plural = "Erlaubnisse" },
            // 154
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Ergebnis", Plural = "Ergebnisse" },
            // 155
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Erwerb", Plural = "Erwerbe" },
            // 156
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Forschung", Plural = "Forschungen" },
            // 157
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Forum", Plural = "Foren" },
            // 158
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Freundeskreis", Plural = "Freundeskreise" },
            // 159
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Funktion", Plural = "Funktionen" },
            // 160
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Gegenstück", Plural = "Gegenstücke" },
            // 161
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Genuss", Plural = "Genüsse" },
            // 162
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Geschichte", Plural = "Geschichten" },
            // 163
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Gewitter", Plural = "Gewitter" },
            // 164
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Grund", Plural = "Gründe" },
            // 165
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Herausforderung", Plural = "Herausforderungen" },
            // 166
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Highlight", Plural = "Highlights" },
            // 167
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Hof", Plural = "Höfe" },
            // 168
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Innovation", Plural = "Innovationen" },
            // 169
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Instrument", Plural = "Instrumente" },
            // 170
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Irrtum", Plural = "Irrtümer" },
            // 171
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Karriere", Plural = "Karrieren" },
            // 172
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Kriterium", Plural = "Kriterien" },
            // 173
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Lebensstandard", Plural = "Lebensstandards" },
            // 174
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Leidenschaft", Plural = "Leidenschaften" },
            // 175
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Lehrmaterial", Plural = "Lehrmaterialien" },
            // 176
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Lieferant", Plural = "Lieferanten" },
            // 177
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Menge", Plural = "Mengen" },
            // 178
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Netzwerk", Plural = "Netzwerke" },
            // 179
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Optimismus", Plural = "Optimismen" },
            // 180
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Option", Plural = "Optionen" },
            // 181
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Paradox", Plural = "Paradoxe" },
            // 182
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Philosoph", Plural = "Philosophen" },
            // 183
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Philosophie", Plural = "Philosophien" },
            // 184
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Publikum", Plural = "Publika" },
            // 185
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Rahmen", Plural = "Rahmen" },
            // 186
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Reduktion", Plural = "Reduktionen" },
            // 187
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Ressort", Plural = "Ressorts" },
            // 188
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Rohstoff", Plural = "Rohstoffe" },
            // 189
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Schwierigkeit", Plural = "Schwierigkeiten" },
            // 190
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Szenario", Plural = "Szenarien" },
            // 191
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Teilnehmer", Plural = "Teilnehmer" },
            // 192
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Tätigkeit", Plural = "Tätigkeiten" },
            // 193
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Umfeld", Plural = "Umfelder" },
            // 194
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Umstand", Plural = "Umstände" },
            // 195
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Unterscheidung", Plural = "Unterscheidungen" },
            // 196
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Verhältnis", Plural = "Verhältnisse" },
            // 197
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Verkauf", Plural = "Verkäufe" },
            // 198
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Verlosung", Plural = "Verlosungen" },
            // 199
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Versprechen", Plural = "Versprechen" },
            // 200
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Vorfall", Plural = "Vorfälle" },
            // 201
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Vorgabe", Plural = "Vorgaben" },
            // 202
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Wachstum", Plural = "Wachstumsphasen" },
            // 203
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Wechsel", Plural = "Wechsel" },
            // 204
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Weisheit", Plural = "Weisheiten" },
            // 205
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Ziel", Plural = "Ziele" },
            // 206
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Zweifel", Plural = "Zweifel" },
            // 207
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Absender", Plural = "Absender" },
            // 208
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Abrechnung", Plural = "Abrechnungen" },
            // 209
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Abenteuer", Plural = "Abenteuer" },
            // 210
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Administrator", Plural = "Administratoren" },
            // 211
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Absicherung", Plural = "Absicherungen" },
            // 212
            new LanguageNoun
                { Level = "B2", Article = "das", Noun = "Abschlusszeugnis", Plural = "Abschlusszeugnisse" },
            // 213
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Akteur", Plural = "Akteure" },
            // 214
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Aktivität", Plural = "Aktivitäten" },
            // 215
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Ambiente", Plural = "Ambientes" },
            // 216
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Anhang", Plural = "Anhänge" },
            // 217
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Anleitung", Plural = "Anleitungen" },
            // 218
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Anliegen", Plural = "Anliegen" },
            // 219
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Anschluss", Plural = "Anschlüsse" },
            // 220
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Antwort", Plural = "Antworten" },
            // 221
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Anzeichen", Plural = "Anzeichen" },
            // 222
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Arztbesuch", Plural = "Arztbesuche" },
            // 223
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Aufbewahrung", Plural = "Aufbewahrungen" },
            // 224
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Ausrüstungsgut", Plural = "Ausrüstungsgüter" },
            // 225
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Ausstoß", Plural = "Ausstöße" },
            // 226
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Ausstellung", Plural = "Ausstellungen" },
            // 227
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Automobil", Plural = "Automobile" },
            // 228
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Bauherr", Plural = "Bauherren" },
            // 229
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Bearbeitung", Plural = "Bearbeitungen" },
            // 230
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Beispiel", Plural = "Beispiele" },
            // 231
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Beirat", Plural = "Beiräte" },
            // 232
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Bekanntschaft", Plural = "Bekanntschaften" },
            // 233
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Belieben", Plural = "Belieben" },
            // 234
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Bereich", Plural = "Bereiche" },
            // 235
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Berichtigung", Plural = "Berichtigungen" },
            // 236
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Besitztum", Plural = "Besitztümer" },
            // 237
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Beweis", Plural = "Beweise" },
            // 238
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Bewertung", Plural = "Bewertungen" },
            // 239
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Bildnis", Plural = "Bildnisse" },
            // 240
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Block", Plural = "Blöcke" },
            // 241
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Börse", Plural = "Börsen" },
            // 242
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Budget", Plural = "Budgets" },
            // 243
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Chor", Plural = "Chöre" },
            // 244
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Chance", Plural = "Chancen" },
            // 245
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Chaos", Plural = "Chaos" },
            // 246
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Diplomat", Plural = "Diplomaten" },
            // 247
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Disziplin", Plural = "Disziplinen" },
            // 248
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Durcheinander", Plural = "Durcheinander" },
            // 249
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Einfluss", Plural = "Einflüsse" },
            // 250
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Einstellung", Plural = "Einstellungen" },
            // 251
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Element", Plural = "Elemente" },
            // 252
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Erfinder", Plural = "Erfinder" },
            // 253
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Erkenntnis", Plural = "Erkenntnisse" },
            // 254
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Ereignis", Plural = "Ereignisse" },
            // 255
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Faktor", Plural = "Faktoren" },
            // 256
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Fähigkeit", Plural = "Fähigkeiten" },
            // 257
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Fahrzeug", Plural = "Fahrzeuge" },
            // 258
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Fehler", Plural = "Fehler" },
            // 259
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Finanzierung", Plural = "Finanzierungen" },
            // 260
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Fluggerät", Plural = "Fluggeräte" },
            // 261
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Förderer", Plural = "Förderer" },
            // 262
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Funktion", Plural = "Funktionen" },
            // 263
            new LanguageNoun
                { Level = "B2", Article = "das", Noun = "Garantieversprechen", Plural = "Garantieversprechen" },
            // 264
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Bereich", Plural = "Bereiche" },
            // 265
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Geburt", Plural = "Geburten" },
            // 266
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Gegenteil", Plural = "Gegenteile" },
            // 267
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Geruch", Plural = "Gerüche" },
            // 268
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Gerechtigkeit", Plural = "Gerechtigkeiten" },
            // 269
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Geschick", Plural = "Geschicke" },
            // 270
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Gesichtspunkt", Plural = "Gesichtspunkte" },
            // 271
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Geschwindigkeit", Plural = "Geschwindigkeiten" },
            // 272
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Gewicht", Plural = "Gewichte" },
            // 273
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Hauptsitz", Plural = "Hauptsitze" },
            // 274
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Heimat", Plural = "Heimaten" },
            // 275
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Hobby", Plural = "Hobbys" },
            // 276
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Idealfall", Plural = "Idealfälle" },
            // 277
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Illustration", Plural = "Illustrationen" },
            // 278
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Immunsystem", Plural = "Immunsysteme" },
            // 279
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Imker", Plural = "Imker" },
            // 280
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Innovation", Plural = "Innovationen" },
            // 281
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Interesse", Plural = "Interessen" },
            // 282
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Jagdausflug", Plural = "Jagdausflüge" },
            // 283
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Jury", Plural = "Jurys" },
            // 284
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Kaliber", Plural = "Kaliber" },
            // 285
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Kammerdiener", Plural = "Kammerdiener" },
            // 286
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Kapelle", Plural = "Kapellen" },
            // 287
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Kapitel", Plural = "Kapitel" },
            // 288
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Adlige", Plural = "Adligen" },
            // 289
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Ärger", Plural = "Ärgernisse" },
            // 290
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Annahme", Plural = "Annahmen" },
            // 291
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Anteil", Plural = "Anteile" },
            // 292
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Atmosphäre", Plural = "Atmosphären" },
            // 293
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Attacke", Plural = "Attacken" },
            // 294
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Aufgabe", Plural = "Aufgaben" },
            // 295
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Auflösung", Plural = "Auflösungen" },
            // 296
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Ausnahme", Plural = "Ausnahmen" },
            // 297
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Beleidigung", Plural = "Beleidigungen" },
            // 298
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Besatzung", Plural = "Besatzungen" },
            // 299
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Bestimmung", Plural = "Bestimmungen" },
            // 300
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Betrieb", Plural = "Betriebe" },
            // 301
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Beziehung", Plural = "Beziehungen" },
            // 302
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Botschafter", Plural = "Botschafter" },
            // 303
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Buchhandlung", Plural = "Buchhandlungen" },
            // 304
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Dauer", Plural = "Dauern" },
            // 305
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Demonstration", Plural = "Demonstrationen" },
            // 306
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Design", Plural = "Designs" },
            // 307
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Dilemma", Plural = "Dilemmas" },
            // 308
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Ding", Plural = "Dinge" },
            // 309
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Drehung", Plural = "Drehungen" },
            // 310
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Drohung", Plural = "Drohungen" },
            // 311
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Durchbruch", Plural = "Durchbrüche" },
            // 312
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Ehrgeiz", Plural = "Ehrgeize" },
            // 313
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Eidechse", Plural = "Eidechsen" },
            // 314
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Einrichtung", Plural = "Einrichtungen" },
            // 315
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Empfehlung", Plural = "Empfehlungen" },
            // 316
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Ende", Plural = "Enden" },
            // 317
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Engagement", Plural = "Engagements" },
            // 318
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Entdeckung", Plural = "Entdeckungen" },
            // 319
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Entschädigung", Plural = "Entschädigungen" },
            // 320
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Ersparnis", Plural = "Ersparnisse" },
            // 321
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Fabrik", Plural = "Fabriken" },
            // 322
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Experte", Plural = "Experten" },
            // 323
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Fassung", Plural = "Fassungen" },
            // 324
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Feind", Plural = "Feinde" },
            // 325
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Ferienwohnung", Plural = "Ferienwohnungen" },
            // 326
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Fernbedienung", Plural = "Fernbedienungen" },
            // 327
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Festnahme", Plural = "Festnahmen" },
            // 328
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Führungskraft", Plural = "Führungskräfte" },
            // 329
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Gelegenheit", Plural = "Gelegenheiten" },
            // 330
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Geplante", Plural = "Geplantes" },
            // 331
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Geschirrspüler", Plural = "Geschirrspüler" },
            // 332
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Gesetz", Plural = "Gesetze" },
            // 333
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Getränkemarkt", Plural = "Getränkemärkte" },
            // 334
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Gewinn", Plural = "Gewinne" },
            // 335
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Gewohnheit", Plural = "Gewohnheiten" },
            // 336
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Gipsverband", Plural = "Gipsverbände" },
            // 337
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Gläubiger", Plural = "Gläubiger" },
            // 338
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Glocke", Plural = "Glocken" },
            // 339
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Gürtel", Plural = "Gürtel" },
            // 340
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Hafen", Plural = "Häfen" },
            // 341
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Hauptfach", Plural = "Hauptfächer" },
            // 342
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Hauptstadt", Plural = "Hauptstädte" },
            // 343
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Heirat", Plural = "Heiraten" },
            // 344
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Held", Plural = "Helden" },
            // 345
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Herstellung", Plural = "Herstellungen" },
            // 346
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Hilfsmittel", Plural = "Hilfsmittel" },
            // 347
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Hinweis", Plural = "Hinweise" },
            // 348
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Hosentasche", Plural = "Hosentaschen" },
            // 349
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Hülle", Plural = "Hüllen" },
            // 350
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Impuls", Plural = "Impulse" },
            // 351
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Inland", Plural = "Inländer" },
            // 352
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Inspektion", Plural = "Inspektionen" },
            // 353
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Jackpot", Plural = "Jackpots" },
            // 354
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Juwelier", Plural = "Juweliere" },
            // 355
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Kampagne", Plural = "Kampagnen" },
            // 356
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Kapitän", Plural = "Kapitäne" },
            // 357
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Kaution", Plural = "Kautionen" },
            // 358
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Kinderwagen", Plural = "Kinderwagen" },
            // 359
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Kissen", Plural = "Kissen" },
            // 360
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Komfort", Plural = "Komforts" },
            // 361
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Konferenz", Plural = "Konferenzen" },
            // 362
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Konflikt", Plural = "Konflikte" },
            // 363
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Konsument", Plural = "Konsumenten" },
            // 364
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Kontaktaufnahme", Plural = "Kontaktaufnahmen" },
            // 365
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Korruption", Plural = "Korruptionen" },
            // 366
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Kraft", Plural = "Kräfte" },
            // 367
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Kreislauf", Plural = "Kreisläufe" },
            // 368
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Kreuzfahrtschiff", Plural = "Kreuzfahrtschiffe" },
            // 369
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Kriminalität", Plural = "Kriminalitäten" },
            // 370
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Kritiker", Plural = "Kritiker" },
            // 371
            new LanguageNoun
                { Level = "B2", Article = "die", Noun = "Kulturveranstaltung", Plural = "Kulturveranstaltungen" },
            // 372
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Kundgebung", Plural = "Kundgebungen" },
            // 373
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Kursleiter", Plural = "Kursleiter" },
            // 374
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Kuss", Plural = "Küsse" },
            // 375
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Lebensunterhalt", Plural = "Lebensunterhalte" },
            // 376
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Lehrstelle", Plural = "Lehrstellen" },
            // 377
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Leid", Plural = "Leiden" },
            // 378
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Lichtbild", Plural = "Lichtbilder" },
            // 379
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Limit", Plural = "Limits" },
            // 380
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Lockerung", Plural = "Lockerungen" },
            // 381
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Lüge", Plural = "Lügen" },
            // 382
            new LanguageNoun
                { Level = "B2", Article = "die", Noun = "Luftfeuchtigkeit", Plural = "Luftfeuchtigkeiten" },
            // 383
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Luxus", Plural = "Luxusgüter" },
            // 384
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Mangel", Plural = "Mängel" },
            // 385
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Mangelware", Plural = "Mangelwaren" },
            // 386
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Markenzeichen", Plural = "Markenzeichen" },
            // 387
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Meeresfrüchte", Plural = "Meeresfrüchte" },
            // 388
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Meile", Plural = "Meilen" },
            // 389
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Messer", Plural = "Messer" },
            // 390
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Mindestlohn", Plural = "Mindestlöhne" },
            // 391
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Misserfolg", Plural = "Misserfolge" },
            // 392
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Mitgefühl", Plural = "Mitgefühle" },
            // 393
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Mitteilung", Plural = "Mitteilungen" },
            // 394
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Modedesigner", Plural = "Modedesigner" },
            // 395
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Möblierung", Plural = "Möblierungen" },
            // 396
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Muster", Plural = "Muster" },
            // 397
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Nachbarland", Plural = "Nachbarländer" },
            // 398
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Nachforschung", Plural = "Nachforschungen" },
            // 399
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Nachhaltigkeit", Plural = "Nachhaltigkeiten" },
            // 400
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Nahrung", Plural = "Nahrungen" },
            // 401
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Neugier", Plural = "Neugieren" },
            // 402
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Niederlage", Plural = "Niederlagen" },
            // 403
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Notfall", Plural = "Notfälle" },
            // 404
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Nutzung", Plural = "Nutzungen" },
            // 405
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Obdachlosigkeit", Plural = "Obdachlosigkeiten" },
            // 406
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Ohrring", Plural = "Ohrringe" },
            // 407
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Oper", Plural = "Opern" },
            // 408
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Ordnung", Plural = "Ordnungen" },
            // 409
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Packung", Plural = "Packungen" },
            // 410
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Papierkram", Plural = "Papierkrams" },
            // 411
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Parkbank", Plural = "Parkbänke" },
            // 412
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Parkverbot", Plural = "Parkverbote" },
            // 413
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Pedal", Plural = "Pedale" },
            // 414
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Peinlichkeit", Plural = "Peinlichkeiten" },
            // 415
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Personalausweis", Plural = "Personalausweise" },
            // 416
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Pflicht", Plural = "Pflichten" },
            // 417
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Pfleger", Plural = "Pfleger" },
            // 418
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Pflücke", Plural = "Pflücken" },
            // 419
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Phänomen", Plural = "Phänomene" },
            // 420
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Pilgerreise", Plural = "Pilgerreisen" },
            // 421
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Pilot", Plural = "Piloten" },
            // 422
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Planung", Plural = "Planungen" },
            // 423
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Platte", Plural = "Platten" },
            // 424
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Portemonnaie", Plural = "Portemonnaies" },
            // 425
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Postfach", Plural = "Postfächer" },
            // 426
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Privileg", Plural = "Privilegien" },
            // 427
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Protest", Plural = "Proteste" },
            // 428
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Protokoll", Plural = "Protokolle" },
            // 429
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Publizist", Plural = "Publizisten" },
            // 430
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Pumpe", Plural = "Pumpen" },
            // 431
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Rarität", Plural = "Raritäten" },
            // 432
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Redner", Plural = "Redner" },
            // 433
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Reklamation", Plural = "Reklamationen" },
            // 434
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Rente", Plural = "Renten" },
            // 435
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Rettung", Plural = "Rettungen" },
            // 436
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Revolution", Plural = "Revolutionen" },
            // 437
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Richtung", Plural = "Richtungen" },
            // 438
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Risiko", Plural = "Risiken" },
            // 439
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Roller", Plural = "Roller" },
            // 440
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Rolltreppe", Plural = "Rolltreppen" },
            // 441
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Rückfall", Plural = "Rückfälle" },
            // 442
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Ruf", Plural = "Rufe" },
            // 443
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Ruhe", Plural = "Ruhen" },
            // 444
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Sache", Plural = "Sachen" },
            // 445
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Schatten", Plural = "Schatten" },
            // 446
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Scheitern", Plural = "Scheitern" },
            // 447
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Schauspielhaus", Plural = "Schauspielhäuser" },
            // 448
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Schicksal", Plural = "Schicksale" },
            // 449
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Schließfach", Plural = "Schließfächer" },
            // 450
            new LanguageNoun
                { Level = "B2", Article = "die", Noun = "Schlussfolgerung", Plural = "Schlussfolgerungen" },
            // 451
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Schmerzmittel", Plural = "Schmerzmittel" },
            // 452
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Schornstein", Plural = "Schornsteine" },
            // 453
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Schriftsteller", Plural = "Schriftsteller" },
            // 454
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Schwerpunkt", Plural = "Schwerpunkte" },
            // 455
            new LanguageNoun
                { Level = "B2", Article = "das", Noun = "Selbstbewusstsein", Plural = "Selbstbewusstseine" },
            // 456
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Selbstbedienung", Plural = "Selbstbedienungen" },
            // 457
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Seminarraum", Plural = "Seminarräume" },
            // 458
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Sessel", Plural = "Sessel" },
            // 459
            new LanguageNoun
                { Level = "B2", Article = "das", Noun = "Sicherheitssystem", Plural = "Sicherheitssysteme" },
            // 460
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Sichtweise", Plural = "Sichtweisen" },
            // 461
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Socke", Plural = "Socken" },
            // 462
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Sommerzeit", Plural = "Sommerzeiten" },
            // 463
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Spalte", Plural = "Spalten" },
            // 464
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Spende", Plural = "Spenden" },
            // 465
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Sperrung", Plural = "Sperrungen" },
            // 466
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Spiegelung", Plural = "Spiegelungen" },
            // 467
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Spion", Plural = "Spione" },
            // 468
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Sprachebene", Plural = "Sprachebenen" },
            // 469
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Stadtrundfahrt", Plural = "Stadtrundfahrten" },
            // 470
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Stall", Plural = "Ställe" },
            // 471
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Startpunkt", Plural = "Startpunkte" },
            // 472
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Steckbrief", Plural = "Steckbriefe" },
            // 473
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Steuererklärung", Plural = "Steuererklärungen" },
            // 474
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Stoßzeit", Plural = "Stoßzeiten" },
            // 475
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Störung", Plural = "Störungen" },
            // 476
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Struktur", Plural = "Strukturen" },
            // 477
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Talente", Plural = "Talente" },
            // 478
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Teich", Plural = "Teiche" },
            // 479
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Theke", Plural = "Theken" },
            // 480
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Tiefe", Plural = "Tiefen" },
            // 481
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Träne", Plural = "Tränen" },
            // 482
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Trend", Plural = "Trends" },
            // 483
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Trost", Plural = "Troste" },
            // 484
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Umgebung", Plural = "Umgebungen" },
            // 485
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Umleitung", Plural = "Umleitungen" },
            // 486
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Unabhängigkeit", Plural = "Unabhängigkeiten" },
            // 487
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Ungerechtigkeit", Plural = "Ungerechtigkeiten" },
            // 488
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Unfallbericht", Plural = "Unfallberichte" },
            // 489
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Unterhaltung", Plural = "Unterhaltungen" },
            // 490
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Unterkunft", Plural = "Unterkünfte" },
            // 491
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Unwetter", Plural = "Unwetter" },
            // 492
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Verbesserung", Plural = "Verbesserungen" },
            // 493
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Verdacht", Plural = "Verdächte" },
            // 494
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Verlagshaus", Plural = "Verlagshäuser" },
            // 495
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Verlust", Plural = "Verluste" },
            // 496
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Verpflegung", Plural = "Verpflegungen" },
            // 497
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Verpflichtung", Plural = "Verpflichtungen" },
            // 498
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Vermögen", Plural = "Vermögen" },
            // 499
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Vernetzung", Plural = "Vernetzungen" },
            // 500
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Verordnung", Plural = "Verordnungen" },
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Fortschritt", Plural = "Fortschritte" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Konjunktur", Plural = "Konjunkturen" },
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Kriterium", Plural = "Kriterien" },
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Bestandteil", Plural = "Bestandteile" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Toleranz", Plural = "Toleranzen" },
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Engagement", Plural = "Engagements" },
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Wettbewerb", Plural = "Wettbewerbe" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Perspektive", Plural = "Perspektiven" },
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Verfahren", Plural = "Verfahren" },
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Verlauf", Plural = "Verläufe" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Kooperation", Plural = "Kooperationen" },
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Anspruch", Plural = "Ansprüche" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Erfahrung", Plural = "Erfahrungen" },
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Ergebnis", Plural = "Ergebnisse" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Fähigkeit", Plural = "Fähigkeiten" },
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Gegensatz", Plural = "Gegensätze" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Herausforderung", Plural = "Herausforderungen" },
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Interesse", Plural = "Interessen" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Kritik", Plural = "Kritiken" },
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Mangel", Plural = "Mängel" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Möglichkeit", Plural = "Möglichkeiten" },
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Problem", Plural = "Probleme" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Reaktion", Plural = "Reaktionen" },
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Schritt", Plural = "Schritte" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Situation", Plural = "Situationen" },
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Unterschied", Plural = "Unterschiede" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Veränderung", Plural = "Veränderungen" },
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Verhalten", Plural = "Verhalten" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Verantwortung", Plural = "Verantwortungen" },
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Vorteil", Plural = "Vorteile" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Wirkung", Plural = "Wirkungen" },
            new LanguageNoun { Level = "B2", Article = "das", Noun = "Ziel", Plural = "Ziele" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Zukunft", Plural = "Zukünfte" },
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Zweck", Plural = "Zwecke" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Anforderung", Plural = "Anforderungen" },
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Aufwand", Plural = "Aufwände" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Bedingung", Plural = "Bedingungen" },
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Bereich", Plural = "Bereiche" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Beschäftigung", Plural = "Beschäftigungen" },
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Betrag", Plural = "Beträge" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Beziehung", Plural = "Beziehungen" },
            new LanguageNoun { Level = "B2", Article = "der", Noun = "Einfluss", Plural = "Einflüsse" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Entwicklung", Plural = "Entwicklungen" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Erklärung", Plural = "Erklärungen" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Erwartung", Plural = "Erwartungen" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Forderung", Plural = "Forderungen" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Funktion", Plural = "Funktionen" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Gemeinschaft", Plural = "Gemeinschaften" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Hilfe", Plural = "Hilfen" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Information", Plural = "Informationen" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Lösung", Plural = "Lösungen" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Maßnahme", Plural = "Maßnahmen" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Methode", Plural = "Methoden" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Möglichkeit", Plural = "Möglichkeiten" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Nachricht", Plural = "Nachrichten" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Organisation", Plural = "Organisationen" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Planung", Plural = "Planungen" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Position", Plural = "Positionen" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Reaktion", Plural = "Reaktionen" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Regel", Plural = "Regeln" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Rolle", Plural = "Rollen" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Schwierigkeit", Plural = "Schwierigkeiten" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Situation", Plural = "Situationen" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Strategie", Plural = "Strategien" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Struktur", Plural = "Strukturen" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Technik", Plural = "Techniken" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Theorie", Plural = "Theorien" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Umsetzung", Plural = "Umsetzungen" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Veränderung", Plural = "Veränderungen" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Verbindung", Plural = "Verbindungen" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Verfügung", Plural = "Verfügungen" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Verhandlung", Plural = "Verhandlungen" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Verpflichtung", Plural = "Verpflichtungen" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Voraussetzung", Plural = "Voraussetzungen" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Vorstellung", Plural = "Vorstellungen" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Wirkung", Plural = "Wirkungen" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Zielsetzung", Plural = "Zielsetzungen" },
            new LanguageNoun { Level = "B2", Article = "die", Noun = "Zusammenarbeit", Plural = "Zusammenarbeiten" },
        };

        Service.UpdateNounsAsync(data);
    }
}