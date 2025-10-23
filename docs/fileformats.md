# Vocup file formats

Vocup uses different proprietary formats:

| Extension | Name                            | Usage            | Remarks |
|-----------|---------------------------------|------------------|---------|
| .vhf      | **V**okabel**h**eft **f**ile    | Vocabulary data  |         |
| .vhr      | **V**okabel**h**eft **r**esults | Practice results | Not used for vhf2 files anymore |
| .vdp      |                                 | Backup file      | Backup creation removed in Vocup 1.8.0<br>Backup restore removed in Vocup 1.9.0 |

## .vhf v2

Since Vocup v2, _Vokabelheft files_ are ZIP archives and not encrypted anymore.
Vocup uses the ZIP header to determine whether a .vhf file is vhf1 or vhf2.

The header file makes it easier to identify a ZIP archive as a Vocup file.
Future file formats may maintain compatibility with Vocup v2 by keeping a `book.2.json` file but also adding a `book.3.json` with breaking changes.

`words[].creationTime` (DateTime) was added in file version 2.1.

### Example

`VOCUP VOCABULARY BOOK`
```json
{
  "fileVersion": "2.1",
}
```

`book.2.json`
```json
{
  "motherTongue": "Deutsch",
  "foreignLanguage": "Englisch",
  "practiceMode": "AskForForeignLanguage",
  "words": [
    {
      "motherTongue": "Diskriminierung",
      "foreignLang": "discrimination",
      "foreignLangSynonym": null,
      "practiceStateNumber": 0,
      "practiceDate": "",
      "creationTime": "2018-04-06T13:05:21"
    },
    {
      "motherTongue": "eingehend untersuchen (AE/BE)",
      "foreignLang": "to scrutinize",
      "foreignLangSynonym": "to scrutinise",
      "practiceStateNumber": 1,
      "practiceDate": "2018-04-06T23:04:21",
      "creationTime": "2018-04-06T13:05:41"
    }
  ]
}
```

## .vhf v1

*Vokabelheft files* contain the base64 encoded ciphertext of the DES encrypted inner file.
The encryption is done with a hard-coded key and offers no security but complicates reverse engineering of the file format.
As Vocup became open source in 2018, any protection against reverse engineering is useless.
Future versions of Vocup may drop encryption of files but v1.x will keep this feature for compatibility.

The inner file has 4 lines of metadata. The first line is the file version.
As this number can be parsed as an integer using `1.5.0` for example would be a breaking change.
The second line is the name of the associated *Vokabelheft results* file.
The third line the mother tongue and the fourth line the foreign language.

Each word then has its own line:
```
mother tongue#foreign language#[foreign language (synonym)]
```

### Example
```
1.0
Q2B0Y7x8R3B1j9s6R4q6W2V5
Deutsch
Englisch
Diskriminierung#discrimination#
eingehend untersuchen (AE/BE)#to scrutinize#to scrutinise
Entfremdung, Distanzierung#alienation#
entstellen#to warp#
enttäuscht#disappointed#crestfallen
entwähnen#to wean off#
Freiheit#freedom#liberty
Gefängnisstrafe#imprisonment#jail sentence
gehäuft, gestapelt#piled#
Gewaltenteilung#system of checks and balances#
gipfeln (in)#to culminate (in)#
Gründungsväter#Founding Fathers#
halbieren#to halve#
Haltung, Einstellung#attitude#
Handgelenk#wrist#
harte Arbeit#hard work#
Haufen, Stapel#pile#
```

## .vhr

> ⚠️ This file format is deprecated. Since vhf2 results are saved in the vocabulary book.

Like *Vokabelheft files* *Vokabelheft result* files are DES encrypted and saved as a base64 string.

The inner file contains 2 lines of metadata.
The first line is the path of the associated `.vhf` file.
The second line is the `PracticeMode`. `1` means that the user has to enter each word in foreign langauge.
`2` means that the word is shown in foreign language and the user has to enter it in the mother tongue.

Each word then has its own line:
```
practice state number#dd.MM.yyyy HH:mm
```
The `PracticeStateNumber` is associated with the `PracticeState` enum
but is basically a counter with an offset of `1` because this value means wrongly practiced.
Depending on the user's settings we reach the state of `FullyPracticed` earlier or later.

### Example
```
D:\Schule\Englisch\Vocabulary\Year 11.vhf
1
0#
1#16.04.2018 23:04
2#23.11.2017 22:29
0#
3#23.11.2017 22:35
4#23.11.2017 22:25
2#23.11.2017 22:25
0#
2#23.11.2017 22:37
4#23.11.2017 22:28
1#16.04.2018 23:04
0#
0#
3#23.11.2017 22:35
2#23.11.2017 22:37
1#16.04.2018 23:03
```

## .vdp

> ⚠️ This file format is deprecated. Since Vocup 1.8.0 it is not possible to create new backups anymore. Since Vocup 1.9.0 is also not possible to restore backups anymore.

This format was used for Vocup Backups. It is basically a zip file using `Deflate` compression:
```
*.vdp/
    chars/
        {LangName}.txt
    vhf/
        0.vhf
        1.vhf
        2.vhf
        ...
    vhr/
        {VhrCode}.vhr
    chars.log
    vhf_vhr.log
    vhr.log
```
#### chars
The `chars` folder contains all custom special char files as they are usually stored in `%vhr%\specialchar`.
The `chars.log` file simply lists all backuped special char files.

#### vhf
The `vhf` folder is the most complicated one. In the folder you can find all backuped `.vhf` files not with their original name, but with numbers.
These numbers are resolved with the `vhf_vhr.log` file.

Like the other logfiles it included one line per item:
```
{number}|%vhf%\{relative path}|{vhr code}
```

##### Example
```
0|%vhf%\characterising adjectives.vhf|
1|%vhf%\E2L.vhf|p9R8t1U7R3y3K7c0b8m5B6E0
2|%vhf%\Lessons.vhf|
3|%vhf%\Year 11.vhf|Q2B0Y7x8R3B1j9s6R4q6W2V5
```

#### vhr
The `vhr` folder works pretty much like the `chars` folder.
All `.vhr` files are stored in this folder with their original name.
In the `vhr.log` file all these files are simply listed.
