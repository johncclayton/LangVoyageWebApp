using LangVoyageServer.Models;

namespace LangVoyageServer.Importer;

public partial class DataImporter
{
    public void ImportC2Nouns()
    {
        var data = new[]
        {
            // 1
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Abakus", Plural = "Abaki" },
            // 2
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Abdankung", Plural = "Abdankungen" },
            // 3
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Aberwitz", Plural = "Aberwitze" },
            // 4
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Abglanz", Plural = "Abglänze" },
            // 5
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Abkehr", Plural = "Abkehren" },
            // 6
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Absolutum", Plural = "Absoluta" },
            // 7
            new LanguageNoun
                { Level = "C2", Article = "der", Noun = "Absolventenjahrgang", Plural = "Absolventenjahrgänge" },
            // 8
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Absonderung", Plural = "Absonderungen" },
            // 9
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Abstellgleis", Plural = "Abstellgleise" },
            // 10
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Abstinenzler", Plural = "Abstinenzler" },

            // 11
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Abstrusität", Plural = "Abstrusitäten" },
            // 12
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Abtragungsmodell", Plural = "Abtragungsmodelle" },
            // 13
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Achsenbruch", Plural = "Achsenbrüche" },
            // 14
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Adaption", Plural = "Adaptionen" },
            // 15
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Adelsprädikat", Plural = "Adelsprädikate" },
            // 16
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Adressnachweis", Plural = "Adressnachweise" },
            // 17
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Affekthandlung", Plural = "Affekthandlungen" },
            // 18
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Affentheater", Plural = "Affentheater" },
            // 19
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Agitator", Plural = "Agitatoren" },
            // 20
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Agonie", Plural = "Agonien" },

            // 21
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Agrarwesen", Plural = "Agrarwesen" },
            // 22
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Albdruck", Plural = "Albdrucke" },
            // 23
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Alchemie", Plural = "Alchemien" },
            // 24
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Aliment", Plural = "Alimente" },
            // 25
            new LanguageNoun
                { Level = "C2", Article = "der", Noun = "Allmachtsanspruch", Plural = "Allmachtsansprüche" },
            // 26
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Allmende", Plural = "Allmenden" },
            // 27
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Allroundtalent", Plural = "Allroundtalente" },
            // 28
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Altersquotient", Plural = "Altersquotienten" },
            // 29
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Alterung", Plural = "Alterungen" },
            // 30
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Amalgam", Plural = "Amalgame" },

            // 31
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Amboss", Plural = "Ambosse" },
            // 32
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Ammenmärchen", Plural = "Ammenmärchen" },
            // 33
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Amphitheater", Plural = "Amphitheater" },
            // 34
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Amtsinhaber", Plural = "Amtsinhaber" },
            // 35
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Analogiebildung", Plural = "Analogiebildungen" },
            // 36
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Analphabetentum", Plural = "Analphabetentümer" },
            // 37
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Anarchist", Plural = "Anarchisten" },
            // 38
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Anbahnung", Plural = "Anbahnungen" },
            // 39
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Anbetungsritual", Plural = "Anbetungsrituale" },
            // 40
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Anbruch", Plural = "Anbrüche" },

            // 41
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Andacht", Plural = "Andachten" },
            // 42
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Andenhochland", Plural = "Andenhochländer" },
            // 43
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Anekdotenschatz", Plural = "Anekdotenschätze" },
            // 44
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Anfeindung", Plural = "Anfeindungen" },
            // 45
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Angstsyndrom", Plural = "Angstsyndrome" },
            // 46
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Animalismus", Plural = "Animalismen" },
            // 47
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Animierdame", Plural = "Animierdamen" },
            // 48
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Annuitätendarlehen", Plural = "Annuitätendarlehen" },
            // 49
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Anpfiff", Plural = "Anpfiffe" },
            // 50
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Anreizstruktur", Plural = "Anreizstrukturen" },

            // 51
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Anstandsgefühl", Plural = "Anstandsgefühle" },
            // 52
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Antagonismus", Plural = "Antagonismen" },
            // 53
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Anthologie", Plural = "Anthologien" },
            // 54
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Antlitz", Plural = "Antlitze" },
            // 55
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Apologet", Plural = "Apologeten" },
            // 56
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Apostrophe", Plural = "Apostrophen" },
            // 57
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Aquarell", Plural = "Aquarelle" },
            // 58
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Arbeitsnachweis", Plural = "Arbeitsnachweise" },
            // 59
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Argwohn", Plural = "Argwohne" },
            // 60
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Arkadien", Plural = "Arkadien" },

            // 61
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Arkadenhof", Plural = "Arkadenhöfe" },
            // 62
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Armlänge", Plural = "Armlängen" },
            // 63
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Arzneimittelgesetz", Plural = "Arzneimittelgesetze" },
            // 64
            new LanguageNoun
                { Level = "C2", Article = "der", Noun = "Asylbewerberstrom", Plural = "Asylbewerberströme" },
            // 65
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Attitüde", Plural = "Attitüden" },
            // 66
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Auktionsrecht", Plural = "Auktionsrechte" },
            // 67
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Augenblickswert", Plural = "Augenblickswerte" },
            // 68
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Aufarbeitung", Plural = "Aufarbeitungen" },
            // 69
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Aufatmen", Plural = "Aufatmen" },
            // 70
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Aufmarsch", Plural = "Aufmärsche" },

            // 71
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Aufspaltung", Plural = "Aufspaltungen" },
            // 72
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Aufwühlende", Plural = "Aufwühlende" },
            // 73
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Augenschmaus", Plural = "Augenschmäuse" },
            // 74
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Aura", Plural = "Auren" },
            // 75
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Auskunftsersuchen", Plural = "Auskunftsersuchen" },
            // 76
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Ausläufer", Plural = "Ausläufer" },
            // 77
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Aussichtslosigkeit", Plural = "Aussichtslosigkeiten" },
            // 78
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Ausstandsgeschenk", Plural = "Ausstandsgeschenke" },
            // 79
            new LanguageNoun
                { Level = "C2", Article = "der", Noun = "Authentizitätsbeweis", Plural = "Authentizitätsbeweise" },
            // 80
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Autoritätshörigkeit", Plural = "Autoritätshörigkeiten" },

            // 81
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Autorenhonorar", Plural = "Autorenhonorare" },
            // 82
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Autowahn", Plural = "Autowahne" },
            // 83
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Avalanche", Plural = "Avalanchen" },
            // 84
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Axiom", Plural = "Axiome" },
            // 85
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Bachlauf", Plural = "Bachläufe" },
            // 86
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Backlist", Plural = "Backlisten" },
            // 87
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Bagatellverfahren", Plural = "Bagatellverfahren" },
            // 88
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Bahndamm", Plural = "Bahndämme" },
            // 89
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Balustrade", Plural = "Balustraden" },
            // 90
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Bankhaus", Plural = "Bankhäuser" },

            // 91
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Bariton", Plural = "Baritone" },
            // 92
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Baukunst", Plural = "Baukünste" },
            // 93
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Bauland", Plural = "Bauländer" },
            // 94
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Bauwagen", Plural = "Bauwagen" },
            // 95
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Beaufsichtigung", Plural = "Beaufsichtigungen" },
            // 96
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Becherglas", Plural = "Bechergläser" },
            // 97
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Beckenknochen", Plural = "Beckenknochen" },
            // 98
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Bedachtsamkeit", Plural = "Bedachtsamkeiten" },
            // 99
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Bedenkenmoment", Plural = "Bedenkenmomente" },
            // 100
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Bedienfehler", Plural = "Bedienfehler" },

            // 101
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Bedrohungslage", Plural = "Bedrohungslagen" },
            // 102
            new LanguageNoun
            {
                Level = "C2", Article = "das", Noun = "Beeinträchtigungsniveau", Plural = "Beeinträchtigungsniveaus"
            },
            // 103
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Beelzebub", Plural = "Beelzebuben" },
            // 104
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Befähigung", Plural = "Befähigungen" },
            // 105
            new LanguageNoun
            {
                Level = "C2", Article = "das", Noun = "Befindlichkeitsprotokoll", Plural = "Befindlichkeitsprotokolle"
            },
            // 106
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Befürworter", Plural = "Befürworter" },
            // 107
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Begabungsgeschichte", Plural = "Begabungsgeschichten" },
            // 108
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Beiboot", Plural = "Beiboote" },
            // 109
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Beiratsposten", Plural = "Beiratsposten" },
            // 110
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Beize", Plural = "Beizen" },

            // 111
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Bekennerschreiben", Plural = "Bekennerschreiben" },
            // 112
            new LanguageNoun
                { Level = "C2", Article = "der", Noun = "Belagerungszustand", Plural = "Belagerungszustände" },
            // 113
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Belastungsprobe", Plural = "Belastungsproben" },
            // 114
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Beleuchtungskonzept", Plural = "Beleuchtungskonzepte" },
            // 115
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Beliebtheitsgrad", Plural = "Beliebtheitsgrade" },
            // 116
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Bemerkenswürdigkeit", Plural = "Bemerkenswürdigkeiten" },
            // 117
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Benefizkonzert", Plural = "Benefizkonzerte" },
            // 118
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Benzinpreis", Plural = "Benzinpreise" },
            // 119
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Beredsamkeit", Plural = "Beredsamkeiten" },
            // 120
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Bergmassiv", Plural = "Bergmassive" },

            // 121
            new LanguageNoun
                { Level = "C2", Article = "der", Noun = "Berieselungseffekt", Plural = "Berieselungseffekte" },
            // 122
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Berührungsschwelle", Plural = "Berührungsschwellen" },
            // 123
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Besatzungsrecht", Plural = "Besatzungsrechte" },
            // 124
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Bescheid", Plural = "Bescheide" },
            // 125
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Beschwernis", Plural = "Beschwernisse" },
            // 126
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Bestimmungswort", Plural = "Bestimmungswörter" },
            // 127
            new LanguageNoun
                { Level = "C2", Article = "der", Noun = "Beteuerungseffekt", Plural = "Beteuerungseffekte" },
            // 128
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Bevölkerungsbewegung", Plural = "Bevölkerungsbewegungen" },
            // 129
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Bewegungsschema", Plural = "Bewegungsschemata" },
            // 130
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Beweisgegenstand", Plural = "Beweisgegenstände" },

            // 131
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Beziehungsebene", Plural = "Beziehungsebenen" },
            // 132
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Bienenwachs", Plural = "Bienenwachse" },
            // 133
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Binnenmarkt", Plural = "Binnenmärkte" },
            // 134
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Biografie", Plural = "Biografien" },
            // 135
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Blechblasinstrument", Plural = "Blechblasinstrumente" },
            // 136
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Blickfang", Plural = "Blickfänge" },
            // 137
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Blindschleiche", Plural = "Blindschleichen" },
            // 138
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Blitzlichtgewitter", Plural = "Blitzlichtgewitter" },
            // 139
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Blockhausstil", Plural = "Blockhausstile" },
            // 140
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Blutrache", Plural = "Blutrachen" },

            // 141
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Bodendenkmal", Plural = "Bodendenkmäler" },
            // 142
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Bohrenzähler", Plural = "Bohrenzähler" },
            // 143
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Bohrinsel", Plural = "Bohrinseln" },
            // 144
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Bonusheft", Plural = "Bonushefte" },
            // 145
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Botschaftler", Plural = "Botschaftler" },
            // 146
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Brandmauer", Plural = "Brandmauern" },
            // 147
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Brauchtum", Plural = "Brauchtümer" },
            // 148
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Brautschleier", Plural = "Brautschleier" },
            // 149
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Brieftasche", Plural = "Brieftaschen" },
            // 150
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Bruchstück", Plural = "Bruchstücke" },

            // 151
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Buchdeckel", Plural = "Buchdeckel" },
            // 152
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Buchstabierkunst", Plural = "Buchstabierkünste" },
            // 153
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Buddelspiel", Plural = "Buddelspiele" },
            // 154
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Buhmann", Plural = "Buhmänner" },
            // 155
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Bundeslade", Plural = "Bundesladen" },
            // 156
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Buntglasfenster", Plural = "Buntglasfenster" },
            // 157
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Burggraf", Plural = "Burggrafen" },
            // 158
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Burgmauer", Plural = "Burgmauern" },
            // 159
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Butterfass", Plural = "Butterfässer" },
            // 160
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Chansonnier", Plural = "Chansonniers" },

            // 161
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Charta", Plural = "Chartas" },
            // 162
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Charterflugzeug", Plural = "Charterflugzeuge" },
            // 163
            new LanguageNoun
                { Level = "C2", Article = "der", Noun = "Chirurgeneingriff", Plural = "Chirurgeneingriffe" },
            // 164
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Choreografie", Plural = "Choreografien" },
            // 165
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Chromosom", Plural = "Chromosomen" },
            // 166
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Chronist", Plural = "Chronisten" },
            // 167
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Chronologie", Plural = "Chronologien" },
            // 168
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Cisgender", Plural = "Cisgender" },
            // 169
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Codeknacker", Plural = "Codeknacker" },
            // 170
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Coda", Plural = "Codas" },

            // 171
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Containerterminal", Plural = "Containerterminals" },
            // 172
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Cordonbleu", Plural = "Cordonbleus" },
            // 173
            new LanguageNoun { Level = "C2", Article = "die", Noun = "CorpusDelicti", Plural = "CorpusDelicti" },
            // 174
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Crescendo", Plural = "Crescendos" },
            // 175
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Dachfirst", Plural = "Dachfirste" },
            // 176
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Daguerreotypie", Plural = "Daguerreotypien" },
            // 177
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Damoklesschwert", Plural = "Damoklesschwerter" },
            // 178
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Darmtrakt", Plural = "Darmtrakte" },
            // 179
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Dauerbaustelle", Plural = "Dauerbaustellen" },
            // 180
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Debakel", Plural = "Debakel" },

            // 181
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Dekolletéschnitt", Plural = "Dekolletéschnitte" },
            // 182
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Dekonstruktion", Plural = "Dekonstruktionen" },
            // 183
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Delinquententum", Plural = "Delinquententümer" },
            // 184
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Demagoge", Plural = "Demagogen" },
            // 185
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Demutsgeste", Plural = "Demutsgesten" },
            // 186
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Denunziantentum", Plural = "Denunziantentümer" },
            // 187
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Deppenapostroph", Plural = "Deppenapostrophe" },
            // 188
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Derbheit", Plural = "Derbheiten" },
            // 189
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Detonationsgeräusch", Plural = "Detonationsgeräusche" },
            // 190
            new LanguageNoun
                { Level = "C2", Article = "der", Noun = "Deutungsspielraum", Plural = "Deutungsspielräume" },

            // 191
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Devise", Plural = "Devisen" },
            // 192
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Diagnoseverfahren", Plural = "Diagnoseverfahren" },
            // 193
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Dienerstock", Plural = "Dienerstöcke" },
            // 194
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Differenzierbarkeit", Plural = "Differenzierbarkeiten" },
            // 195
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Diktiergerät", Plural = "Diktiergeräte" },
            // 196
            new LanguageNoun
                { Level = "C2", Article = "der", Noun = "Diskursteilnehmer", Plural = "Diskursteilnehmer" },
            // 197
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Dispersion", Plural = "Dispersionen" },
            // 198
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Dissertationsvorhaben", Plural = "Dissertationsvorhaben" },
            // 199
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Doppelmoralist", Plural = "Doppelmoralisten" },
            // 200
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Doppelzüngigkeit", Plural = "Doppelzüngigkeiten" },

            // 201
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Dorfidyll", Plural = "Dorfidylle" },
            // 202
            new LanguageNoun
                { Level = "C2", Article = "der", Noun = "Dornröschenschlaf", Plural = "Dornröschenschläfe" },
            // 203
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Dosisangabe", Plural = "Dosisangaben" },
            // 204
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Drehkreuz", Plural = "Drehkreuze" },
            // 205
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Dreitagebart", Plural = "Dreitagebärte" },
            // 206
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Dringlichkeitssitzung", Plural = "Dringlichkeitssitzungen" },
            // 207
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Drohpotenzial", Plural = "Drohpotenziale" },
            // 208
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Druckabfall", Plural = "Druckabfälle" },
            // 209
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Dunkelziffer", Plural = "Dunkelziffern" },
            // 210
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Durchgangslager", Plural = "Durchgangslager" },

            // 211
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Durchhaltewille", Plural = "Durchhaltewillen" },
            // 212
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Dürftigkeit", Plural = "Dürftigkeiten" },
            // 213
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Dynamit", Plural = "Dynamite" },
            // 214
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Egozentriker", Plural = "Egozentriker" },
            // 215
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Ehrenbezeichnung", Plural = "Ehrenbezeichnungen" },
            // 216
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Eigenlob", Plural = "Eigenlob" },
            // 217
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Eilzusteller", Plural = "Eilzusteller" },
            // 218
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Einbusse", Plural = "Einbussen" },
            // 219
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Eingeständnis", Plural = "Eingeständnisse" },
            // 220
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Einhandsegler", Plural = "Einhandsegler" },

            // 221
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Einhegung", Plural = "Einhegungen" },
            // 222
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Einkammerparlament", Plural = "Einkammerparlamente" },
            // 223
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Einlassstutzen", Plural = "Einlassstutzen" },
            // 224
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Eintönigkeit", Plural = "Eintönigkeiten" },
            // 225
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Einvernehmen", Plural = "Einvernehmen" },
            // 226
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Einwegbecher", Plural = "Einwegbecher" },
            // 227
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Einwirkung", Plural = "Einwirkungen" },
            // 228
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Elektrorad", Plural = "Elektroräder" },
            // 229
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Elektroschrott", Plural = "Elektroschrotte" },
            // 230
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Eliteuniversität", Plural = "Eliteuniversitäten" },

            // 231
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Emblem", Plural = "Embleme" },
            // 232
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Empfindungssturm", Plural = "Empfindungsstürme" },
            // 233
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Empirie", Plural = "Empirien" },
            // 234
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Endlager", Plural = "Endlager" },
            // 235
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Energiefresser", Plural = "Energiefresser" },
            // 236
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Enthüllungsgeschichte", Plural = "Enthüllungsgeschichten" },
            // 237
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Entkrampfungsmittel", Plural = "Entkrampfungsmittel" },
            // 238
            new LanguageNoun
                { Level = "C2", Article = "der", Noun = "Entlastungsangriff", Plural = "Entlastungsangriffe" },
            // 239
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Entomologie", Plural = "Entomologien" },
            // 240
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Entstehungsdatum", Plural = "Entstehungsdaten" },

            // 241
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Entzugsschaden", Plural = "Entzugsschäden" },
            // 242
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Epik", Plural = "Epiken" },
            // 243
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Epochenerbe", Plural = "Epochenerben" },
            // 244
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Erbhof", Plural = "Erbhöfe" },
            // 245
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Erbsünde", Plural = "Erbsünden" },
            // 246
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Erfordernis", Plural = "Erfordernisse" },
            // 247
            new LanguageNoun
                { Level = "C2", Article = "der", Noun = "Erhebungszeitraum", Plural = "Erhebungszeiträume" },
            // 248
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Erinnerungspflicht", Plural = "Erinnerungspflichten" },
            // 249
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Ermunterungsschreiben", Plural = "Ermunterungsschreiben" },
            // 250
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Erosionsfaktor", Plural = "Erosionsfaktoren" },

            // 251
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Eskapade", Plural = "Eskapaden" },
            // 252
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Eskimogebiet", Plural = "Eskimogebiete" },
            // 253
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Esperantist", Plural = "Esperantisten" },
            // 254
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Essayistik", Plural = "Essayistiken" },
            // 255
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Etikettierungssystem", Plural = "Etikettierungssysteme" },
            // 256
            new LanguageNoun
                { Level = "C2", Article = "der", Noun = "Etikettenschwindel", Plural = "Etikettenschwindel" },
            // 257
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Eugenik", Plural = "Eugeniken" },
            // 258
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Eulennest", Plural = "Eulennester" },
            // 259
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Europagedanke", Plural = "Europagedanken" },
            // 260
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Exaltiertheit", Plural = "Exaltiertheiten" },

            // 261
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Exempel", Plural = "Exempel" },
            // 262
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Existentialismus", Plural = "Existentialismen" },
            // 263
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Expansionstendenz", Plural = "Expansionstendenzen" },
            // 264
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Experimentallabor", Plural = "Experimentallabore" },
            // 265
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Expertenstatus", Plural = "Expertenstatus" },
            // 266
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Exploration", Plural = "Explorationen" },
            // 267
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Extrablatt", Plural = "Extrablätter" },
            // 268
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Extrawunsch", Plural = "Extrawünsche" },
            // 269
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Fachmeinung", Plural = "Fachmeinungen" },
            // 270
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Fachterminus", Plural = "Fachtermini" },

            // 271
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Faktorwert", Plural = "Faktorwerte" },
            // 272
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Fallgrube", Plural = "Fallgruben" },
            // 273
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Fallbeispiel", Plural = "Fallbeispiele" },
            // 274
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Fanal", Plural = "Fanale" },
            // 275
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Fehlannahme", Plural = "Fehlannahmen" },
            // 276
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Fehldiagnose", Plural = "Fehldiagnosen" },
            // 277
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Feigling", Plural = "Feiglinge" },
            // 278
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Feindseligkeit", Plural = "Feindseligkeiten" },
            // 279
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Feinwerkzeug", Plural = "Feinwerkzeuge" },
            // 280
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Feinschliff", Plural = "Feinschliffe" },

            // 281
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Feldforschung", Plural = "Feldforschungen" },
            // 282
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Feldlager", Plural = "Feldlager" },
            // 283
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Feldzug", Plural = "Feldzüge" },
            // 284
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Fensterfront", Plural = "Fensterfronten" },
            // 285
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Fernweh", Plural = "Fernweh" },
            // 286
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Festakt", Plural = "Festakte" },
            // 287
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Festivalkultur", Plural = "Festivalkulturen" },
            // 288
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Feuerzeug", Plural = "Feuerzeuge" },
            // 289
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Filialleiter", Plural = "Filialleiter" },
            // 290
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Finesse", Plural = "Finessen" },

            // 291
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Fischernetz", Plural = "Fischernetze" },
            // 292
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Fitzel", Plural = "Fitzel" },
            // 293
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Flatterhaftigkeit", Plural = "Flatterhaftigkeiten" },
            // 294
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Flickwerk", Plural = "Flickwerke" },
            // 295
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Fliederstrauch", Plural = "Fliedersträucher" },
            // 296
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Fliehburg", Plural = "Fliehburgen" },
            // 297
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Flottenmanöver", Plural = "Flottenmanöver" },
            // 298
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Fluglotse", Plural = "Fluglotsen" },
            // 299
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Flugzeugbestuhlung", Plural = "Flugzeugbestuhlungen" },
            // 300
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Fokusgebiet", Plural = "Fokusgebiete" },

            // 301
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Folgefehler", Plural = "Folgefehler" },
            // 302
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Folkloreforschung", Plural = "Folkloreforschungen" },
            // 303
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Fördercontrolling", Plural = "Fördercontrollings" },
            // 304
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Formfehler", Plural = "Formfehler" },
            // 305
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Formsache", Plural = "Formsachen" },
            // 306
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Forschungsdesiderat", Plural = "Forschungsdesiderate" },
            // 307
            new LanguageNoun
                { Level = "C2", Article = "der", Noun = "Fortbildungsbedarf", Plural = "Fortbildungsbedarfe" },
            // 308
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Fortschreibung", Plural = "Fortschreibungen" },
            // 309
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Forumtheater", Plural = "Forumtheater" },
            // 310
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Fötus", Plural = "Föten" },

            // 311
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Fragestellung", Plural = "Fragestellungen" },
            // 312
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Freilandhuhn", Plural = "Freilandhühner" },
            // 313
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Fremdkörper", Plural = "Fremdkörper" },
            // 314
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Fresssucht", Plural = "Fresssüchte" },
            // 315
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Frettchengehege", Plural = "Frettchengehege" },
            // 316
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Frischmarkt", Plural = "Frischmärkte" },
            // 317
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Frisierkunst", Plural = "Frisierkünste" },
            // 318
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Froschkonzert", Plural = "Froschkonzerte" },
            // 319
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Frustkauf", Plural = "Frustkäufe" },
            // 320
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Fundamentalkritik", Plural = "Fundamentalkritiken" },

            // 321
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Funktionsdiagramm", Plural = "Funktionsdiagramme" },
            // 322
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Furor", Plural = "Furors" },
            // 323
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Fusionspolitik", Plural = "Fusionspolitiken" },
            // 324
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Futurprojekt", Plural = "Futurprojekte" },
            // 325
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Galerist", Plural = "Galeristen" },
            // 326
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Galionsfigur", Plural = "Galionsfiguren" },
            // 327
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Gasspeichersystem", Plural = "Gasspeichersysteme" },
            // 328
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Gebärmutterhals", Plural = "Gebärmutterhälse" },
            // 329
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Gebetsmühle", Plural = "Gebetsmühlen" },
            // 330
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Gebirgsmassiv", Plural = "Gebirgsmassive" },

            // 331
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Gebrauchswert", Plural = "Gebrauchswerte" },
            // 332
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Geburtserleichterung", Plural = "Geburtserleichterungen" },
            // 333
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Gegenargument", Plural = "Gegenargumente" },
            // 334
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Geheimgang", Plural = "Geheimgänge" },
            // 335
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Geheimpolizei", Plural = "Geheimpolizeien" },
            // 336
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Geheimwissen", Plural = "Geheimwissen" },
            // 337
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Geistesmensch", Plural = "Geistesmenschen" },
            // 338
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Geizmentalität", Plural = "Geizmentalitäten" },
            // 339
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Geläut", Plural = "Geläute" },
            // 340
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Geltungsverlust", Plural = "Geltungsverluste" },

            // 341
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Gemeindeordnung", Plural = "Gemeindeordnungen" },
            // 342
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Gemeinwesen", Plural = "Gemeinwesen" },
            // 343
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Genitivfehler", Plural = "Genitivfehler" },
            // 344
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Genusssucht", Plural = "Genusssüchte" },
            // 345
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Gerede", Plural = "Gerede" },
            // 346
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Gerichtsstand", Plural = "Gerichtsstände" },
            // 347
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Gesandtschaft", Plural = "Gesandtschaften" },
            // 348
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Geschicklichkeits­spiel", Plural = "Geschicklichkeitsspiele" },
            // 349
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Gesichtsverlust", Plural = "Gesichtsverluste" },
            // 350
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Gesinnungsgemeinschaft", Plural = "Gesinnungsgemeinschaften" },

            // 351
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Gestühl", Plural = "Gestühle" },
            // 352
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Gewaltherrscher", Plural = "Gewaltherrscher" },
            // 353
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Gewaltprävention", Plural = "Gewaltpräventionen" },
            // 354
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Gewohnheitsrecht", Plural = "Gewohnheitsrechte" },
            // 355
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Gigantismus", Plural = "Gigantismen" },
            // 356
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Glanzzeit", Plural = "Glanzzeiten" },
            // 357
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Globalitätsbewusstsein", Plural = "Globalitätsbewusstseine" },
            // 358
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Godfather", Plural = "Godfathers" },
            // 359
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Gorillahaltung", Plural = "Gorillahaltungen" },
            // 360
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Götterbild", Plural = "Götterbilder" },

            // 361
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Grabesstille", Plural = "Grabesstillen" },
            // 362
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Grabschändung", Plural = "Grabschändungen" },
            // 363
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Graduierungssystem", Plural = "Graduierungssysteme" },
            // 364
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Granatsplitter", Plural = "Granatsplitter" },
            // 365
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Gratwanderung", Plural = "Gratwanderungen" },
            // 366
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Grauen", Plural = "Grauen" },
            // 367
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Groll", Plural = "Grolle" },
            // 368
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Groteske", Plural = "Grotesken" },
            // 369
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Guckloch", Plural = "Gucklöcher" },
            // 370
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Gütestandard", Plural = "Gütestandards" },

            // 371
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Hader", Plural = "Hader" },
            // 372
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Haftungssystem", Plural = "Haftungssysteme" },
            // 373
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Hagelsturm", Plural = "Hagelstürme" },
            // 374
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Halde", Plural = "Halden" },
            // 375
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Hallenbad", Plural = "Hallenbäder" },
            // 376
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Hallodri", Plural = "Hallodris" },
            // 377
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Halogenlampe", Plural = "Halogenlampen" },
            // 378
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Halsband", Plural = "Halsbänder" },
            // 379
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Haltepunkt", Plural = "Haltepunkte" },
            // 380
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Handelskette", Plural = "Handelsketten" },

            // 381
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Handzeichen", Plural = "Handzeichen" },
            // 382
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Hangelpfad", Plural = "Hangelpfade" },
            // 383
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Härtefallregelung", Plural = "Härtefallregelungen" },
            // 384
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Harnisch", Plural = "Harnische" },
            // 385
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Harnstoff", Plural = "Harnstoffe" },
            // 386
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Havarie", Plural = "Havarien" },
            // 387
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Hauptgericht", Plural = "Hauptgerichte" },
            // 388
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Hauptwohnsitz", Plural = "Hauptwohnsitze" },
            // 389
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Hebamme", Plural = "Hebammen" },
            // 390
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Heckfenster", Plural = "Heckfenster" },

            // 391
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Heereszug", Plural = "Heereszüge" },
            // 392
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Heimfahrt", Plural = "Heimfahrten" },
            // 393
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Heiratsinstitut", Plural = "Heiratsinstitute" },
            // 394
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Heißhunger", Plural = "Heißhunger" },
            // 395
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Heizperiode", Plural = "Heizperioden" },
            // 396
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Heldengedicht", Plural = "Heldengedichte" },
            // 397
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Hemmfaktor", Plural = "Hemmfaktoren" },
            // 398
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Hemmschwelle", Plural = "Hemmschwellen" },
            // 399
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Hennenei", Plural = "Henneneier" },
            // 400
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Herbststurm", Plural = "Herbststürme" },

            // 401
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Herde", Plural = "Herden" },
            // 402
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Herzblut", Plural = "Herzblut" },
            // 403
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Hescher", Plural = "Hescher" },
            // 404
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Heuchelei", Plural = "Heucheleien" },
            // 405
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Hexengebräu", Plural = "Hexengebräue" },
            // 406
            new LanguageNoun
                { Level = "C2", Article = "der", Noun = "Hierarchieaufbau", Plural = "Hierarchieaufbauten" },
            // 407
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Hilfskraft", Plural = "Hilfskräfte" },
            // 408
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Himmelsgewölbe", Plural = "Himmelsgewölbe" },
            // 409
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Hintern", Plural = "Hintern" },
            // 410
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Hirarchieebene", Plural = "Hirarchieebenen" },

            // 411
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Hochfischen", Plural = "Hochfischen" },
            // 412
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Hochkonjunktur", Plural = "Hochkonjunkturen" },
            // 413
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Hochstapelei", Plural = "Hochstapeleien" },
            // 414
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Hoffnungsbild", Plural = "Hoffnungsbilder" },
            // 415
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Holzschnitt", Plural = "Holzschnitte" },
            // 416
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Honigernte", Plural = "Honigernten" },
            // 417
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Hüllenstadium", Plural = "Hüllenstadien" },
            // 418
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Humanist", Plural = "Humanisten" },
            // 419
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Hundeliebe", Plural = "Hundelieben" },
            // 420
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Hutgeflecht", Plural = "Hutgeflechte" },

            // 421
            new LanguageNoun
                { Level = "C2", Article = "der", Noun = "Hypothesenentwurf", Plural = "Hypothesenentwürfe" },
            // 422
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Idiosynkrasie", Plural = "Idiosynkrasien" },
            // 423
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Igelnest", Plural = "Igelnester" },
            // 424
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Ikonoklast", Plural = "Ikonoklasten" },
            // 425
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Illoyalität", Plural = "Illoyalitäten" },
            // 426
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Imitat", Plural = "Imitate" },
            // 427
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Imperativsatz", Plural = "Imperativsätze" },
            // 428
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Imposanz", Plural = "Imposanzen" },
            // 429
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Impulszentrum", Plural = "Impulszentren" },
            // 430
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Inbegriff", Plural = "Inbegriffe" },

            // 431
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Incognitogrenze", Plural = "Incognitogrenzen" },
            // 432
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Indizienverfahren", Plural = "Indizienverfahren" },
            // 433
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Insiderscherz", Plural = "Insiderscherze" },
            // 434
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Insolvenzmasse", Plural = "Insolvenzmassen" },
            // 435
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Intendantenzimmer", Plural = "Intendantenzimmer" },
            // 436
            new LanguageNoun
                { Level = "C2", Article = "der", Noun = "Intendantenwechsel", Plural = "Intendantenwechsel" },
            // 437
            new LanguageNoun
            {
                Level = "C2", Article = "die", Noun = "Interdependenzforschung", Plural = "Interdependenzforschungen"
            },
            // 438
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Interessegremium", Plural = "Interessegremien" },
            // 439
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Irrglaube", Plural = "Irrglauben" },
            // 440
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Irrsinnstat", Plural = "Irrsinnstaten" },

            // 441
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Jagdlager", Plural = "Jagdlager" },
            // 442
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Jargon", Plural = "Jargons" },
            // 443
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Journalismusforschung", Plural = "Journalismusforschungen" },
            // 444
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Kakteenbeet", Plural = "Kakteenbeete" },
            // 445
            new LanguageNoun
                { Level = "C2", Article = "der", Noun = "Kaleidoskopeffekt", Plural = "Kaleidoskopeffekte" },
            // 446
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Kalkulationstabelle", Plural = "Kalkulationstabellen" },
            // 447
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Kalligraphieset", Plural = "Kalligraphiesets" },
            // 448
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Kanalbau", Plural = "Kanalbauten" },
            // 449
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Kante", Plural = "Kanten" },
            // 450
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Kapitalverkehr", Plural = "Kapitalverkehre" },

            // 451
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Karosseriebauer", Plural = "Karosseriebauer" },
            // 452
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Kartoffelfäule", Plural = "Kartoffelfäulen" },
            // 453
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Kartografiewerk", Plural = "Kartografiewerke" },
            // 454
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Kasper", Plural = "Kasper" },
            // 455
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Kastanienallee", Plural = "Kastanienalleen" },
            // 456
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Katharsiserlebnis", Plural = "Katharsiserlebnisse" },
            // 457
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Katzenjammer", Plural = "Katzenjammer" },
            // 458
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Kaufzurückhaltung", Plural = "Kaufzurückhaltungen" },
            // 459
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Kavaliersdelikt", Plural = "Kavaliersdelikte" },
            // 460
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Kehrreim", Plural = "Kehrreime" },

            // 461
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Kehrtwende", Plural = "Kehrtwenden" },
            // 462
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Kennwort", Plural = "Kennwörter" },
            // 463
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Kerkermeister", Plural = "Kerkermeister" },
            // 464
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Kiesgrube", Plural = "Kiesgruben" },
            // 465
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Kinderhospiz", Plural = "Kinderhospize" },
            // 466
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Kindersarg", Plural = "Kindersärge" },
            // 467
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Kirchenstrafe", Plural = "Kirchenstrafen" },
            // 468
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Klagelied", Plural = "Klagelieder" },
            // 469
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Klammersatz", Plural = "Klammersätze" },
            // 470
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Klangvielfalt", Plural = "Klangvielfalten" },

            // 471
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Kleeblatt", Plural = "Kleeblätter" },
            // 472
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Klettergurt", Plural = "Klettergurte" },
            // 473
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Klimaveränderung", Plural = "Klimaveränderungen" },
            // 474
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Kochmesser", Plural = "Kochmesser" },
            // 475
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Kompasskurs", Plural = "Kompasskurse" },
            // 476
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Komplexitätsforschung", Plural = "Komplexitätsforschungen" },
            // 477
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Konklave", Plural = "Konklaven" },
            // 478
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Konfessionskrieg", Plural = "Konfessionskriege" },
            // 479
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Konjunkturlage", Plural = "Konjunkturlagen" },
            // 480
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Konservierungsverfahren", Plural = "Konservierungsverfahren" },

            // 481
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Konsumrausch", Plural = "Konsumräusche" },
            // 482
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Kontaktpflege", Plural = "Kontaktpflegen" },
            // 483
            new LanguageNoun
                { Level = "C2", Article = "das", Noun = "Konversionsmerkmal", Plural = "Konversionsmerkmale" },
            // 484
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Kopfsprung", Plural = "Kopfsprünge" },
            // 485
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Kopierstation", Plural = "Kopierstationen" },
            // 486
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Korrektiv", Plural = "Korrektive" },
            // 487
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Krakenarm", Plural = "Krakenarme" },
            // 488
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Krämerseele", Plural = "Krämerseelen" },
            // 489
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Kräuterbündel", Plural = "Kräuterbündel" },
            // 490
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Krimskrams", Plural = "Krimskrams" },

            // 491
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Kritikunfähigkeit", Plural = "Kritikunfähigkeiten" },
            // 492
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Krokodilbecken", Plural = "Krokodilbecken" },
            // 493
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Kronzeuge", Plural = "Kronzeugen" },
            // 494
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Kryptografie", Plural = "Kryptografien" },
            // 495
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Kugelgelenk", Plural = "Kugelgelenke" },
            // 496
            new LanguageNoun { Level = "C2", Article = "der", Noun = "Kulturtourist", Plural = "Kulturtouristen" },
            // 497
            new LanguageNoun { Level = "C2", Article = "die", Noun = "Kumulation", Plural = "Kumulationen" },
            // 498
            new LanguageNoun { Level = "C2", Article = "das", Noun = "Kupferdach", Plural = "Kupferdächer" },
            // 499
            new LanguageNoun
                { Level = "C2", Article = "der", Noun = "Kuriositätenkabinett", Plural = "Kuriositätenkabinette" },
            // 500
            new LanguageNoun
                { Level = "C2", Article = "die", Noun = "Kürzestgeschichte", Plural = "Kürzestgeschichten" },
        };

        Service.UpdateNounsAsync(data);
    }
}